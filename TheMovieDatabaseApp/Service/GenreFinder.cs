using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace TheMovieDatabaseApp.Service
{
    public class GenreFinder : IGenreFinder
    {
        public const string Resource = "/genre/movie/list";
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public GenreFinder(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
        }

        public async Task<List<GenreDto>> GetAllAsync()
        {
            var result =  await _baseUrl.AppendPathSegment(Resource)
                 .SetQueryParams(new { api_key = _apiKey })
                 .GetJsonAsync<GenreResultDto>();
            return result?.Genres ?? new List<GenreDto>();
        }
    }
}