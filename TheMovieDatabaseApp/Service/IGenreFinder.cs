using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheMovieDatabaseApp.Service
{
    public interface IGenreFinder
    {
        Task<List<GenreDto>> GetAllAsync();
    }
}