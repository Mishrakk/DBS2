using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DataMapper
{
    public class CopyMapper : IMapper<Copy>
    {
        private static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["Rental"].ToString();
        public static CopyMapper Instance { get; } = new CopyMapper();
        // This is a singleton, so constructor is private
        private CopyMapper() { }

        public Copy GetByID(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM copies WHERE copy_id = @ID", conn))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Copy(id, (bool)reader["available"], (int)reader["movie_id"]);
                    }
                }
            }
            return null;
        }

        public List<Copy> GetByMovieId(int movieId)
        {
            List<Copy> list = new List<Copy>();
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM copies WHERE movie_id = @movieID", conn))
                {
                    command.Parameters.AddWithValue("@movieID", movieId);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Copy((int)reader["copy_id"], (bool)reader["available"], (int)reader["movie_id"]));
                    }
                }
            }
            return list;
        }
        public void Save(Copy copy)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                // This is an UPSERT operation - if record doesn't exist in the database it is created, otherwise it is updated
                using (var command = new NpgsqlCommand("INSERT INTO copies(copy_id, available, movie_id) " +
                    "VALUES (@ID, @available, @movieId) " +
                    "ON CONFLICT (copy_id) DO UPDATE " +
                    "SET available = @available, movie_id = @movieId", conn))
                {
                    command.Parameters.AddWithValue("@ID", copy.ID);
                    command.Parameters.AddWithValue("@available", copy.Available);
                    command.Parameters.AddWithValue("@movieId", copy.MovieId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Copy copy)
        {
            throw new Exception("Not yet implemented");
        }
    }
}
