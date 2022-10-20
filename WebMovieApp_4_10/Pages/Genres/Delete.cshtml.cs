using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Genres
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public MovieGenres MovieGenres { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MovieGenres == null)
            {
                return NotFound();
            }

            var moviegenres = await _context.MovieGenres.FirstOrDefaultAsync(m => m.ID == id);

            if (moviegenres == null)
            {
                return NotFound();
            }
            else 
            {
                MovieGenres = moviegenres;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MovieGenres == null)
            {
                return NotFound();
            }
            var moviegenres = await _context.MovieGenres.FindAsync(id);

            if (moviegenres != null)
            {
                MovieGenres = moviegenres;
                _context.MovieGenres.Remove(MovieGenres);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
