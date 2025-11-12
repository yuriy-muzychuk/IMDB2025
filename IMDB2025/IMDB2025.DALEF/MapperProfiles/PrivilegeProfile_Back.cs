using AutoMapper;
using IMDB2025.DTO;

namespace IMDB2025.DALEF.MapperProfiles
{
    public class PrivilegeProfile_Back : Profile
    {
        public PrivilegeProfile_Back()
        {
            CreateMap<Privilege, Models.Privilege>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()));
            CreateMap<Models.Privilege, Privilege>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Enum.Parse<PrivilegeType>(src.Name)));
        }
    }
}
