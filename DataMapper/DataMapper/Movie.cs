using System.Collections.Generic;

namespace DataMapper
{
    public class Movie
    {
        public int ID { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public double Price { get; set; }
        public List<Copy> Copies { get; private set; }
        public Movie(int id, string title, int year, double price, List<Copy> copies = null)
        {
            ID = id;
            Title = title;
            Year = year;
            Price = price;
            Copies = copies;
        }
        public override string ToString()
        {
            return $"Movie {ID}: {Title} produced in {Year} costs {Price} and has {Copies.Count} copies";
        }
    }
}
