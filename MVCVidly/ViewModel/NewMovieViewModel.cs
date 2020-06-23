﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCVidly.Models;

namespace MVCVidly.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public Movie Movie { get; set; }
    }
}