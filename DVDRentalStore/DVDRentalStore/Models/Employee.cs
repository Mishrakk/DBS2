using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public class Employee : Person
    {
        public DateTime HireDate { get; set; }
        public string Localization { get; set; }
    }
}