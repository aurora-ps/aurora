using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Report.SaveReport
{
    public class SaveReportCommandHandler : IRequestHandler<SaveReportCommand, SaveReportCommandResult>
    {
        public async Task<SaveReportCommandResult> Handle(SaveReportCommand command, CancellationToken cancellationToken)
        {
            return SaveReportCommandResult.Success(command);
        }
    }

    public class SaveReportCommand : IRequest<SaveReportCommandResult>, IReportRecord
    {
        public string Id { get; init; }
        public DateTime? Date { get; init; }
        public TimeSpan? Time { get; init; }
        public AgencyRecord Agency { get; init; }
        public IncidentTypeRecord IncidentType { get; init; }
        public double? Miles { get; init; }
        public LocationRecord Location { get; init; }
        public string Narrative { get; init; }
        public IList<ReportPersonRecord> People { get; init; }
    }

    public class SaveReportCommandValidator
    {
    }

    public class SaveReportCommandResult
    {
        public SaveReportCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static SaveReportCommandResult Success(SaveReportCommand command)
        {
            return new SaveReportCommandResult(true) { ReportRecord = command };
        }

        public bool IsSuccess { get; set; } = false;

        public SaveReportCommand ReportRecord { get; set; }
    }
}
