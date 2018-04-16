using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace TheMovieDatabaseApp.ViewModel
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IMovieDataSource _movieDataSource;

        private int _currentPage = 1;
        private int _totalPages = 1;
        private bool _isLoadingMore;
        private bool _hasError;

        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }

        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool NetworkUnavailable => !App.IsNetworkAvailabe();

        public bool NetworkAvailable => !NetworkUnavailable;

        public ICommand MovieSelectedCommand { get; }

        public InfiniteScrollCollection<Movie> Movies { get; set; }

        //TODO: use some dependency injection container 
        public MainPageViewModel(INavigation navigation) : this(navigation, new MovieDataSource(new MovieFinder(Settings.ApiBaseUrl, Settings.ApiKey), new GenreFinder(Settings.ApiBaseUrl, Settings.ApiKey))) { }

        public MainPageViewModel(INavigation navigation, IMovieDataSource movieDataSource)
        {
            _movieDataSource = movieDataSource;
            MovieSelectedCommand = new Command<Movie>(async m => await navigation.PushAsync(new DetailsPage(m)));
            Movies = new InfiniteScrollCollection<Movie>
            {
                OnLoadMore = OnLoadMore,
                OnError = OnError,
                OnCanLoadMore = OnCanLoadMore,
                OnBeforeLoadMore = OnBeforeLoadMore,
                OnAfterLoadMore = OnAfterLoadMore
            };
            Movies.LoadMoreAsync();
        }

        private async Task<MoviesPage> GetMoviesAsync()
        {
            return await _movieDataSource.GetMoviesAsync(_currentPage);
        }

        public async Task<IEnumerable<Movie>> OnLoadMore()
        {
            var movies = await GetMoviesAsync();
            _totalPages = movies.TotalPages;
            _currentPage++;
            return movies.Movies;
        }

        public void OnBeforeLoadMore()
        {
            IsLoadingMore = true;
        }

        public void OnAfterLoadMore()
        {
            IsLoadingMore = false;
            HasError = false;
        }

        public void OnError(Exception exception)
        {
            IsLoadingMore = false;
            HasError = true;
        }

        public bool OnCanLoadMore()
        {
            return NetworkAvailable && _currentPage <= _totalPages;
        }
    }
}