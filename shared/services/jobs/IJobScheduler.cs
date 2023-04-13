public interface IJobScheduler
{
    Task<JobCreateResponseV1> JobCreateAsync(Guid recordingId, ArgoJobCreateRequestV1 jobCreateRequest, string jobKind);
}