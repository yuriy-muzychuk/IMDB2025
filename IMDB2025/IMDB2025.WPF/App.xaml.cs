using AutoMapper;
using IMDB2025.BL.Concrete;
using IMDB2025.BL.Interfaces;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DALEF.Concrete;
using IMDB2025.DALEF.MapperProfiles;
using IMDB2025.WPF.ViewModels;
using IMDB2025.WPF.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;

namespace IMDB2025.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Configure DI container
            var services = new ServiceCollection();

            // Logging
            services.AddLogging(builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Information);
            });

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<IMapper>(sp =>
            {
                // Ensure logger factory is available for MapperConfiguration constructor (some AutoMapper setups expect it)
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                var config = new MapperConfiguration(cfg =>
                {
                    // Allow AutoMapper to construct profile instances from DI so profiles can receive ILoggerFactory / ILogger<T>
                    cfg.ConstructServicesUsing(sp.GetService);

                    // Load profiles from the DAL EF mapper assembly
                    cfg.AddMaps(typeof(MovieProfile_Back).Assembly);
                }, loggerFactory);

                // Create IMapper that will use the service provider to resolve services when mapping
                return config.CreateMapper();
            });

            string connStr = configuration.GetConnectionString("IMDB") ?? "";

            // DAL and BL registrations
            services.AddTransient<IMovieDal>(sp => new MovieDalEf(connStr, sp.GetRequiredService<IMapper>()));
            services.AddTransient<IGenreDal>(sp => new GenreDalEf(connStr, sp.GetRequiredService<IMapper>()));
            
            
            services.AddTransient<IMovieManager, MovieManager>();

            // Register windows so they can be resolved with DI
            services.AddTransient<MovieListViewModel>();
            services.AddTransient<MovieDetailsSimpleViewModel>();

            services.AddTransient<MovieListMVVM>();
            services.AddTransient<MovieList>();
            services.AddTransient<MovieDetailsSimple>();

            Services = services.BuildServiceProvider();

            var main = Services.GetRequiredService<MovieListMVVM>();
            Current.MainWindow = main;
            main.Show();
        }
    }
}
