using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp.Tests
{
    public class MovieDataSourceTests
    {
        private Mock<IMovieFinder> _movieFinderMock;
        private Mock<IGenreFinder> _genreFinderMock;
        private MovieDataSource _movieDataSource;

        [SetUp]
        public void SetUp()
        {
            _movieFinderMock = new Mock<IMovieFinder>();
            _movieFinderMock.Setup(m => m.GetPage(It.IsAny<int>())).ReturnsAsync(new List<MovieDto>());
            _genreFinderMock = new Mock<IGenreFinder>();
            _genreFinderMock.Setup(g => g.GetAll()).ReturnsAsync(new List<GenreDto>());
            _movieDataSource = new MovieDataSource(_movieFinderMock.Object, _genreFinderMock.Object);
        }

        [Test]
        public async Task GetMovies_ShouldGetGenres()
        {
            await GetMovies();
            _genreFinderMock.Verify(g => g.GetAll());
        }

        [Test]
        public async Task GetMovies_ShouldGetMovies()
        {
            await GetMovies();
            _movieFinderMock.Verify(g => g.GetPage(1));
        }

        [Test]
        public async Task GetMovies_ShouldReturnListOfMovies()
        {
            var result = await GetMovies();
            Assert.IsInstanceOf<List<Movie>>(result);
        }

        private async Task<List<Movie>> GetMovies()
        {
            return await _movieDataSource.GetMovies(1);
        }
    }
}