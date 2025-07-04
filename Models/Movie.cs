﻿namespace CinemaApp.Web.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public ICollection<CinemaMovie> CinemaMovies = new List<CinemaMovie>();

    }
}
