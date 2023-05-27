using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommand : IRequest<SaveReportCommandResult>
{
    public ReportRecord ReportRecord { get; set; }

    public static SaveReportCommand Create(ReportRecord reportRecord)
    {
        return new SaveReportCommand
        {
            ReportRecord = reportRecord
        };
    }
}