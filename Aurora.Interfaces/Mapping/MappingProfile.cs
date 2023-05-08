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

        CreateMap<ReportPerson, ReportPerson>();
        CreateMap<ReportPersonRecord, ReportPersonRecord>();
        CreateMap<ReportPerson, ReportPersonRecord>()
            .ReverseMap()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));

        CreateMap<AuroraUser, AuroraUser>();
        CreateMap<UserRecord, UserRecord>();
        CreateMap<AuroraUser, UserRecord>()
            .ReverseMap()
            .ForMember(d => d.Reports, opt => opt.Ignore());
    }
}