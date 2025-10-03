using AutoMapper;
using IMDB2025.DALEF.Concrete;
using IMDB2025.DALEF.MapperProfiles;
using IMDB2025.DTO;
using Microsoft.Extensions.Logging;

namespace IMDB2025.DAL.Tests
{
    public class MovieDalTests
    {
        private readonly string _connStr = "Data Source=Firefly;Initial Catalog=IMDB2025_Test;Integrated Security=True;Trust Server Certificate=True";
        private MovieDalEf _dal;
        private readonly IMapper _mapper;
        private List<Movie> _movies;
        public MovieDalTests() {
            
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MovieProfile_Back).Assembly), loggerFactory);
            _mapper = mapperConfig.CreateMapper();
        }

        [OneTimeSetUp]
        public void OneSetup()
        {
            _dal = new MovieDalEf(_connStr, _mapper);
            
            _movies = new List<Movie>();
            _movies.Add(_dal.Create(new Movie
            {
                Title = "Test 1",
                Genre = new DTO.Genre { GenreId = 1 },
                ReleaseDate = new DateTime(2001, 1, 1)
            }));
        }

        [OneTimeTearDown]
        public void OneTearDown() 
        {
            foreach (var movie in _movies)
            {
                _dal.Delete(movie.MovieId);
            }
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [TearDown]
        public void Teardown()
        {
            
        }

        [Test]
        public void GetByIdSucceeds()
        {
            //Arrange-Act-Assert

            var movie = _dal.GetById(_movies[0].MovieId);
            Assert.That(movie, Is.Not.Null);
            Assert.That(movie.MovieId, Is.EqualTo(_movies[0].MovieId));
            Assert.That(movie.Title, Is.EqualTo(_movies[0].Title));
        }

        [Test]
        public void GetByIdFails()
        {
            //Arrange-Act-Assert
            var movie = _dal.GetById(-1);
            Assert.That(movie, Is.Null);
        }
    }
}