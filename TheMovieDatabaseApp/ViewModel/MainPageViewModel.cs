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

        bool _isLoadingMore;

        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }

        private bool _hasError;

        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged(nameof(HasError));
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
                    _totalPages = movies.TotalPages;
                    IsLoadingMore = false;
                    HasError = false;
                    _currentPage++;
                    return movies.Movies;
                },
                OnError = HandleError,
                OnCanLoadMore = () => _currentPage <= _totalPages
            };
            Movies.LoadMoreAsync();
        }

        private async Task<MoviesPage> GetMovies()
        {
            return await _movieDataSource.GetMovies(_currentPage);
        }

        private void HandleError(Exception exception)
        {
            IsLoadingMore = false;
            HasError = true;
        }


    }
}