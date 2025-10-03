using AutoMapper;

namespace IMDB2025.DALEF.MapperProfiles
{
    public class PersonProfile_Back : Profile
    {
        public PersonProfile_Back()
        {
            CreateMap<DTO.Person, Models.Person>()
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MoviesDirected))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Roles));
            CreateMap<Models.Person, DTO.Person>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Actors))
                .ForMember(dest => dest.MoviesDirected, opt => opt.MapFrom(src => src.Movies));
        }
    }
}
