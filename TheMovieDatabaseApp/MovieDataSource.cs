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

        public async Task<List<Movie>> GetMovies(int page)
        {
            _genres = _genres ?? await _genreFinder.GetAll();
            var movies = await _movieFinder.GetPage(page);
            var result = DtoToModelMapper.Map(movies, _genres);
            return result.ToList();
        }
    }
}