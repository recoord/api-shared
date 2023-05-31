using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

public class ArgoJobScheduler : IJobScheduler
{
    private readonly HttpClient _httpClient;
    private readonly string _argoSubmitUrl;
    private readonly string _argoToken;
    private readonly Uri _jobsApiUrl;

    public ArgoJobScheduler(HttpClient httpClient)
    {
        _httpClient = httpClient;
        var url = Environment.GetEnvironmentVariable("JOBS_API_URL") ?? "https://localhost:7141/jobs/v1";
        _jobsApiUrl = new Uri(url);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        var username = Environment.GetEnvironmentVariable("API_USERNAME")!;
        var password = Environment.GetEnvironmentVariable("API_PASSWORD")!;
        var authenticationString = $"{username}:{password}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
        _httpClient.DefaultRequestHeaders.Remove(HeaderNames.Authorization);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Basic {base64EncodedAuthenticationString}");
        _argoSubmitUrl = Environment.GetEnvironmentVariable("ARGO_SUBMIT_URL")!;
        _argoToken = Environment.GetEnvironmentVariable("ARGO_TOKEN")!;
    }

    public async Task<JobCreateResponseV1> JobCreateAsync(ArgoJobCreateRequestV1 jobCreateRequest, string jobKind)
    {
        var argoArgs = new ArgoWorkflowArgsV1
        {
            Namespace = "processing-argo",
            resourceKind = "WorkflowTemplate",
            resourceName = jobCreateRequest.WorkflowName,
            submitOptions = new SubmitOptions
            {
                parameters = jobCreateRequest.JobInputs
            }
        };
        var args = JsonSerializer.Serialize(argoArgs);

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
                return new JobCreateResponseV1();
            }
        }
        catch (Exception)
        {
            return new JobCreateResponseV1();
        }
    }
}