using Aurora.Interfaces.Models;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;

namespace Aurora.Interfaces.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Agency, Agency>();
        CreateMap<AgencyRecord, AgencyRecord>();
        CreateMap<Agency, AgencyRecord>()
            .ReverseMap();

        CreateMap<AgencyIncidentType,  AgencyIncidentType>();
        CreateMap<IncidentTypeRecord, IncidentTypeRecord>();
        CreateMap<AgencyIncidentType, IncidentTypeRecord>()
            .ReverseMap();

        CreateMap<IncidentType, IncidentType>();
        CreateMap<IncidentTypeRecord, IncidentTypeRecord>();
        CreateMap<IncidentType, IncidentTypeRecord>()
            .ReverseMap();

        CreateMap<Location, Location>();
        CreateMap<LocationRecord, LocationRecord>();
        CreateMap<Location, LocationRecord>()
            .ReverseMap();

        CreateMap<MinistryOpportunityRecord, MinistryOpportunityRecord>();

        CreateMap<Person, Person>();
            
        CreateMap<PhoneNumber, PhoneNumber>();
        CreateMap<PhoneNumberRecord, PhoneNumberRecord>();
        CreateMap<PhoneNumber, PhoneNumberRecord>()
            .ReverseMap();

        CreateMap<Report, Report>();
        CreateMap<ReportRecord, ReportRecord>();
        CreateMap<Report, ReportRecord>()
            .ForMember(d => d.CreatedByUserId, opt => opt.MapFrom(src => src.ReportUserId))
            .ForMember(d => d.CreatedBy, opt =>  opt.MapFrom(s => s.ReportUser))
            .ForMember(d => d.UserId, opt => opt.MapFrom(src => src.ReportUserId))
            .ForMember(d => d.User, opt => opt.MapFrom(src => src.ReportUser))
            .ReverseMap()
            .ForMember(d => d.Agency, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.CreatedByUser, opt => opt.Ignore())
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.IncidentType, opt => opt.Ignore())
            .ForMember(d => d.ReportId, opt => opt.Ignore())
            .ForMember(d => d.ReportUser, opt => opt.Ignore())
            .ForMember(d => d.People, opt => opt.Ignore())
            ;

        CreateMap<Report, ReportSummaryRecord>()
            .ForMember(d => d.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId))
            .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId))
            .ForMember(d => d.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
            .ForMember(d => d.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(d => d.AgencyId, opt => opt.MapFrom(src => src.AgencyId))
            .ForMember(d => d.AgencyName, opt => opt.MapFrom(src => src.Agency.Name))
            .ForMember(d => d.IncidentTypeId, opt => opt.MapFrom(src => src.IncidentTypeId))
            .ForMember(d => d.IncidentTypeName, opt => opt.MapFrom(src => src.IncidentType.Name))
            .ForMember(d => d.LocationAddress, opt => opt.MapFrom(src => src.Location == null ? "" : src.Location.Address))
            .ForMember(d => d.LocationCity, opt => opt.MapFrom(src => src.Location == null ? "" : src.Location.City))
            .ForMember(d => d.LocationState, opt => opt.MapFrom(src => src.Location == null ? "" : src.Location.State))
            .ForMember(d => d.LocationZip, opt => opt.MapFrom(src => src.Location == null ? "" : src.Location.Zip))
            .ForMember(d => d.DeletedOnUtc, opt => opt.MapFrom(src => src.DeletedOnUtc))
            .ForMember(d => d.CreatedOnUtc, opt => opt.MapFrom(src => src.CreatedOnUtc))
            .ForMember(d => d.ClearedDate, opt => opt.MapFrom(src => src.ClearedDate))
            .ForMember(d => d.State, opt => opt.MapFrom(src => src.State))
            .ForMember(d => d.UserId, opt => opt.MapFrom(src => src.ReportUserId))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.ReportUser.UserName))
            .ForMember(d => d.CreatedByDisplay, opt => opt.MapFrom(src => $"{src.CreatedByUser.LastName}, {src.CreatedByUser.FirstName}"))
            .ForMember(d => d.ReportUserDisplay, opt => opt.MapFrom(src => $"{src.ReportUser.LastName}, {src.ReportUser.FirstName}"))
            ;

        CreateMap<ReportPerson, ReportPerson>();
        CreateMap<ReportPersonRecord, ReportPersonRecord>();
        CreateMap<ReportPerson, ReportPersonRecord>()
            .ReverseMap()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));

        CreateMap<AuroraUser, AuroraUser>();
        CreateMap<UserRecord, UserRecord>();
        CreateMap<AuroraUser, UserRecord>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.UserName))
            .ReverseMap()
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Reports, opt => opt.Ignore());
    }
}