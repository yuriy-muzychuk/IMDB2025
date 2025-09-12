namespace IMDB2025.Console
{
    using IMDB2025.DAL.Concrete;
    using IMDB2025.DTO;
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to IMDB!");
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

                Console.WriteLine("\nType:\n1 to get all Movies;\n2 to get all Genres;\n3 to insert a movie;\nq to Quit.");

                c = Console.ReadLine()[0];
            }
        }

        private static void InsertMovie()
        {
            var dal = new MovieDal();

            var oldMovie = new Movie
            {
                Title = "Avatar: The Way of Water",
                Genre = new Genre { GenreId = 3 },
                ReleaseDate = new DateTime(2022, 12, 16)
            };

            var newMovie = dal.Create(oldMovie);

            Console.WriteLine($"Inserted Movie: {newMovie.MovieId}: {newMovie.Title} - {newMovie.Genre.Name} - {newMovie.ReleaseDate.ToShortDateString()}");
        }

        private static void GetAllMovies()
        {
            var dal = new MovieDal();
            var movies = dal.GetAll();

            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.MovieId}: {movie.Title} - {movie.Genre.Name} - {movie.ReleaseDate.ToShortDateString()}");
            }
        }
    }
}
