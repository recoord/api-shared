using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public abstract class ASqsBackgroundService : BackgroundService
{
    private readonly string _serviceName;
    private readonly ILogger _logger;
    private readonly IAmazonSQS _sqs;
    private readonly string _sqsQueueUrl;
    private int _sqsCoolDownWaitTimeInSeconds;
    protected readonly int _maxMessageRetries;
    private readonly int _queueTtl;
    protected readonly int _maxTimeoutSeconds;

    public ASqsBackgroundService(
        string serviceName,
        ILogger logger,
        IAmazonSQS sqs,
        string sqsQueueUrl)
    {
        _serviceName = serviceName;
        _logger = logger;
        _sqs = sqs;
        _sqsQueueUrl = sqsQueueUrl;
        _sqsCoolDownWaitTimeInSeconds = 1;
        _maxMessageRetries = int.Parse(Environment.GetEnvironmentVariable("MAX_SQS_MESSAGE_RETRIES") ?? "3");
        _queueTtl = int.Parse(Environment.GetEnvironmentVariable("SQS_TTL") ?? "3");
        _maxTimeoutSeconds = int.Parse(Environment.GetEnvironmentVariable("MAX_TIMEOUT_SECONDS") ?? "10800");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Starting {_serviceName} service.");
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var messageReceiptHandle = "";
            try
            {
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _sqsQueueUrl,
                    WaitTimeSeconds = 5
                };
                var result = await _sqs.ReceiveMessageAsync(request);
                _sqsCoolDownWaitTimeInSeconds = 1;

                if (result.Messages.Any())
                {
                    foreach (var message in result.Messages)
                    {
                        _logger.LogInformation(message.Body);
                        messageReceiptHandle = message.ReceiptHandle;

                        var response = await this.HandleMessageAsync(message);

                        if (response.Result == ESqsMessageHandlerResult.Retry)
                        {
                            int waitTime = Math.Min(_queueTtl + (int)Math.Pow(2, response.Retries), _maxTimeoutSeconds);
                            await _sqs.ChangeMessageVisibilityAsync(_sqsQueueUrl, message.ReceiptHandle, waitTime);
                            _logger.LogWarning($"Will retry the message again in {waitTime} seconds. Moving to the next message.");

                            // Don't delete message but retry later
                            messageReceiptHandle = "";
                        }
                        else
                        {
                            await _sqs.DeleteMessageAsync(_sqsQueueUrl, message.ReceiptHandle);
                            messageReceiptHandle = "";
                        }
                    }
                }
                else
                {
                    _logger.LogDebug("No messages from SQS");
                }
            }
            catch (AmazonSQSException e)
            {
                _logger.LogError(e.ToString());
                await Task.Delay(TimeSpan.FromSeconds(_sqsCoolDownWaitTimeInSeconds), stoppingToken);
                _sqsCoolDownWaitTimeInSeconds *= 2;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                if (!string.IsNullOrEmpty(messageReceiptHandle))
                {
                    await _sqs.DeleteMessageAsync(_sqsQueueUrl, messageReceiptHandle);
                }
            }
        }
    }

    protected abstract Task<SqsMessageHandlerResponse> HandleMessageAsync(Message message);

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Stopping {_serviceName} service.");
        await base.StopAsync(stoppingToken);
        _logger.LogInformation($"Done stopping {_serviceName} service.");
    }
}