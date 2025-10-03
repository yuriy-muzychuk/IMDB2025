using AutoMapper;

namespace IMDB2025.DALEF.MapperProfiles
{
    public class GenreProfile_Back: Profile
    {
        public GenreProfile_Back()
        {
            CreateMap<DTO.Genre, Models.Genre>().ReverseMap();
        }
    }
}
