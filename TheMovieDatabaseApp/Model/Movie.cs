using System;

namespace TheMovieDatabaseApp.Model
{
    public class Movie
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Genre { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}