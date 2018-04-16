using System;
using System.Collections.Generic;
using System.Linq;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp
{
    public class DtoToModelMapper
    {
        public static List<Movie> Map(IEnumerable<MovieDto> movies, IEnumerable<GenreDto> genres)
        {
            return movies.Select(movie => new Movie
            {
                BackdropPath = movie.Backdrop_Path,
                PosterPath = movie.Poster_Path,
                Genre = String.Join(", ", genres.Where(g => movie.Genre_Ids.Contains(g.Id)).Select(g => g.Name)),
                ReleaseDate = movie.Release_Date,
                Overview = movie.Overview,
                Title = movie.Original_Title
            }).ToList();
        }
    }
}