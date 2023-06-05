public static class JobStateExtensions
{
    public static bool IsEndState(this JobStateV1 jobState)
    {
        return jobState == JobStateV1.Failed ||
                jobState == JobStateV1.Completed ||
                jobState == JobStateV1.Aborted ||
                jobState == JobStateV1.TimedOut ||
                jobState == JobStateV1.NoSignal ||
                jobState == JobStateV1.MaxRetriesReached;
    }
}
