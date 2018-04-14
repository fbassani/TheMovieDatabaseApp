using System;
using System.Collections.Generic;
using System.Linq;
using TheMovieDatabaseApp.Model;
using TheMovieDatabaseApp.Service;

namespace TheMovieDatabaseApp
{
    public class DtoToModelMapper
    {
        //TODO: move to settings
        private const string ImageUrl = "http://image.tmdb.org/t/p/w300/";

        public static List<Movie> Map(IEnumerable<MovieDto> movies, IEnumerable<GenreDto> genres)
        {
            return movies.Select(movie => new Movie
            {
                BackdropUrl = $"{ImageUrl}{movie.Backdrop_Path}",
                PosterUrl = $"{ImageUrl}{movie.Poster_Path}",
                Genre = String.Join(", ", genres.Where(g => movie.Genre_Ids.Contains(g.Id)).Select(g => g.Name)),
                ReleaseDate = movie.Release_Date,
                Overview = movie.Overview,
                Title = movie.Original_Title
            }).ToList();
        }
    }
}