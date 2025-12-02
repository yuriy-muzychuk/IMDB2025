using AutoMapper;
using IMDB2025.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMDB2025.MVC.App.MappingProfiles
{
    public class GenreListItemProfile : Profile
    {
        public GenreListItemProfile()
        {
            CreateMap<Genre, SelectListItem>()
                .ForMember(dest => dest.Value, src => src.MapFrom(g => g.GenreId))
                .ForMember(dest => dest.Text, src => src.MapFrom(g => g.Name));
        }
    }
}
