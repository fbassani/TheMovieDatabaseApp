using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace TheMovieDatabaseApp.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IMovieDataSource _movieDataSource;

        private int _currentPage = 1;


        bool _isLoadingMore;
        bool IsLoadingMore
        {
            get
            {
                return _isLoadingMore;
            }
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }

        public ICommand MovieSelectedCommand { get; }

        public InfiniteScrollCollection<Movie> Movies { get; set; }


        public MainPageViewModel(INavigation navigation) : this(navigation, new MovieDataSource(new MovieFinder("https://api.themoviedb.org/3", "1f54bd990f1cdfb230adb312546d765d"), new GenreFinder("https://api.themoviedb.org/3", "1f54bd990f1cdfb230adb312546d765d"))) { }

        public MainPageViewModel(INavigation navigation, IMovieDataSource movieDataSource)
        {
            _movieDataSource = movieDataSource;
            MovieSelectedCommand = new Command<Movie>(async m => await navigation.PushAsync(new DetailsPage(m)));
            Movies = new InfiniteScrollCollection<Movie>
            {
                OnLoadMore = async () =>
                {
                    IsLoadingMore = true;
                    var movies = await GetMovies();
                    IsLoadingMore = false;
                    _currentPage++;
                    return movies;
                }
            };
            Movies.LoadMoreAsync();
        }

        private async Task<List<Movie>> GetMovies()
        {
            return await _movieDataSource.GetMovies(_currentPage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}