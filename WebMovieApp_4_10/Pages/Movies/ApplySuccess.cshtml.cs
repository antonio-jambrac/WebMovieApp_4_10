using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Movies
{
    public class ApplySuccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ApplySuccessModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int movieId)
        {
            if (movieId == null)
                return NotFound();

            Movie = await _context.Movie.FindAsync(movieId);

            return Page();
        }
    }
}
