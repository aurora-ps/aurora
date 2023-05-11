using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Grains;

public class AgencyGrain : Grain, IAgencyGrain
{
    private readonly ReportDbContext _reportContext;
    private readonly IMapper _mapper;

    private AgencyRecord? _state;

    public AgencyGrain(ReportDbContext reportContext, IMapper mapper)
    {
        _reportContext = reportContext;
        _mapper = mapper;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        await RefreshStateFromPersistence(cancellationToken);

        await base.OnActivateAsync(cancellationToken);
    }

    private async Task RefreshStateFromPersistence(CancellationToken cancellationToken)
    {
        _state = await _reportContext
            .Agencies.Select(a => new AgencyRecord
            {
                Id = a.Id,
                Name = a.Name,
                IncidentTypes = a.IncidentTypes.Select(it => new IncidentTypeRecord
                {
                    Id = it.IncidentType.Id,
                    Name = it.IncidentType.Name,
                    CollectLocation = it.CollectLocation ?? it.IncidentType.CollectLocation,
                    CollectPerson = it.CollectPerson ?? it.IncidentType.CollectPerson,
                    CollectTime = it.CollectTime ?? it.IncidentType.CollectTime,
                    RequiresTime = it.RequiresTime ?? it.IncidentType.RequiresTime,
                    ShowGospelPresentations = it.ShowGospelPresentations ?? it.IncidentType.ShowGospelPresentations,
                    ShowProfessionsOfFaith = it.ShowProfessionsOfFaith ?? it.IncidentType.ShowProfessionsOfFaith,
                    ShowBaptisms = it.ShowBaptisms ?? it.IncidentType.ShowBaptisms,
                    ShowBibleStudies = it.ShowBibleStudies ?? it.IncidentType.ShowBibleStudies,
                    ShowCounselingOpportunities =
                        it.ShowCounselingOpportunities ?? it.IncidentType.ShowCounselingOpportunities
                }).OrderBy(_ => _.Name).ToList()
            }).SingleOrDefaultAsync(x => x.Id == this.GetPrimaryKeyString(), cancellationToken);
    }

    public Task<AgencyRecord?> GetDetailsAsync()
    {
        return Task.FromResult(_state);
    }

    public async Task<IList<IncidentTypeRecord>?> GetIncidentTypesAsync()
    {
        if (_state == null)
            return await Task.FromResult<IList<IncidentTypeRecord>?>(null);

        return await Task.FromResult<IList<IncidentTypeRecord>?>(_state.IncidentTypes);
    }

    public Task<bool> RemoveIncidentTypeAsync(IncidentTypeRecord? incidentType)
    {
        if(incidentType == null)
            return Task.FromResult(false);

        if(_state?.IncidentTypes == null)
            return Task.FromResult(false);

        if(_state.IncidentTypes.All(_ => _.Id != incidentType.Id))
            return Task.FromResult(false);
            
        _state.IncidentTypes.Remove(incidentType);
        return Task.FromResult(true);
    }

    public Task<bool> AddIncidentTypeAsync(IncidentTypeRecord? incidentType)
    {
        if(incidentType == null)
            return Task.FromResult(false);

        if(_state == null)
            return Task.FromResult(false);

        _state.IncidentTypes ??= new List<IncidentTypeRecord>();

        // Already exists
        if(_state.IncidentTypes.Any(_ => _.Id == incidentType.Id))
            return Task.FromResult(false);

        _state.IncidentTypes.Add(incidentType);
        return Task.FromResult(true);
    }

    public Task<string> SetAgencyName(string name)
    {
        _state ??= new AgencyRecord
        {
            Id = this.GetPrimaryKeyString()
        };

        _state.Name = name;

        return Task.FromResult(_state.Name);
    }

    public async Task<bool> SaveChangesAsync()
    {
        if(!this.CanSave())
            return false;  

        var existingAgency = _reportContext.Agencies
            .Include(_ => _.IncidentTypes)
            .SingleOrDefault(_ => _.Id == _state!.Id);

        if (existingAgency == null)
        {
            var newAgency = new Agency(this.GetPrimaryKeyString(), _state!.Name)
            {
                IncidentTypes = _state!.IncidentTypes?.Select(_ => new AgencyIncidentType
                                    { AgencyId = this.GetPrimaryKeyString(), IncidentTypeId = _.Id }).ToList() ??
                                new List<AgencyIncidentType>()
            };
            _reportContext.Agencies.Add(newAgency);
            await _reportContext.SaveChangesAsync();
            return true;
        }
        else
        {
            existingAgency.Name = _state!.Name;
            UpdateIncidentTypes(existingAgency);
            await _reportContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public Task UpdateIncidentTypeAsync(IncidentTypeRecord requestIncidentType)
    {
        var agencyIncidentType = _state?.IncidentTypes?.FirstOrDefault(_ => _.Id == requestIncidentType.Id);
        if (agencyIncidentType == null)
            return Task.CompletedTask;

        agencyIncidentType = requestIncidentType;

        return Task.CompletedTask;
    }

    private void UpdateIncidentTypes(Agency existingAgency)
    {
        var existingIncidentTypes = existingAgency.IncidentTypes.Select(_ => _.IncidentTypeId).ToList();

        var incidentTypesToDelete = existingAgency.IncidentTypes.Where(_ => _state!.IncidentTypes?.All(i => i.Id != _.IncidentTypeId) ?? true).ToList();
        var incidentTypesToAdd = _state!.IncidentTypes?.Where(_ => existingIncidentTypes.All(i => i != _.Id)).ToList() ?? new List<IncidentTypeRecord>();
        var incidentTypesToUpdate = _state!.IncidentTypes?.Where(_ => existingIncidentTypes.Any(i => i == _.Id)).ToList() ?? new List<IncidentTypeRecord>();

        foreach (var incidentType in incidentTypesToDelete)
            existingAgency.IncidentTypes.Remove(incidentType);

        foreach (var incidentType in incidentTypesToAdd)
            existingAgency.IncidentTypes.Add(new AgencyIncidentType { AgencyId = this.GetPrimaryKeyString(), IncidentTypeId = incidentType.Id });

        foreach (var incidentTypeRecord in incidentTypesToUpdate)
        {
            var agencyIncidentType = existingAgency.IncidentTypes.FirstOrDefault(_ => _.IncidentTypeId == incidentTypeRecord.Id);
            if (agencyIncidentType != null)
                _mapper.Map(incidentTypeRecord, agencyIncidentType);
        }
    }

    private bool CanSave()
    {
        if(_state == null)
            return false;

        if(string.IsNullOrWhiteSpace(_state.Name))
            return false;

        return true;
    }
}