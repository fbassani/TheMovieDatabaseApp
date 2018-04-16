using System;

namespace TheMovieDatabaseApp.Service
{
    public class MovieDto
    {
        public string Poster_Path { get; set; }
        public string Overview { get; set; }
        public DateTime Release_Date { get; set; }
        public int[] Genre_Ids { get; set; }
        public string Backdrop_Path { get; set; }
        public string Original_Title { get; set; }
    }
}