namespace WebMovieApp_4_10.Models
{
    public class Movie_Genre
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int MovieGenresId { get; set; }
        public MovieGenres MovieGenre { get; set; }
    }
}
