using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class DetailsModel : PageModel
    {
        private static int GetNumberOfWorkingDays(DateTime start, DateTime end)
        {
            int days = 0;
            while(start <= end)
            {
                if(start.DayOfWeek != DayOfWeek.Sunday && start.DayOfWeek != DayOfWeek.Saturday)
                {
                    ++days;
                }
                start = start.AddDays(1);
            }
            return days;
        }

        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public DetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public Movie Movie { get; set; }
        public int RecordingTime { get; set; }
        public int TotalCost { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.MovieInvites!.Where(i => i.Status == InviteStatus.Accepted).Where(i => i.MovieID == id))
                    .ThenInclude(a => a.ApplicationUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ID == id);

            var recTime = GetNumberOfWorkingDays(movie.RecordingStar, movie.RecordingFinish);

            var actroCost = 0;

            foreach(var user in movie.MovieInvites)
            {
                if(user.ApplicationUser.MovieFee == null)
                {
                    user.ApplicationUser.MovieFee = 0;
                }
                actroCost +=  (int)user.ApplicationUser.MovieFee * recTime;
            }

            if (recTime > 0)
            {
                RecordingTime = recTime;
            }
            else RecordingTime = 1;

            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
                TotalCost = actroCost;
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Movie, MovieOperation.Read);

            if (!isAuthorized.Succeeded)
                return Forbid();

            return Page();
        }
    }
}
