using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Rental
    {
        public int ID { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CopyId { get; set; }
        public int ClientId { get; set; }
        public virtual Copy Copy { get; set; }
        public virtual Client Client { get; set; }
    }
}