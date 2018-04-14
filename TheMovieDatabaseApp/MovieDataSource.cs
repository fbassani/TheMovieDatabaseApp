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

        public MovieDataSource(IMovieFinder movieFinder, IGenreFinder genreFinder)
        {
            _movieFinder = movieFinder;
            _genreFinder = genreFinder;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var genres = await _genreFinder.GetAll();
            var movies = await _movieFinder.GetPage();
            var result = DtoToModelMapper.Map(movies, genres);
            return result.ToList();
        }
    }
}