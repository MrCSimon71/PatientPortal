using AutoMapper;
using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Api.DTOs;

namespace PDDS.PatientData.Api.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .AfterMap((src, dest) => dest.Password = null);

            CreateMap<UserDto, User>();
        }
    }
}
