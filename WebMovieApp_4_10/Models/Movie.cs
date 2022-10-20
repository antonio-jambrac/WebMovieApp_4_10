using System.ComponentModel.DataAnnotations;

namespace WebMovieApp_4_10.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Director { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public string CreatorId { get; set; }

        [Display(Name = "Recording Star")]
        [DataType(DataType.Date)]
        public DateTime RecordingStar { get; set; }
        
        [Display(Name = "Recording End")]
        [DataType(DataType.Date)]
        public DateTime RecordingFinish { get; set; }

        public ICollection<ApplicationUser>? Actors { get; set; }
        public ICollection<MovieInvite>? MovieInvites { get; set; }
        public ICollection<Movie_Genre>? Movie_Genres { get; set; } 
    }
}
