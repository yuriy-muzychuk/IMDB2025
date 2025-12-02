using AutoMapper;
using IMDB2025.BL.Interfaces;
using IMDB2025.DTO;
using IMDB2025.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMDB2025.MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieController> _logger;
        public MovieController(IMovieManager manager, IMapper mapper, ILogger<MovieController> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: MovieController
        public ActionResult Index()
        {
            var movies = _manager.GetAllMovies();
            return View(movies);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private void AddDictionaries(EditMovieModel model)
        {
            model.Genres = _mapper.Map<List<SelectListItem>>(_manager.GetAllGenres());
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            var editMovieModel = new EditMovieModel();
            AddDictionaries(editMovieModel);
            return View(editMovieModel);
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditMovieModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //throw new ArgumentOutOfRangeException("test");

                    var movie = _manager.CreateMovie(_mapper.Map<Movie>(model));
                    return RedirectToAction("Index");
                }
                else
                {
                    AddDictionaries(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception has occurred when creating a movie object. Exception: {ex}", ex);
                ModelState.AddModelError(string.Empty, $"An exception has occurred: {ex}");
                AddDictionaries(model);
                return View(model);
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            var editMovieModel = _manager.GetMovieById(id) is Movie movie
                ? _mapper.Map<EditMovieModel>(movie)
                : new EditMovieModel();
            AddDictionaries(editMovieModel);
            return View(editMovieModel);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditMovieModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //throw new ArgumentOutOfRangeException("test");

                    var movie = _manager.UpdateMovie(_mapper.Map<Movie>(model));
                    return RedirectToAction("Index");
                }
                else
                {
                    model.MovieId = id;
                    AddDictionaries(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception has occurred when updating the movie {movie}. Exception: {ex}", model, ex);
                model.MovieId = id;

                ModelState.AddModelError(string.Empty, $"An exception has occurred: {ex}");
                AddDictionaries(model);
                return View(model);
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
