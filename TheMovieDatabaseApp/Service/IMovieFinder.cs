using System.Threading.Tasks;

namespace TheMovieDatabaseApp.Service
{
    public interface IMovieFinder
    {
        Task<MovieResultDto> GetPageAsync(int page);
    }
}