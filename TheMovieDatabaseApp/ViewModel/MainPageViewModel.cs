using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;
using Xamarin.Forms;

namespace TheMovieDatabaseApp.ViewModel
{
    public class MainPageViewModel
    {
        private readonly IMovieDataSource _movieDataSource;

        public ICommand MovieSelectedCommand { get; }

        public TaskRunWrapper<List<Movie>> Movies { get; set; }

        public MainPageViewModel(INavigation navigation) : this(navigation, new MovieDataSource(new MovieFinder("https://api.themoviedb.org/3", "1f54bd990f1cdfb230adb312546d765d"), new GenreFinder("https://api.themoviedb.org/3", "1f54bd990f1cdfb230adb312546d765d"))) { }

        public MainPageViewModel(INavigation navigation, IMovieDataSource movieDataSource)
        {
            _movieDataSource = movieDataSource;
            Movies = new TaskRunWrapper<List<Movie>>(GetMovies());
            MovieSelectedCommand = new Command<Movie>(async m => await navigation.PushAsync(new DetailsPage(m)));
        }

        private async Task<List<Movie>> GetMovies()
        {
            return await _movieDataSource.GetMovies();
        }
    }
}