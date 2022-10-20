using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Movie> Movie { get;set; } = default!;
        public IList<Movie_Genre> Genres { get;set; } = default!;
        public SelectList? GenresSelectList { get;set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get;set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchGenre { get;set; }

        public async Task OnGetAsync(string sortMovies)
        {
            Genres = await _context.Movie_Genres.Include(x => x.MovieGenre).Include(m => m.Movie).ToListAsync();

            IQueryable<string> genreQuery = from m in _context.MovieGenres orderby m.Genre select m.Genre;

            Movie = await _context.Movie.Include(m => m.Movie_Genres).AsNoTracking().ToListAsync();

            if (!string.IsNullOrEmpty(sortMovies))
            {
                Movie = Movie.Where(i => i.CreatorId == _userManager.GetUserId(User)).ToList();
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                var movies = Movie.Where(m => m.Title.Contains(SearchString));
                Movie = movies.ToList();
            }

            if (!string.IsNullOrEmpty(SearchGenre))
            {
                var movies = Genres.Where(m => m.MovieGenre.Genre == SearchGenre).Select(m => m.Movie).ToList();
                Movie = movies.ToList();
                //Movie = movies.Where(m => Movie.Contains(m)).ToList();
            }

            GenresSelectList = new SelectList(await genreQuery.Distinct().ToListAsync());
        }

        // Handle Actor Invitations
        public async Task<IActionResult> OnPostAsync(int movieId, string actorId)
        {
            var movieInvite = new MovieInvite
            {
                MovieID = movieId,
                ApplicationUserId = actorId,
                InviterId = actorId,
                Status = InviteStatus.Submitted
            };

            if(movieInvite != null)
            {
                _context.MovieInvites.Add(movieInvite);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("ApplySuccess", new { movieId });
        }
    }
}
