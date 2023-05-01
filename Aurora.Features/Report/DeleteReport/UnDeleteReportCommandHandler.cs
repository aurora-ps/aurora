using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.Report.DeleteReport;


public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, DeleteReportCommandResult>
{
    private readonly IClusterClient _clusterClient;

    public DeleteReportCommandHandler(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }

    public async Task<DeleteReportCommandResult> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var reportGrain = _clusterClient.GetGrain<IReportGrain>(request.ReportId);
        var results = await reportGrain.DeleteAsync();

        if (results.HasValue)
        {
            return DeleteReportCommandResult.CreateSuccess(request.ReportId, results.Value);
        }

        return DeleteReportCommandResult.CreateFailure(request.ReportId, "Unable to Delete");
    }
}

public class DeleteReportCommand : IRequest<DeleteReportCommandResult>
{
    public DeleteReportCommand(string reportId)
    {
        ReportId = reportId;
    }
    
    public string ReportId { get; }
}

public class DeleteReportCommandResult
{
    private DeleteReportCommandResult(string reportId, bool success, DateTime? deletedOnUtc)
    {
        ReportId = reportId;
        Success = success;
        DeletedOnUtc = deletedOnUtc;
    }

    private DeleteReportCommandResult(string reportId, bool success, string errorMessage)
    {
        this.ReportId = reportId;
        this.Success = success;
        this.ErrorMessage = errorMessage;
    }

    public string ReportId { get; }

    public bool Success { get; }

    public string ErrorMessage { get; }

    public DateTime? DeletedOnUtc { get; set; }

    public static DeleteReportCommandResult CreateSuccess(string reportId, DateTime deletedOnUtc)
    {
        return new DeleteReportCommandResult(reportId, true, deletedOnUtc);
    }

    public static DeleteReportCommandResult CreateFailure(string reportId, string errorMessage)
    {
        return new DeleteReportCommandResult(reportId, false, errorMessage);
    }
}