using System;
using System.Linq;
using NUnit.Framework;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp.Tests
{
    public class DtoToModelMapperTests
    {
        private MovieDto[] _movies;
        private GenreDto[] _genres;
        private Movie _mappedMovie;

        [SetUp]
        public void SetUp()
        {
            _movies = CreateMovies();
            _genres = CreateGenres(); ;
            _mappedMovie = DtoToModelMapper.Map(_movies, _genres).First();
        }

        [Test]
        public void Map_ShouldMapBackdropUrl()
        {
            StringAssert.EndsWith(GetMovieProperty(m => m.Backdrop_Path), _mappedMovie.BackdropUrl);
        }

        [Test]
        public void Map_ShouldMapPosterUrl()
        {
            StringAssert.EndsWith(GetMovieProperty(m => m.Poster_Path), _mappedMovie.PosterUrl);
        }

        [Test]
        public void Map_ShouldMapOverview()
        {
            Assert.AreEqual(GetMovieProperty(m => m.Overview), _mappedMovie.Overview);
        }

        [Test]
        public void Map_ShouldMapTitle()
        {
            Assert.AreEqual(GetMovieProperty(m => m.Original_Title), _mappedMovie.Title);
        }

        [Test]
        public void Map_ShouldMapReleaseDate()
        {
            Assert.AreEqual(GetMovieProperty(m => m.Release_Date), _mappedMovie.ReleaseDate);
        }

        [Test]
        public void Map_ShouldMapGenres()
        {
            Assert.AreEqual("Action, Romance", _mappedMovie.Genre);
        }

        private T GetMovieProperty<T>(Func<MovieDto, T> selector)
        {
            return selector(_movies[0]);
        }

        private static GenreDto[] CreateGenres()
        {
            return new[]
            {
                new GenreDto
                {
                    Id = 1,
                    Name = "Action"
                },
                new GenreDto
                {
                    Id = 2,
                    Name = "Romance"
                }
            };
        }

        private static MovieDto[] CreateMovies()
        {
            return new[]
            {
                new MovieDto
                {
                    Backdrop_Path = "Backdrop",
                    Poster_Path = "Poster",
                    Genre_Ids = new[] {1, 2},
                    Original_Title = "The movie",
                    Overview = "Movie overview",
                    Release_Date = new DateTime(2018, 1, 1)
                }
            };
        }
    }
}