public static class EventBridgeConstants
{
    #region Detail types
    public static readonly string DetailTypeRecordingUploaded = "recording-uploaded";
    public static readonly string DetailTypeJobStateChanged = "job-state-changed";
    #endregion // Detail types

    #region Event source
    public static readonly string SourceRecordings = "recordings-api";
    public static readonly string SourceJobs = "jobs-api";
    #endregion // Event source
}