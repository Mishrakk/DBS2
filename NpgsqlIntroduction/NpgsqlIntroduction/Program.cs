using System;
using Npgsql;

namespace NpgsqlIntroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify connection options and open an connection		
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["Rental"].ToString();
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            // Define a query
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT title FROM movies", conn);

            // Execute a query
            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            // Read all rows and output the first column in each row
            while (dataReader.Read())
                Console.Write($"title: {dataReader[0]}\n");
            // Once we're done reading data we need to close the reader
            dataReader.Close();

            // NpgsqlCommand is also disposable, so we can use it in using block
            using (var cmd2 = new NpgsqlCommand("DELETE FROM movies WHERE year < 1950", conn))
            {
                // DELETE statements are not returning any data, so we execute them as NonQuery
                cmd2.ExecuteNonQuery();
            }

            using (var cmd2 = new NpgsqlCommand("SELECT title, year FROM movies", conn))
            {

                using (NpgsqlDataReader dataReader2 = cmd2.ExecuteReader())
                {
                    while (dataReader2.Read())
                        // We can access row fields either by column index or column name
                        Console.Write($"Movie {dataReader2["title"]} was produced in {dataReader2["year"]}\n");
                }
            }

            Console.Write("Display movies produced in year: ");
            string year = Console.ReadLine();
            // This is a bad way of introducing parameters to queries. This is in fact SQL Injection vulnerability!
            // Never write this type of code!!!
            using (var cmd2 = new NpgsqlCommand($"SELECT title, year FROM movies WHERE year = {year}", conn))
            {

                using (NpgsqlDataReader dataReader2 = cmd2.ExecuteReader())
                {
                    Console.WriteLine($"Movies produced in {year}");
                    while (dataReader2.Read())
                        Console.Write($"Movie {dataReader2["title"]} was produced in {dataReader2["year"]}\n");
                }
            }

            // Instead do this properly, by introducing Parameters to sql commands.
            using (var cmd2 = new NpgsqlCommand("SELECT title, year FROM movies WHERE year = @year", conn))
            {
                cmd2.Parameters.AddWithValue("@year", int.Parse(year));
                using (NpgsqlDataReader dataReader2 = cmd2.ExecuteReader())
                {
                    Console.WriteLine($"Movies produced in {year}");
                    while (dataReader2.Read())
                        Console.Write($"Movie {dataReader2["title"]} was produced in {dataReader2["year"]}\n");
                }
            }

            // Close connection
            conn.Close();
        }
    }
}
