using System.Threading.Tasks;
using TheMovieDatabaseApp.Model;

namespace TheMovieDatabaseApp
{
    public interface IMovieDataSource
    {
        Task<MoviesPage> GetMovies(int page);
    }
}