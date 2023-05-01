using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Report.SaveReport
{
    public class SaveReportCommandHandler : IRequestHandler<SaveReportCommand, SaveReportCommandResult>
    {
        private readonly IClusterClient _clusterClient;

        public SaveReportCommandHandler(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        public async Task<SaveReportCommandResult> Handle(SaveReportCommand command,
            CancellationToken cancellationToken)
        {
            var report = command.ReportRecord;
            if (await this.ReportExists(report.Id))
            {
                await this.UpdateReport(report);
            }
            else
            {
                await this.CreateReport(report);
            }
            
            var reportGrain = _clusterClient.GetGrain<IReportGrain>(report.Id);
            var updatedReport = await reportGrain.GetAsync();
            return new SaveReportCommandResult(true){ReportRecord = updatedReport};
        }

        private async Task<SaveReportCommandResult> CreateReport(ReportRecord command)
        {
            var reportGrain = _clusterClient.GetGrain<IReportGrain>(command.Id);
            await reportGrain.AddOrUpdateAsync(command);
            return SaveReportCommandResult.Success(command);
        }

        private async Task<SaveReportCommandResult> UpdateReport(ReportRecord command)
        {
            var reportGrain = _clusterClient.GetGrain<IReportGrain>(command.Id);
            if (await reportGrain.IsPersistedAsync())
            {
                await reportGrain.AddOrUpdateAsync(command);
                return SaveReportCommandResult.Success(command);
            }

            return new SaveReportCommandResult(false);
        }

        private async Task<bool> ReportExists(string? reportId)
        {
            if (string.IsNullOrEmpty(reportId))
                return false;

            var reportService = _clusterClient.GetGrain<IReportServiceGrain>("");
            return await reportService.ReportExistsAsync(reportId);
        }
    }
}
