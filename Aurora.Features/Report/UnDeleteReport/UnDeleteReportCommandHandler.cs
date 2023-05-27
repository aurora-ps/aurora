using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
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

    public async Task<UnDeleteReportCommandResult> Handle(UnDeleteReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == request.ReportId, cancellationToken);
            if (report == null)
            {
                return UnDeleteReportCommandResult.CreateFailure(request.ReportId, "Report Not Found");
            }


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

public class UnDeleteReportCommand : IRequest<UnDeleteReportCommandResult>
{
    public UnDeleteReportCommand(string reportId)
    {
        ReportId = reportId;
    }
    
    public string ReportId { get; }
}

public class UnDeleteReportCommandResult
{
    private UnDeleteReportCommandResult(string reportId, bool success)
    {
        ReportId = reportId;
        Success = success;
    }

    private UnDeleteReportCommandResult(string reportId, bool success, string errorMessage) : this(reportId, success)
    {
        this.ErrorMessage = errorMessage;
    }

    public string ReportId { get; }

    public bool Success { get; }

    public string ErrorMessage { get; }
    public DateTime? DeletedOnUtc { get; }

    public static UnDeleteReportCommandResult CreateSuccess(string reportId)
    {
        return new UnDeleteReportCommandResult(reportId, true);
    }

    public static UnDeleteReportCommandResult CreateFailure(string reportId, string errorMessage)
    {
        return new UnDeleteReportCommandResult(reportId, false, errorMessage);
    }
}