﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels
{
    public class CinemaDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public List<MovieProgramViewModel> Movies { get; set; } = new List<MovieProgramViewModel>();
    }
}
