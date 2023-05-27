using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommandHandler : IRequestHandler<SaveReportCommand, SaveReportCommandResult>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public SaveReportCommandHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SaveReportCommandResult> Handle(SaveReportCommand command,
        CancellationToken cancellationToken)
    {
        if (command?.ReportRecord == null)
            throw new ArgumentNullException(nameof(command.ReportRecord));

        if (await ReportExists(command.ReportRecord.Id))
            return await UpdateReport(command.ReportRecord, cancellationToken);

        return await CreateReport(command.ReportRecord, cancellationToken);
    }

    private async Task<SaveReportCommandResult> CreateReport(ReportRecord command, CancellationToken cancellationToken)
    {
        await AddOrUpdateAsync(command, cancellationToken);
        return SaveReportCommandResult.Success(command);
    }

    private async Task<SaveReportCommandResult> UpdateReport(ReportRecord command, CancellationToken cancellationToken)
    {
        await AddOrUpdateAsync(command, cancellationToken);
        return SaveReportCommandResult.Success(command);
    }

    private async Task<bool> ReportExists(string? reportId)
    {
        if (string.IsNullOrEmpty(reportId))
            return false;

        return await _context.Reports.AnyAsync(_ => _.Id == reportId);
    }

    private async Task<bool> AddOrUpdateAsync(ReportRecord record, CancellationToken cancellationToken)
    {
        var report = await GetRecordForUpdateAsync(record);

        if (report != null)
        {
            _mapper.Map(record, report);
            UpdateReportPeople(record, report);
        }
        else
        {
            report = _mapper.Map<Interfaces.Models.Reporting.Report>(record);
            report.Id = record.Id;
            UpdateReportPeople(record, report);

            await _context.Reports.AddAsync(report, cancellationToken);
        }

        var results = await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task<Interfaces.Models.Reporting.Report?> GetRecordForUpdateAsync(ReportRecord record)
    {
        return await _context.Reports
            .Include(r => r.Agency)
            .Include(r => r.IncidentType)
            .Include(r => r.Location)
            .Include(r => r.MinistryOpportunity)
            .Include(r => r.People).ThenInclude(p => p.Location)
            .Include(r => r.People).ThenInclude(p => p.PhoneNumber)
            .FirstOrDefaultAsync(_ => _.Id == record.Id);
    }

    private void UpdateReportPeople(ReportRecord record, Interfaces.Models.Reporting.Report report)
    {
        var people = record.People.ToList();
        var peopleToDelete = report.People.Where(_ => people.All(p => p.Id != _.Id)).ToList();
        var peopleToAdd = people.Where(_ => report.People.All(p => p.Id != _.Id)).ToList();
        var peopleToUpdate = people.Where(_ => report.People.Any(p => p.Id == _.Id)).ToList();

        foreach (var person in peopleToDelete)
            report.People.Remove(person);

        foreach (var person in peopleToAdd)
            report.People.Add(_mapper.Map<ReportPerson>(person));

        foreach (var person in peopleToUpdate)
        {
            var reportPerson = report.People.FirstOrDefault(_ => _.Id == person.Id);
            if (reportPerson != null)
                _mapper.Map(person, reportPerson);
        }
    }
}