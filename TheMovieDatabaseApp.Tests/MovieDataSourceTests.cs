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
            _movieFinderMock.Setup(m => m.GetPageAsync(It.IsAny<int>())).ReturnsAsync(new MovieResultDto { Results = new List<MovieDto>()});
            _genreFinderMock = new Mock<IGenreFinder>();
            _genreFinderMock.Setup(g => g.GetAllAsync()).ReturnsAsync(new List<GenreDto>());
            _movieDataSource = new MovieDataSource(_movieFinderMock.Object, _genreFinderMock.Object);
        }

        [Test]
        public async Task GetMovies_ShouldGetGenres()
        {
            await GetMoviesAsync();
            _genreFinderMock.Verify(g => g.GetAllAsync());
        }

        [Test]
        public async Task GetMovies_ShouldGetMovies()
        {
            await GetMoviesAsync();
            _movieFinderMock.Verify(g => g.GetPageAsync(1));
        }

        [Test]
        public async Task GetMovies_ShouldReturnMoviesPage()
        {
            var result = await GetMoviesAsync();
            Assert.IsInstanceOf<MoviesPage>(result);
        }

        private async Task<MoviesPage> GetMoviesAsync()
        {
            return await _movieDataSource.GetMoviesAsync(1);
        }
    }
}