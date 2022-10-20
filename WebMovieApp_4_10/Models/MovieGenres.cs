namespace WebMovieApp_4_10.Models
{
    public class MovieGenres
    {
        public int ID { get; set; }
        public string Genre { get; set; }

        public ICollection<Movie_Genre>? Movie_Genres { get; set; }
    }
}
