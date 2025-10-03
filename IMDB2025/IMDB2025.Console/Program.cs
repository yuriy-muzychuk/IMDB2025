namespace IMDB2025.Console
{
    using AutoMapper;
    using IMDB2025.DAL.Concrete;
    using IMDB2025.DALEF.Concrete;
    using IMDB2025.DALEF.MapperProfiles;
    using IMDB2025.DTO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;

    internal class Program
    {
        static string _connectionString;
        static IMapper _mapper;
        static ILogger<Program> _logger;

        static void Main(string[] args)
        {
            /*Console.WriteLine("Welcome to IMDB!");
            char c = 's';

            while (c != 'q' && c != 'Q')
            {
                switch (c)
                {
                    case '1':
                        Console.WriteLine("You chose to get all Movies.");

                        GetAllMovies();
                        break;
                    case '2':
                        Console.WriteLine("You chose to get all Genres.");
                        break;
                    case '3':
                        Console.WriteLine("You chose to insert a movie.");

                        InsertMovie();
                        break;
                    case '4':
                        Console.WriteLine("You chose to delete a movie.");
                        DeleteMovie();
                        break;
                    case 'q':
                        Console.WriteLine("You chose to Quit.");
                        break;
                    default:
                        if (c != 's')
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                        break;
                }

                Console.WriteLine("\nType:\n1 to get all Movies;\n2 to get all Genres;\n3 to insert a movie;\n4 to delete a movie;\nq to Quit.");

                c = Console.ReadLine()[0];
            }*/

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            _connectionString = configuration.GetConnectionString("IMDB") ?? "";

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
            });

            _logger = loggerFactory.CreateLogger<Program>();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(MovieProfile_Back).Assembly), loggerFactory);
            _mapper = config.CreateMapper();
            TestMapping();
        }

        private static void TestMapping()
        {
            var dal = new MovieDalEf(_connectionString, _mapper);
            var dieHard = dal.GetById(4);

            PrintMovie(dieHard);
        }

        private static void DeleteMovie()
        {
            Console.WriteLine("Enter the MovieId to delete:");

            if (int.TryParse(Console.ReadLine(), out int movieId))
            {
                var dal = new MovieDal(_connectionString);
                bool success = false;
                try
                {
                    success = dal.Delete(movieId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                if (success)
                {
                    Console.WriteLine($"Movie with ID {movieId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Movie with ID {movieId} was not deleted.");
                }
            }
            else
            {
                Console.WriteLine("Invalid MovieId. Please enter a valid integer.");
            }
        }

        private static void InsertMovie()
        {
            var dal = new MovieDalEf(_connectionString, _mapper);

            var oldMovie = new Movie
            {
                Title = "Avatar: The Way of Water",
                Genre = new Genre { GenreId = 3 },
                ReleaseDate = new DateTime(2022, 12, 16)
            };

            var newMovie = dal.Create(oldMovie);

            PrintMovie(newMovie);
        }

        private static void PrintMovie(Movie movie)
        {
            //_logger.LogInformation("{Movie}", movie);
            Console.WriteLine(movie);
        }

        private static void GetAllMovies()
        {
            var dal = new MovieDalEf(_connectionString, _mapper);
            var movies = dal.GetAll();

            foreach (var movie in movies)
            {
                PrintMovie(movie);
            }
        }
    }
}
