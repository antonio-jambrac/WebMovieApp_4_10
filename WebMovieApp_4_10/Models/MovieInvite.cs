using System.ComponentModel.DataAnnotations.Schema;

namespace WebMovieApp_4_10.Models
{
    public enum InviteStatus
    {
        Submitted, Accepted, Rejected
    }

    public class MovieInvite
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        public int? MovieID { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string? ApplicationUserId { get; set; }
        public InviteStatus? Status { get; set; }
        public string InviterId { get; set; }

        public Movie? Movie { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
