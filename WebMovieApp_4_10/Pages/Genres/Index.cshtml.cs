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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MovieGenres> MovieGenres { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MovieGenres != null)
            {
                MovieGenres = await _context.MovieGenres.ToListAsync();
            }
        }
    }
}
