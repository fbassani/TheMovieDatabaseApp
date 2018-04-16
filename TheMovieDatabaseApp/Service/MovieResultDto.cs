using System.Collections.Generic;

namespace TheMovieDatabaseApp.Service
{
    public class MovieResultDto
    {
        public List<MovieDto> Results { get; set; }
        public int Total_Pages { get; set; }
    }
}