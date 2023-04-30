﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<SaveReportCommandResult> Handle(SaveReportCommand command, CancellationToken cancellationToken)
        {
            if (await this.ReportExists(command.Id))
            {
                return await this.UpdateReport(command);
            }

            return await this.CreateReport(command);
        }

        private async Task<SaveReportCommandResult> CreateReport(SaveReportCommand command)
        {
            var reportGrain = _clusterClient.GetGrain<IReportGrain>(command.Id);
            var reportRecord = new ReportRecord
            {
                Id = command.Id,
                UserId = command.UserId,
                Agency = command.Agency,
                Date = command.Date,
                IncidentType = command.IncidentType,
                Location = command.Location,
                Miles = command.Miles,
                Narrative = command.Narrative,
                People = command.People,
                Time = command.Time
            };

            await reportGrain.AddOrUpdateAsync(reportRecord);
            return SaveReportCommandResult.Success(command);
        }

        private async Task<SaveReportCommandResult> UpdateReport(SaveReportCommand command)
        {
            var reportGrain = _clusterClient.GetGrain<IReportGrain>(command.Id);
            if (await reportGrain.IsPersistedAsync())
            {
                var reportRecord = new ReportRecord
                {
                    Id = command.Id,
                    UserId = command.UserId,
                    Agency = command.Agency,
                    Date = command.Date,
                    IncidentType = command.IncidentType,
                    Location = command.Location,
                    Miles = command.Miles,
                    Narrative = command.Narrative,
                    People = command.People,
                    Time = command.Time
                };

                await reportGrain.AddOrUpdateAsync(reportRecord);
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
