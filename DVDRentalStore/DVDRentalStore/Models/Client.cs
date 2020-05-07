using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}