using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.Report.UnDeleteReport;


public class UnDeleteReportCommandHandler : IRequestHandler<UnDeleteReportCommand, UnDeleteReportCommandResult>
{
    private readonly IClusterClient _clusterClient;

    public UnDeleteReportCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<UnDeleteReportCommandResult> Handle(UnDeleteReportCommand request, CancellationToken cancellationToken)
    {
        var reportGrain = _clusterClient.GetGrain<IReportGrain>(request.ReportId);
        var report = await reportGrain.UnDeleteAsync();

        if (report)
        {
            return UnDeleteReportCommandResult.CreateSuccess(request.ReportId);
        }

        return UnDeleteReportCommandResult.CreateFailure(request.ReportId, "Unable to Un-Delete");
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