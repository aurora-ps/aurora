using Aurora.Grains;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Report.GetReport
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, GetReportQueryResult>
    {
        private readonly IClusterClient _clusterClient;

        public GetReportQueryHandler(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        public async Task<GetReportQueryResult> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var reportGrain = _clusterClient.GetGrain<IReportGrain>(request.ReportId);
            var report = await reportGrain.GetAsync();
            if (report == null)
            {
                return GetReportQueryResult.CreateNotFound();
            }
            return GetReportQueryResult.CreateSuccess(report);
        }
    }

    public class GetReportQueryResult
    {
        public GetReportQueryResult(bool success, ReportRecord? record)
        {
            this.Success = success;
            this.Report = record;
        }

        public ReportRecord? Report { get; set; }

        public bool Success { get; set; }

        public static GetReportQueryResult CreateSuccess(ReportRecord report)
        {
            return new GetReportQueryResult(true, report);
        }

        public static GetReportQueryResult CreateNotFound()
        {
            return new GetReportQueryResult(false, null);
        }
    }

    public class GetReportQuery : IRequest<GetReportQueryResult>
    {
        public GetReportQuery(string reportId)
        {
            this.ReportId = reportId;
        }

        public string ReportId { get; set; }
    }
}
