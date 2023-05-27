using Aurora.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Report.UnDeleteReport;

public class UnDeleteReportCommandHandler : IRequestHandler<UnDeleteReportCommand, UnDeleteReportCommandResult>
{
    private readonly IReportDbContext _context;

    public UnDeleteReportCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<UnDeleteReportCommandResult> Handle(UnDeleteReportCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == request.ReportId, cancellationToken);
            if (report == null) return UnDeleteReportCommandResult.CreateFailure(request.ReportId, "Report Not Found");


            report.DeletedOnUtc = null;
            await _context.SaveChangesAsync(cancellationToken);
            return UnDeleteReportCommandResult.CreateSuccess(request.ReportId);
        }
        catch (Exception)
        {
            return UnDeleteReportCommandResult.CreateFailure(request.ReportId, "Unable to Un-Delete");
        }
    }
}