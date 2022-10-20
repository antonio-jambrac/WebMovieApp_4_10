using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Authorization;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Movies
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
      public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Movie, MovieOperation.Delete);

            if (!isAuthorized.Succeeded)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);

            if (movie != null)
            {
                Movie = movie;
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Movie, MovieOperation.Delete);

            if (!isAuthorized.Succeeded)
                return Forbid();

            _context.Movie.Remove(Movie);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
