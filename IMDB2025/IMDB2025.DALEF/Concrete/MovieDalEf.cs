using AutoMapper;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Concrete
{
    public class MovieDalEf : IMovieDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;

        public MovieDalEf(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public Movie Create(Movie movie)
        {
            using (var context = new ImdbContext(_connStr))
            {
                var entity = new Models.Movie
                {
                    Title = movie.Title,
                    GenreId = movie.Genre.GenreId,
                    ReleaseDate = movie.ReleaseDate.HasValue ? DateOnly.FromDateTime(movie.ReleaseDate.Value) : (DateOnly?)null
                };
                context.Movies.Add(entity);
                context.SaveChanges();
                movie.MovieId = entity.MovieId;
                return movie;
            }
        }

        public bool Delete(int movieId)
        {
            using (var context = new ImdbContext(_connStr))
            {
                var entity = context.Movies.FirstOrDefault(m => m.MovieId == movieId);
                if (entity == null) return false;
                context.Movies.Remove(entity);
                int affectedRows = context.SaveChanges();
                return affectedRows == 1;
            }
        }

        public List<Movie> GetAll()
        {
            using(var context = new ImdbContext(_connStr))
            {
                return context.Movies.Select(m => 
                new Movie 
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate.HasValue ? m.ReleaseDate.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                    Genre = new Genre 
                    {
                        GenreId = m.Genre.GenreId,
                        Name = m.Genre.Name
                    }
                }).ToList();
            }
        }

        public Movie? GetById(int movieId)
        {
            using(var context = new ImdbContext(_connStr))
            {
                var m = context.Movies
                    .Include(m => m.Genre)
                    .Include(m => m.Actors)
                    .ThenInclude(a => a.Person)
                    .Include(m => m.People)
                    .FirstOrDefault(m => m.MovieId == movieId);
                if (m == null) return null;
                return _mapper.Map<Movie>(m);
            }
        }

        public Movie Update(Movie movie)
        {
            using(var context = new ImdbContext(_connStr))
            {
                //var movieInDB = context.TblMovies.Update(_mapper.Map<TblMovie>(movie));

                var entity = context.Movies.FirstOrDefault(m => m.MovieId == movie.MovieId);
                if (entity == null)
                {
                    entity = _mapper.Map<Models.Movie>(movie);
                    context.Movies.Add(entity);
                }
                else
                {
                    _mapper.Map(movie, entity);
                }
                context.SaveChanges();
                return _mapper.Map<Movie>(entity);
            }
        }
    }
}
