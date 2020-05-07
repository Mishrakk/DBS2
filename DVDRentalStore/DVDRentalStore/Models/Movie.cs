using System.Collections.Generic;

namespace DVDRentalStore.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
        public Movie(int movieId, string title, int year, List<Copy> copies = null)
        {
            MovieId = movieId;
            Title = title;
            Year = year;
            Copies = copies;
        }
        public Movie()
        {

        }
    }
}