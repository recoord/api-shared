using System.Text.Json;
namespace Api.Shared;

using Amazon.Lambda.CloudWatchEvents;
using Amazon.SQS;
using Amazon.SQS.Model;

public class TestJobScheduler : IJobScheduler
{
    private readonly HttpClient _httpClient;
    private readonly string _sqsQueueUrl;
    private readonly IAmazonSQS _sqsClient;

    public TestJobScheduler(HttpClient httpClient, IAmazonSQS sqs)
    {
        _httpClient = httpClient;
        _sqsQueueUrl = Environment.GetEnvironmentVariable("TEST_SQS_URL")!;
        _sqsClient = sqs;
    }
    public async Task<JobCreateResponseV1> JobCreateAsync(ArgoJobCreateRequestV1 jobCreateRequest, string jobKind)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        var jobId = Guid.NewGuid();

        var jobStateNotification = new CloudWatchEvent<JobStateNotificationV1>
        {
            Source = EventBridgeConstants.SourceJobs,
            DetailType = EventBridgeConstants.DetailTypeJobStateChanged,
            Detail = new JobStateNotificationV1
            {
                JobId = jobId,
                JobState = JobStateV1.Completed
            }
        };

        var sendRequest = new SendMessageRequest
        {
            QueueUrl = _sqsQueueUrl,
            MessageBody = JsonSerializer.Serialize(jobStateNotification, new JsonSerializerOptions(JsonSerializerDefaults.Web))
        };
        var sendResponse = await _sqsClient.SendMessageAsync(sendRequest);

        return new JobCreateResponseV1
        {
            Job = new JobV1
            {
                JobId = jobId,
                JobState = JobStateV1.Completed
            }
        };
    }
}