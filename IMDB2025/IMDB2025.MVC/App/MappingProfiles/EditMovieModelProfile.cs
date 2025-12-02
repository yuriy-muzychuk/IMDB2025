using AutoMapper;
using IMDB2025.DTO;
using IMDB2025.MVC.Models;

namespace IMDB2025.MVC.App.MappingProfiles
{
    public class EditMovieModelProfile : Profile
    {
        public EditMovieModelProfile()
        {
            CreateMap<Movie, EditMovieModel>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.GenreId));

            CreateMap<EditMovieModel, Movie>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => new Genre
                {
                    GenreId = src.GenreId,
                    Name = "N/A"
                }));
        }
    }
}
