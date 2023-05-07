using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Interfaces.Models;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;

namespace Aurora.Interfaces.Mapping
{
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
                .ReverseMap();

            CreateMap<ReportPerson, ReportPerson>();
            CreateMap<ReportPersonRecord, ReportPersonRecord>();
            CreateMap<ReportPerson, ReportPersonRecord>()
                .ReverseMap();

            CreateMap<AuroraUser, AuroraUser>();
            CreateMap<UserRecord, UserRecord>();
            CreateMap<AuroraUser, UserRecord>()
                .ReverseMap();
        }
    }
}
