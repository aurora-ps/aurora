namespace Aurora.Features.Report.UnDeleteReport;

public class UnDeleteReportCommandResult
{
    private UnDeleteReportCommandResult(string reportId, bool success)
    {
        ReportId = reportId;
        Success = success;
    }

    private UnDeleteReportCommandResult(string reportId, bool success, string errorMessage) : this(reportId, success)
    {
        ErrorMessage = errorMessage;
    }

    public string ReportId { get; }

    public bool Success { get; }

    public string ErrorMessage { get; }
    public DateTime? DeletedOnUtc { get; }

    public static UnDeleteReportCommandResult CreateSuccess(string reportId)
    {
        return new UnDeleteReportCommandResult(reportId, true);
    }

    public static UnDeleteReportCommandResult CreateFailure(string reportId, string errorMessage)
    {
        return new UnDeleteReportCommandResult(reportId, false, errorMessage);
    }
}