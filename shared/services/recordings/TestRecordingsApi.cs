namespace Api.Shared;

public class TestRecordingsApi : IRecordingsApiV1
{
    public TestRecordingsApi(HttpClient httpClient)
    {
    }

    public async Task<RecordingGetResponseV1> RecordingGet(RecordingGetRequestV1 recordingGetRequest)
    {
        await Task.CompletedTask;

        if (recordingGetRequest.RecordingId == Guid.Empty)
        {
            return new RecordingGetResponseV1();
        }

        return new RecordingGetResponseV1
        {
            Recording = new RecordingV1
            {
                RecordingId = recordingGetRequest.RecordingId
            }
        };
    }
}