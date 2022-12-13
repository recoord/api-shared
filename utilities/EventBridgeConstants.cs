public static class EventBridgeConstants
{
    #region Detail types
    public static readonly string DetailTypeRecordingUploaded = "recording-uploaded";
    public static readonly string DetailTypeJobStateChanged = "job-state-changed";
    public static readonly string DetailTypeS3ObjectCreated = "Object Created";
    #endregion // Detail types

    #region Event source
    public static readonly string SourceRecordings = "recordings-api";
    public static readonly string SourceJobs = "jobs-api";
    public static readonly string SourceAwsS3 = "aws.s3";
    public static readonly string SourceArgoWorkflows = "argo-workflows";
    #endregion // Event source
}