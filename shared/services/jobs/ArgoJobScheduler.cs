namespace Api.Shared;

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

public class ArgoJobScheduler : IJobScheduler
{
    private readonly HttpClient _httpClient;
    private readonly string _argoSubmitUrl;
    private readonly string _argoToken;
    private readonly string _argoAuthHeader;
    private readonly Uri _jobsApiUrl;
    private readonly ILogger _logger;

    public ArgoJobScheduler(HttpClient httpClient, ILogger<ArgoJobScheduler> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        var url = Environment.GetEnvironmentVariable("JOBS_API_URL") ?? "https://localhost:7141/jobs/v1";
        _jobsApiUrl = new Uri(url);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        var username = Environment.GetEnvironmentVariable("API_USERNAME")!;
        var password = Environment.GetEnvironmentVariable("API_PASSWORD")!;
        var authenticationString = $"{username}:{password}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
        _httpClient.DefaultRequestHeaders.Remove(HeaderNames.Authorization);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Basic {base64EncodedAuthenticationString}");
        _argoSubmitUrl = Environment.GetEnvironmentVariable("ARGO_SUBMIT_URL")!;
        _argoToken = Environment.GetEnvironmentVariable("ARGO_TOKEN")!;
        _argoAuthHeader = Environment.GetEnvironmentVariable("ARGO_AUTH_HEADER") ?? "Bearer";
    }

    public async Task<JobCreateResponseV1> JobCreateAsync(ArgoJobCreateRequestV1 jobCreateRequest, string jobKind)
    {
        var argoArgs = new ArgoWorkflowArgsV1
        {
            Namespace = "processing-argo",
            ResourceKind = "WorkflowTemplate",
            ResourceName = jobCreateRequest.WorkflowName,
            SubmitOptions = new SubmitOptions
            {
                Parameters = jobCreateRequest.JobInputs
            }
        };
        var args = JsonSerializer.Serialize(argoArgs, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        var body = new JobCreateRequestV1
        {
            JobSystemKind = "argo",
            JobSystemCreateUrlPath = _argoSubmitUrl,
            JobTimeoutMinutes = jobCreateRequest.JobTimeoutMinutes,
            JobKind = jobKind,
            JobSystemArgs = new JobSystemArgsV1
            {
                additionalJSON = JsonSerializer.Deserialize<Dictionary<string, object>>(args) ?? new Dictionary<string, object>()
            },
            Tags = jobCreateRequest.Tags,
            Notifications = jobCreateRequest.Notifications,
            Synchronized = jobCreateRequest.Synchronized,
            SystemAuth = new SystemAuthV1
            {
                Header = _argoAuthHeader,
                Token = _argoToken
            },
            AwsCredentials = jobCreateRequest.AwsCredentials
        };

        try
        {
            var data = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_jobsApiUrl, data);
            var result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode.IsSuccessStatusCode())
            {
                return JsonSerializer.Deserialize<JobCreateResponseV1>(result, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
            }
            else
            {
                _logger.LogWarning($"Could not create argo job: request={JsonSerializer.Serialize(body)} statusCode={response.StatusCode}, result={result}");
                return new JobCreateResponseV1();
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Error creating argo job: request={JsonSerializer.Serialize(body)}, error={e.ToString()}");
            return new JobCreateResponseV1();
        }
    }
}