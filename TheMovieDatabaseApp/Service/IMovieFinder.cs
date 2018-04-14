using System.Threading.Tasks;

namespace TheMovieDatabaseApp.Service
{
    public interface IMovieFinder
    {
        Task<MovieResultDto> GetPage(int page = 0);
    }
}