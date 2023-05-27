namespace Aurora.Features.Report.DeleteReport;

public class DeleteReportCommandResult
{
    private DeleteReportCommandResult(string reportId, bool success, DateTime? deletedOnUtc)
    {
        ReportId = reportId;
        Success = success;
        DeletedOnUtc = deletedOnUtc;
    }

    private DeleteReportCommandResult(string reportId, bool success, string errorMessage)
    {
        this.ReportId = reportId;
        this.Success = success;
        this.ErrorMessage = errorMessage;
    }

    public string ReportId { get; }

    public bool Success { get; }

    public string ErrorMessage { get; }

    public DateTime? DeletedOnUtc { get; set; }

    public static DeleteReportCommandResult CreateSuccess(string reportId, DateTime deletedOnUtc)
    {
        return new DeleteReportCommandResult(reportId, true, deletedOnUtc);
    }

    public static DeleteReportCommandResult CreateFailure(string reportId, string errorMessage)
    {
        return new DeleteReportCommandResult(reportId, false, errorMessage);
    }
}