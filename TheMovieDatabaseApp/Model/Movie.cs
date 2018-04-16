using System;

namespace TheMovieDatabaseApp.Model
{
    public class Movie
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Genre { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string ImagePath => string.IsNullOrEmpty(BackdropPath) ? PosterPath : BackdropPath;

    }
}