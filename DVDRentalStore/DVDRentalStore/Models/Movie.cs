using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Movie(int movieId, string title, int year)
        {
            MovieId = movieId;
            Title = title;
            Year = year;
        }
    }
}