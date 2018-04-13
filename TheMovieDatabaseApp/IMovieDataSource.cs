using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieDatabaseApp.Model;

namespace TheMovieDatabaseApp
{
    public interface IMovieDataSource
    {
        Task<List<Movie>> GetMovies();
    }
}