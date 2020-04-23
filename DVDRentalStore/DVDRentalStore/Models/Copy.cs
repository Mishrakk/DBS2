using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Copy
    {
        public int ID { get; private set; }
        public bool Available { get; private set; }
        public int MovieId { get; private set; }
        public Copy(int id, bool available, int movieId)
        {
            ID = id;
            Available = available;
            MovieId = movieId;
        }
    }
}