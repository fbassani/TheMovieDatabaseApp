using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace TheMovieDatabaseApp.Service
{
    public class MovieFinder : IMovieFinder
    {
        public const string Resource = "/movie/upcoming";

        private readonly string _baseUrl;
        private readonly string _apiKey;

        public MovieFinder(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
        }

        public async Task<MovieResultDto> GetPage(int page = 1)
        {
            var result = await _baseUrl.AppendPathSegment(Resource)
                .SetQueryParams(new { api_key = _apiKey, page})
                .GetJsonAsync<MovieResultDto>();
            return result;
        }

    }
}