namespace DataMapper
{
    public class Copy
    {
        public int ID { get; private set; }
        public bool Available { get; private set; }
        public int MovieId { get; private set; }
        public Copy(int id, bool available, int movieId)
        {
            ID = id;
            Available = available;
            MovieId = movieId;
        }
    }
}
