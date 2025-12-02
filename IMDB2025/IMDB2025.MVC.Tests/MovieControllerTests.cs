using AutoMapper;
using IMDB2025.BL.Interfaces;
using IMDB2025.DTO;
using IMDB2025.MVC.Controllers;
using IMDB2025.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IMDB2025.MVC.Tests
{
    public class MovieControllerTests
    {
        private Mock<IMovieManager> _mockManager;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<MovieController>> _mockLogger;
        private MovieController _controller;

        [SetUp]
        public void Setup()
        {
            _mockManager = new Mock<IMovieManager>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<MovieController>>();
            _controller = new MovieController(_mockManager.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Test]
        public void Index_ReturnsViewWithMovies()
        {
            // Arrange
            var movies = new List<Movie> { new Movie { Title = "Movie1" }, new Movie { Title = "Movie2" } };
            _mockManager.Setup(m => m.GetAllMovies()).Returns(movies);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(movies, result.Model);
        }

        [Test]
        public void Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var model = new EditMovieModel { Title = "Valid Movie", GenreId = 1, ReleaseDate = DateTime.Now };
            var movie = new Movie { Title = "Valid Movie" };
            _mockMapper.Setup(m => m.Map<Movie>(model)).Returns(movie);
            _mockManager.Setup(m => m.CreateMovie(movie)).Returns(movie);

            // Act
            var result = _controller.Create(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void Create_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var model = new EditMovieModel { Title = "", GenreId = 1, ReleaseDate = DateTime.Now };
            _controller.ModelState.AddModelError("Title", "Title is required!");

            // Act
            var result = _controller.Create(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [Test]
        public void Create_ExceptionInManager_ReturnsViewWithError()
        {
            // Arrange
            var model = new EditMovieModel { Title = "Valid Movie", GenreId = 1, ReleaseDate = DateTime.Now };
            var exception = new Exception("Test exception");
            _mockMapper.Setup(m => m.Map<Movie>(model)).Returns(new Movie());
            _mockManager.Setup(m => m.CreateMovie(It.IsAny<Movie>())).Throws(exception);

            // Act
            var result = _controller.Create(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
            Assert.IsTrue(_controller.ModelState.ContainsKey(string.Empty));
            Assert.AreEqual($"An exception has occurred: {exception}", _controller.ModelState[string.Empty].Errors[0].ErrorMessage);
        }


        [Test]
        public void Edit_ExceptionInManager_ReturnsViewWithError()
        {
            // Arrange
            var model = new EditMovieModel { MovieId = 1, Title = "Updated Movie", GenreId = 1, ReleaseDate = DateTime.Now };
            var exception = new Exception("Test exception");
            _mockMapper.Setup(m => m.Map<Movie>(model)).Returns(new Movie());
            _mockManager.Setup(m => m.UpdateMovie(It.IsAny<Movie>())).Throws(exception);

            // Act
            var result = _controller.Edit(1, model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
            Assert.IsTrue(_controller.ModelState.ContainsKey(string.Empty));
            Assert.AreEqual($"An exception has occurred: {exception}", _controller.ModelState[string.Empty].Errors[0].ErrorMessage);
        }


        [Test]
        public void Edit_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var model = new EditMovieModel { MovieId = 1, Title = "Updated Movie", GenreId = 1, ReleaseDate = DateTime.Now };
            var movie = new Movie { Title = "Updated Movie" };
            _mockMapper.Setup(m => m.Map<Movie>(model)).Returns(movie);
            _mockManager.Setup(m => m.UpdateMovie(movie)).Returns(movie);

            // Act
            var result = _controller.Edit(1, model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void Edit_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var model = new EditMovieModel { MovieId = 1, Title = "", GenreId = 1, ReleaseDate = DateTime.Now };
            _controller.ModelState.AddModelError("Title", "Title is required!");

            // Act
            var result = _controller.Edit(1, model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [Test]
        public void Delete_ValidId_RedirectsToIndex()
        {
            // Act
            var result = _controller.Delete(1, null) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}