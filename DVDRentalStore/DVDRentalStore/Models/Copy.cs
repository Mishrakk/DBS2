using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Copy
    {
        public int ID { get; set; }
        public bool Available { get; set; }
        public int MovieId { get; set; }
        public string SerialNumber { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public Copy(int id, bool available, int movieId)
        {
            ID = id;
            Available = available;
            MovieId = movieId;
        }
        public Copy()
        {

        }
    }
}