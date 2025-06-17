using CinemaApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaApp.Web.Data
{
    public class CinemaAppDbContext: DbContext
    {
        public CinemaAppDbContext(DbContextOptions<CinemaAppDbContext> options) : base(options){}
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaMovie> CinemaMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.CinemaMovies)
                .WithOne(cm => cm.Movie)
                .HasForeignKey(cm => cm.MovieId);
            
            modelBuilder.Entity<Cinema>()
                .HasMany(c => c.CinemaMovies)
                .WithOne(cm => cm.Cinema)
                .HasForeignKey(cm => cm.CinemaId);

            modelBuilder.Entity<CinemaMovie>()
                .HasKey(cm => new { cm.CinemaId, cm.MovieId });
        }
    }
}
