﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheMovieDatabaseApp.Service
{
    public interface IMovieFinder
    {
        Task<List<MovieDto>> GetPage(int page = 0);
    }
}