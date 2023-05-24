public interface IJobScheduler
{
    Task<JobCreateResponseV1> JobCreateAsync(ArgoJobCreateRequestV1 jobCreateRequest, string jobKind);
}