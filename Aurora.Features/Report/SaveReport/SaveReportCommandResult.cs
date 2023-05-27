using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommandResult
{
    public SaveReportCommandResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }

    public IReportRecord ReportRecord { get; set; }

    public static SaveReportCommandResult Success(IReportRecord command)
    {
        return new SaveReportCommandResult(true) { ReportRecord = command };
    }
}