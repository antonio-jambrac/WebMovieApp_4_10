using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Genres
{
    public class AddGenreModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddGenreModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Movie_Genre> MovieGenres { get; set; }
        public IList<MovieGenres> Genres { get; set; }
        public int MovieId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var allGenres = await _context.MovieGenres.ToListAsync();

            var addedMovieGenres = await _context.Movie_Genres.Include(m => m.Movie).Where(i => i.MovieId == id)
                .Include(g => g.MovieGenre)
                .AsNoTracking()
                .ToListAsync();
            
            if(addedMovieGenres == null)
                return NotFound();

            MovieId = id;
            Genres = allGenres;
            MovieGenres = addedMovieGenres;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int genreId)
        {
            if (id == 0 || genreId == 0)
                return NotFound();

            var m_g = new Movie_Genre
            {
                MovieId = id,
                MovieGenresId = genreId
            };

            _context.Movie_Genres.Add(m_g);
            await _context.SaveChangesAsync();

            return RedirectToPage("./AddGenre", new {id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int movieGenreId)
        {
            if (movieGenreId == 0)
                return NotFound();

            var m_g = await _context.Movie_Genres.FindAsync(movieGenreId);
            int movieId = new int();

            if(m_g != null)
            {
                movieId = m_g.MovieId;
                _context.Movie_Genres.Remove(m_g);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./AddGenre", new {id = movieId });
        }
    }
}
