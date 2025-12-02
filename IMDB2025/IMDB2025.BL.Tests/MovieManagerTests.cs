using IMDB2025.BL.Concrete;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;
using Moq;
using NUnit.Framework;


namespace IMDB2025.BL.Tests
{
    [TestFixture]
    public class MovieManagerTests
    {
        private Mock<IMovieDal> _movieDalMock;
        private Mock<IGenreDal> _genreDalMock;
        private MovieManager _sut;

        [SetUp]
        public void SetUp()
        {
            _movieDalMock = new Mock<IMovieDal>(MockBehavior.Strict);
            _genreDalMock = new Mock<IGenreDal>(MockBehavior.Strict);
            _sut = new MovieManager(_movieDalMock.Object, _genreDalMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _movieDalMock.VerifyAll();
            _genreDalMock.VerifyAll();
        }

        [Test]
        public void GetAllMovies_ShouldReturnMoviesFromDal()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { MovieId = 1, Title = "Die Hard", Genre = new Genre { GenreId = 1, Name = "Action" } },
                new Movie { MovieId = 2, Title = "The Matrix", Genre = new Genre { GenreId = 1, Name = "Action" } }
            };
            _movieDalMock.Setup(m => m.GetAll()).Returns(movies);
            //_movieDalMock.Setup(m => m.GetById(20)).Returns(movies[0]);

            // Act
            var result = _sut.GetAllMovies();

            // Assert
            Assert.That(result, Is.SameAs(movies));
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void CreateMovie_ShouldCallDalAndReturnCreatedInstance()
        {
            // Arrange
            var input = new Movie
            {
                Title = "Alien",
                Genre = new Genre { GenreId = 2, Name = "Sci-Fi" },
                ReleaseDate = new DateTime(1979, 5, 25)
            };
            var created = new Movie
            {
                MovieId = 42,
                Title = input.Title,
                Genre = input.Genre,
                ReleaseDate = input.ReleaseDate
            };
            _movieDalMock.Setup(m => m.Create(input)).Returns(created);

            // Act
            var result = _sut.CreateMovie(input);

            // Assert
            Assert.That(result.MovieId, Is.EqualTo(created.MovieId));
            Assert.That(result.Title, Is.EqualTo(created.Title));
        }

        [Test]
        public void GetMovieById_ShouldReturnMovie_WhenFound()
        {
            // Arrange
            var movie = new Movie { MovieId = 7, Title = "Se7en", Genre = new Genre { GenreId = 3, Name = "Thriller" } };
            _movieDalMock.Setup(m => m.GetById(7)).Returns(movie);

            // Act
            var result = _sut.GetMovieById(7);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Se7en"));
        }

        [Test]
        public void GetMovieById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _movieDalMock.Setup(m => m.GetById(999)).Returns((Movie?)null);

            // Act
            var result = _sut.GetMovieById(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void UpdateMovie_ShouldReturnUpdatedInstance()
        {
            // Arrange
            var original = new Movie
            {
                MovieId = 5,
                Title = "Blade Runner",
                Genre = new Genre { GenreId = 2, Name = "Sci-Fi" },
                ReleaseDate = new DateTime(1982, 6, 25)
            };
            var updated = new Movie
            {
                MovieId = 5,
                Title = "Blade Runner (Final Cut)",
                Genre = original.Genre,
                ReleaseDate = original.ReleaseDate
            };
            _movieDalMock.Setup(m => m.Update(original)).Returns(updated);

            // Act
            var result = _sut.UpdateMovie(original);

            // Assert
            Assert.That(result.Title, Is.EqualTo("Blade Runner (Final Cut)"));
        }

        [Test]
        public void GetAllGenres_ShouldReturnGenresFromDal()
        {
            // Arrange
            var genres = new List<Genre>
            {
                new Genre { GenreId = 1, Name = "Action" },
                new Genre { GenreId = 2, Name = "Sci-Fi" }
            };
            _genreDalMock.Setup(g => g.GetAll()).Returns(genres);

            // Act
            var result = _sut.GetAllGenres();

            // Assert
            Assert.That(result, Is.SameAs(genres));
            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}