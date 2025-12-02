using IMDB2025.MVC.App.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMDB2025.MVC.Models
{
    public class EditMovieModel
    {
        public EditMovieModel()
        {
            Title = string.Empty;

            Genres = new List<SelectListItem>();

            ReleaseDate = DateTime.Now;
        }

        public int MovieId { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title length should be between 5 and 100!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Genre is required!")]
        [DisplayName("Genre")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Release date is required!")]
        [DisplayName("Release Date")]
        [DateRange("1900-01-01", "2100-12-31")]
        public DateTime ReleaseDate { get; set; }

        public List<SelectListItem> Genres { get; set; }
    }
}
