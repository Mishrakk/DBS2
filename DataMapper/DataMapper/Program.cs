using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Simple fetch from database
            Movie movie = MovieMapper.Instance.GetByID(2);
            Console.WriteLine(movie.ToString());
            // This time MovieMapper will get the movie from cache, instead of getting it from database
            movie = MovieMapper.Instance.GetByID(2);
            Console.WriteLine(movie.ToString());

            // New object is created
            movie = new Movie(123, "The Last Samurai", 2003, 10);
            // Before the object was only in the memory. We need to save it, to store it in the persistence layer
            MovieMapper.Instance.Save(movie);
            Console.WriteLine(MovieMapper.Instance.GetByID(123).ToString());

            // We adjust the price
            movie.Price = 4.5;
            // We need to Save the object, or else price change will not be reflected in the database
            MovieMapper.Instance.Save(movie);
            // Price is changed both in the in-memory object and in the persistence layer
            Console.WriteLine(MovieMapper.Instance.GetByID(123).ToString());



            // Object is removed from the database
            try
            {
                MovieMapper.Instance.Delete(movie);
                if (MovieMapper.Instance.GetByID(123) == null)
                {
                    Console.WriteLine("Object is removed from the database");
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
