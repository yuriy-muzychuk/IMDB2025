using AutoMapper;
using IMDB2025.DALEF.Models;

namespace IMDB2025.DALEF.MapperProfiles
{
    public class MovieProfile_Back: Profile
    {
        public MovieProfile_Back()
        {
            CreateMap<DTO.Movie, Movie>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.GenreId))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.HasValue ? DateOnly.FromDateTime(src.ReleaseDate.Value) : (DateOnly?)null))
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
                .ForMember(dest => dest.People, opt => opt.MapFrom(src => src.Directors));
            CreateMap<Movie, DTO.Movie>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.HasValue ? src.ReleaseDate.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null))
                .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => src.People))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => new DTO.Genre
                {
                    GenreId = src.Genre.GenreId,
                    Name = src.Genre.Name ?? "N/A"
                }));
        }
    }
}
