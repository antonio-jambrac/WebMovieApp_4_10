using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Authorization;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Movies
{
    [Authorize]
    public class InvitationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InvitationsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MovieInvite> Invites { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var movieInvites = await _context.MovieInvites
                .Include(m => m.Movie)
                .Include(u => u.ApplicationUser)
                .AsNoTracking()
                .ToListAsync();

            var userMovieIdList = await _context.Movie.Where(m => m.CreatorId == _userManager.GetUserId(User))
                .Select(i => i.ID).ToListAsync();
            var curUserId = _userManager.GetUserId(User);

            if (User.IsInRole(Constraints.ActorRole))
            {
                movieInvites = movieInvites.Where(i => i.ApplicationUserId == curUserId)
                    .Where(i => i.InviterId != curUserId).ToList();
            }
            else
            {
                movieInvites = movieInvites.Where(i => userMovieIdList.Contains((int)i.MovieID))
                    .Where(a => a.InviterId != curUserId).ToList();
            }

            if(movieInvites == null)
                return NotFound();

            Invites = movieInvites;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id ,InviteStatus status)
        {
            if (id == 0)
                return NotFound();

            var invite = await _context.MovieInvites.FindAsync(id);
            var actroStatus = status == InviteStatus.Accepted ? InviteStatus.Accepted : InviteStatus.Rejected;

            invite.Status = actroStatus;

            _context.Attach(invite).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Invitations");
        }

    }
}
