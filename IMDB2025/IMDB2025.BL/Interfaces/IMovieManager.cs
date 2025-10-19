using IMDB2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB2025.BL.Interfaces
{
    public interface IMovieManager
    {
        List<Movie> GetAllMovies();
    }
}
