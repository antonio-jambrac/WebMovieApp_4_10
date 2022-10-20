using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Authorization;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Movies
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var isActor = User.IsInRole(Constraints.ActorRole);

            IQueryable<string> genreList = from m in _context.MovieGenres select m.Genre;

            GenreList = new SelectList(await genreList.ToListAsync());

            if (isActor)
                return Forbid();

            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }
        public SelectList GenreList { get; set; }
        public string Genre { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (User == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var creatorId = _userManager.GetUserId(User);

            Movie.Director = user.Fullname;
            Movie.CreatorId = creatorId;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Movie, MovieOperation.Create);

            if (!isAuthorized.Succeeded)
                return Forbid();

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
