using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MovieInvite> MovieInvites { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }
        public DbSet<Movie_Genre> Movie_Genres { get; set; }
    }
}