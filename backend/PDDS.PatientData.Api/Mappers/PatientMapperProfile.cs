using AutoMapper;
using PDDS.PatientData.Api.DTOs;
using PDDS.PatientData.Core.Entities;

namespace PDDS.PatientData.Api.Mappers
{
    public class PatientMapperProfile : Profile
    {
        public PatientMapperProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(entity => entity.DateOfBirth.Value.ToString("MM/dd/yyyy")));

            CreateMap<PatientDto, Patient>();
        }
    }
}
