using Aurora.Interfaces;
using MediatR;

namespace Aurora.Features.Report.GetReports
{
    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, GetReportsQueryResult>
    {
        private readonly IClusterClient _clusterClient;

        public GetReportsQueryHandler(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        public async Task<GetReportsQueryResult> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            var reportService = _clusterClient.GetGrain<IReportServiceGrain>("");
            var reports = await reportService.GetAllAsync();

            return GetReportsQueryResult.CreateSuccess(reports);
        }
    }
}
