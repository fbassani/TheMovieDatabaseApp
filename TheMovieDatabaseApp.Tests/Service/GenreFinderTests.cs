using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using NUnit.Framework;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp.Tests.Service
{
    public class GenreFinderTests
    {
        private const string BaseUrl = "http://base.url";
        private const string ApiKey = "abc123";
        private GenreFinder _genreFinder;

        [SetUp]
        public void SetUp()
        {
            _genreFinder = new GenreFinder(BaseUrl, ApiKey);
        }

        [Test]
        public async Task GetAllAsync_ShouldCallApi()
        {
            using (var httpTest = new HttpTest())
            {
                await _genreFinder.GetAllAsync();
                httpTest.ShouldHaveCalled($"{BaseUrl}{GenreFinder.Resource}?api_key={ApiKey}")
                    .WithVerb(HttpMethod.Get);
            }
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfGenreDto()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(new
                {
                    genres = new[]
                    {
                        new {id = 1}
                    }
                });
                var dtos = await _genreFinder.GetAllAsync();
                Assert.AreEqual(1, dtos[0].Id);
            }
        }
    }
}