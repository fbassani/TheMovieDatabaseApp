using System.Collections.Generic;

namespace TheMovieDatabaseApp.Model
{
    public class MoviesPage
    {
        public int TotalPages { get; set; }
        public List<Movie> Movies { get; set; }
    }
}