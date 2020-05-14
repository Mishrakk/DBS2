using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Client : Person
    {
        public DateTime Birthday { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}