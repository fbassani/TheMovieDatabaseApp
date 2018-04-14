using System;
using System.Collections.Generic;

namespace TheMovieDatabaseApp.Model
{
    public class Movie
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Genre { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string ImageUrl => string.IsNullOrEmpty(BackdropUrl) ? PosterUrl : BackdropUrl;
    }

    public class MoviesPage
    {
        public int TotalPages { get; set; }
        public List<Movie> Movies { get; set; }
    }
}