using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using NUnit.Framework;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp.Tests.Service
{
    public class MovieFinderTests
    {
        private const string BaseUrl = "http://base.url";
        private const string ApiKey = "abc123";
        private MovieFinder _movieFinder;

        [SetUp]
        public void SetUp()
        {
            _movieFinder = new MovieFinder(BaseUrl, ApiKey);
        }

        [Test]
        public async Task GetPage_ShouldCallApi()
        {
            using (var httpTest = new HttpTest())
            {
                await _movieFinder.GetPage();
                httpTest.ShouldHaveCalled($"{BaseUrl}{MovieFinder.Resource}?api_key={ApiKey}")
                    .WithVerb(HttpMethod.Get);
            }
        }

        [Test]
        public async Task GetPage_ShouldReturnListOfMovieDto()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(new
                {
                    results = new[]
                    {
                        new {overview = "overview"}
                    }
                });
                var dtos = await _movieFinder.GetPage();
                Assert.AreEqual("overview", dtos[0].Overview);
            }
        }
    }
}