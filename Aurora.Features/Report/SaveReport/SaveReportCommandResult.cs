using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommandResult
{
    public SaveReportCommandResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public static SaveReportCommandResult Success(IReportRecord command)
    {
        return new SaveReportCommandResult(true) { ReportRecord = command };
    }

    public bool IsSuccess { get; set; } = false;

    public IReportRecord ReportRecord { get; set; }
}