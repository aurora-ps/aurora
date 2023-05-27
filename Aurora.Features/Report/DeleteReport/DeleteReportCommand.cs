using MediatR;

namespace Aurora.Features.Report.DeleteReport;

public class DeleteReportCommand : IRequest<DeleteReportCommandResult>
{
    public DeleteReportCommand(string reportId)
    {
        ReportId = reportId;
    }

    public string ReportId { get; }
}