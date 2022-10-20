using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Authorization;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Users
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ApplicationUser> ApplicationUsers { get; set; }
        public IList<IdentityUserRole<string>> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _context.ApplicationUsers.ToListAsync();
            Roles = await _context.UserRoles.ToListAsync();

            foreach(var user in users.ToList())
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, Constraints.AdminRole);
                if (isAdmin) users.Remove(user);
            }

            if (!User.IsInRole(Constraints.AdminRole))
                return Forbid();

            if(users != null)
            {
                ApplicationUsers = users;
            }

            return Page();
        }

        //Delete User
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);

            if (!User.IsInRole(Constraints.AdminRole))
                return Forbid();

            if (user == null)
                return NotFound();

            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Users");
        }
    }
}
