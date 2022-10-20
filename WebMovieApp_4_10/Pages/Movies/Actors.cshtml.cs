using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebMovieApp_4_10.Authorization;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Movies
{
    [Authorize]
    public class ActorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActorsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ApplicationUser> Users { get; set; }
        public MovieInvite Invite { get; set; }
        public IList<MovieInvite> Invites { get; set; }
        public IList<string> UserIds { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Users = _userManager.GetUsersInRoleAsync(Constraints.ActorRole).Result;
            Invites = await _context.MovieInvites.Where(i => i.MovieID == id).ToListAsync();

            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
                return NotFound();

            if (movie.CreatorId != _userManager.GetUserId(User))
                return Forbid();

            var inviteUserId = new List<string>();
                
            foreach(var item in Invites)
            {
                inviteUserId.Add(item.ApplicationUserId);
            }
            UserIds = inviteUserId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id /*movie id*/ ,string actorId)
        {
            if (id == 0 || actorId == null)
                return Page();

            var movie = await _context.Movie.FindAsync(id);

            Invite = new MovieInvite
            {
                MovieID = id,
                InviterId = movie.CreatorId,
                ApplicationUserId = actorId,
                Status = InviteStatus.Submitted
            };

            _context.MovieInvites.Add(Invite);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
