using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;

namespace DataMapper
{
    public class MovieMapper : IMapper<Movie>
    {
        private static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["Rental"].ToString();
        private readonly Dictionary<int, Movie> _cache = new Dictionary<int, Movie>();

        public static MovieMapper Instance { get; } = new MovieMapper();
        // This is a singleton, so constructor is private
        private MovieMapper() { }
        public Movie GetByID(int id)
        {
            if (_cache.ContainsKey(id))
            {
                return _cache[id];
            }
            Movie movie = GetByIDFromDB(id);
            _cache.Add(movie.ID, movie);
            return movie;
        }

        private Movie GetByIDFromDB(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM movies WHERE movie_id = @ID", conn))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        double pp = Convert.ToDouble(reader["price"]);
                        var copies = CopyMapper.Instance.GetByMovieId(id);
                        return new Movie(id, (string)reader["title"], (int)reader["year"], pp, copies);
                    }
                }
            }
            return null;
        }
        public void Save(Movie movie)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                // This is an UPSERT operation - if record doesn't exist in the database it is created, otherwise it is updated
                using (var command = new NpgsqlCommand("INSERT INTO movies(movie_id, title, year, price) " +
                    "VALUES (@ID, @title, @year, @price) " +
                    "ON CONFLICT (movie_id) DO UPDATE " +
                    "SET title = @title, year = @year, price = @price", conn))
                {
                    command.Parameters.AddWithValue("@ID", movie.ID);
                    command.Parameters.AddWithValue("@title", movie.Title);
                    command.Parameters.AddWithValue("@year", movie.Year);
                    command.Parameters.AddWithValue("@price", movie.Price);
                    command.ExecuteNonQuery();
                }
                // We need to save every copy in our list. 
                // Notice the "?" symbol - Copies might be an empty list, so we need protection from NullReferenceException
                movie.Copies?.ForEach(obj => CopyMapper.Instance.Save(obj));
            }
        }
        public void Delete(Movie movie)
        {
            throw new Exception("Not yet implemented");
        }
    }
}
