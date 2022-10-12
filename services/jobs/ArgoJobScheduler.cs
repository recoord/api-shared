using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Net.Http.Headers;

public class ArgoJobScheduler
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private readonly string _token;

    public ArgoJobScheduler(ILogger<ArgoJobScheduler> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        var url = Environment.GetEnvironmentVariable("JOBS_API_URL") ?? "https://localhost:7141/jobs/v1";
        _httpClient.BaseAddress = new Uri(url);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        _token = Environment.GetEnvironmentVariable("ARGO_TOKEN")!;
    }

    public async Task<JobCreateResponseV1> JobCreateAsync(ArgoJobCreateRequestV1 jobCreateRequest)
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
            JobSystemCreateUrlPath = "/workflows/processing-argo/submit",
            JobSystemAbortUrlPath = "/workflows/processing-argo/{0}/stop",
            JobTimeoutMinutes = jobCreateRequest.JobTimeoutMinutes,
            SystemAuth = new SystemAuthV1
            {
                Header = "Bearer",
                Token = _token
            },
            JobSystemArgs = new JobSystemArgsV1
            {
                additionalJSON = JsonSerializer.Deserialize<Dictionary<string, object>>(args) ?? new Dictionary<string, object>()
            }
        };
        var data = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync((string?)null, data);
        var result = response.Content.ReadAsStringAsync().Result;

        _logger.LogInformation($"Result: {result}");

        if (response?.StatusCode == HttpStatusCode.OK)
        {
            return JsonSerializer.Deserialize<JobCreateResponseV1>(result, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
        }
        else
        {
            throw new InvalidOperationException(result);
        }
    }
}