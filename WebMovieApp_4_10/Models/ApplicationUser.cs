using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebMovieApp_4_10.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? MovieFee { get; set; }

        [Display(Name = "Actor Name")]
        public string Fullname
        {
            get { return FirstName + " " + LastName; }
        }

        public ICollection<MovieInvite>? MovieInvites { get; set; }
    }
}
