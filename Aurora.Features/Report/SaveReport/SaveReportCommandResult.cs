namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommandResult
{
    public SaveReportCommandResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public static SaveReportCommandResult Success(SaveReportCommand command)
    {
        return new SaveReportCommandResult(true) { ReportRecord = command };
    }

    public bool IsSuccess { get; set; } = false;

    public SaveReportCommand ReportRecord { get; set; }
}