using IMDB2025.WPF.Windows;
using System.Configuration;
using System.Data;
using System.Windows;

namespace IMDB2025.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var main = new MovieList();
            Current.MainWindow = main;
            main.Show();
        }
    }
}
