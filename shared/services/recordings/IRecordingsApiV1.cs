namespace Api.Shared;

public interface IRecordingsApiV1
{
    Task<RecordingGetResponseV1> RecordingGet(RecordingGetRequestV1 recordingGetRequest);
}