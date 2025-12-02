using AutoMapper;
using IMDB2025.BL.Concrete;
using IMDB2025.BL.Interfaces;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DALEF.Concrete;
using IMDB2025.DALEF.MapperProfiles;
using IMDB2025.MVC.App.MappingProfiles;

namespace IMDB2025.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddLog4Net("log4net.config");
            });


            builder.Services.AddSingleton<IMapper>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ConstructServicesUsing(sp.GetService);
                    cfg.AddMaps(typeof(MovieProfile_Back).Assembly, typeof(GenreListItemProfile).Assembly);
                }, loggerFactory);

                return config.CreateMapper();
            });

            string connStr = builder.Configuration.GetConnectionString("IMDB") ?? "";

            // DAL and BL registrations
            builder.Services.AddTransient<IMovieDal>(sp => new MovieDalEf(connStr, sp.GetRequiredService<IMapper>()))
                            .AddTransient<IGenreDal>(sp => new GenreDalEf(connStr, sp.GetRequiredService<IMapper>()))
                            .AddTransient<IUserDal>(sp => new UserDalEf(connStr, sp.GetRequiredService<IMapper>()))
                            .AddTransient<IUserPrivilegeDal>(sp => new UserPrivilegeDalEf(connStr, sp.GetRequiredService<IMapper>()))
                            .AddTransient<IMovieManager, MovieManager>()
                            .AddTransient<IAuthManager, AuthManager>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
