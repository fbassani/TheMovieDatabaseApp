using System.Collections.ObjectModel;
using System.Windows.Input;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;
using Xamarin.Forms;

namespace TheMovieDatabaseApp.ViewModel
{
    public class MainPageViewModel
    {
        public ICommand MovieSelectedCommand { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }

        public MainPageViewModel(INavigation navigation)
        {
            var datasource = new MovieDataSource(new MovieFinder());
            Movies = new ObservableCollection<Movie>(datasource.GetMovies());
            MovieSelectedCommand = new Command<Movie>(async m => await navigation.PushAsync(new DetailsPage(m)));
        }
    }
}