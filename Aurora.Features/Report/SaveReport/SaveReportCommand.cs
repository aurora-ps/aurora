using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommand : IRequest<SaveReportCommandResult>
{
    public static SaveReportCommand Create(ReportRecord reportRecord)
    {
        return new SaveReportCommand()
        {
            ReportRecord = reportRecord
        };
    }

    public ReportRecord ReportRecord { get; set; }
}