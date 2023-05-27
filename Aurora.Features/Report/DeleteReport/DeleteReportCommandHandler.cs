using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Report.DeleteReport;


public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, DeleteReportCommandResult>
{
    private readonly IReportDbContext _context;

    public DeleteReportCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteReportCommandResult> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        if (request?.ReportId == null)
            throw new ArgumentNullException(nameof(request.ReportId));

        var report = await _context.Reports.FirstOrDefaultAsync(x => x.Id == request.ReportId, cancellationToken);

        if (report == null)
            return DeleteReportCommandResult.CreateFailure(request.ReportId, "Report Not Found");

        if(report.DeletedOnUtc.HasValue)
            return DeleteReportCommandResult.CreateFailure(request.ReportId, "Report Already Deleted");

        try
        {
            report.DeletedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);

            return DeleteReportCommandResult.CreateSuccess(request.ReportId, report.DeletedOnUtc.Value);
        }
        catch (Exception ex)
        {
            return DeleteReportCommandResult.CreateFailure(request.ReportId, $"Unable to Delete - {ex.Message}");
        }
    }
}