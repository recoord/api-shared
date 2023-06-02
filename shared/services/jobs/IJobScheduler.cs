using Microsoft.AspNetCore.Http;

public interface IJobScheduler
{
    Task<JobCreateResponseV1> JobCreateAsync(ArgoJobCreateRequestV1 jobCreateRequest, string jobKind);

    Task<IResult> JobRerunAsync(ArgoJobCreateRequestV1 jobCreateRequest);
}