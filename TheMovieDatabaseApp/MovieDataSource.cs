using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp
{
    public class MovieDataSource : IMovieDataSource
    {
        private readonly IMovieFinder _movieFinder;
        private readonly IGenreFinder _genreFinder;
        private List<GenreDto> _genres;


        public MovieDataSource(IMovieFinder movieFinder, IGenreFinder genreFinder)
        {
            _movieFinder = movieFinder;
            _genreFinder = genreFinder;
        }

        public async Task<MoviesPage> GetMovies(int page)
        {
            _genres = _genres ?? await _genreFinder.GetAll();
            var moviesResult = await _movieFinder.GetPage(page);
            var movies =  DtoToModelMapper.Map(moviesResult.Results, _genres);
            return new MoviesPage {
                TotalPages = moviesResult.Total_Pages,
                Movies = movies
            };
        }
    }
}