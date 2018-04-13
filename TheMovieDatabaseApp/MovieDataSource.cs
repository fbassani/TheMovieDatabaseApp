using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp
{
    public class MovieDataSource : IMovieDataSource
    {
        private readonly IMovieFinder _movieFinder;

        public MovieDataSource(IMovieFinder movieFinder)
        {
            _movieFinder = movieFinder;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await Task.FromResult(new List<Movie> 
            {
                new Movie
                {
                    Title = "Back to the future",
                    Summary = "Marty McFly, a 17-year-old high school student, is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his close friend, the maverick scientist Doc Brown.",
                    Genre = "Sci fi",
                    PosterUrl = "https://ia.media-imdb.com/images/M/MV5BZmU0M2Y1OGUtZjIxNi00ZjBkLTg1MjgtOWIyNThiZWIwYjRiXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_UX182_CR0,0,182,268_AL_.jpg",
                    ReleaseDate = new DateTime(1985, 1, 1)

                },
                new Movie
                {
                    Title = "The Godfather",
                    Summary = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    Genre = "Drama",
                    PosterUrl = "https://ia.media-imdb.com/images/M/MV5BY2Q2NzQ3ZDUtNWU5OC00Yjc0LThlYmEtNWM3NTFmM2JiY2VhXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SY1000_CR0,0,700,1000_AL_.jpg",
                    ReleaseDate = new DateTime(1972, 1, 1)
                }
            });
        }
    }
}