using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
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

            IList<ReportRecord> reports;
            if (!string.IsNullOrEmpty(request.UserId) && !request.ShowAll)
            {
                reports = await reportService.GetUserReportsAsync(request.UserId, request.ShowHidden);
            }
            else
            {
                reports = await reportService.GetAllAsync(request.ShowHidden);
            }
            

            return GetReportsQueryResult.CreateSuccess(reports);
        }
    }
}
