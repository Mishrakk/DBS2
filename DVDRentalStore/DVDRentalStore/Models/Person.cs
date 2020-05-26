using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DVDRentalStore.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Column("FName")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("LName")]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}