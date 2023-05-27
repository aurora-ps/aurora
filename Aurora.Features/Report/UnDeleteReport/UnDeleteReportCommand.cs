using MediatR;

namespace Aurora.Features.Report.UnDeleteReport;

public class UnDeleteReportCommand : IRequest<UnDeleteReportCommandResult>
{
    public UnDeleteReportCommand(string reportId)
    {
        ReportId = reportId;
    }
    
    public string ReportId { get; }
}