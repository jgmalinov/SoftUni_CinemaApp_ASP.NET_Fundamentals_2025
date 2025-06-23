using CinemaApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Web.Data
{
    public class CinemaAppDbContext: IdentityDbContext<IdentityUser>
    {
        public CinemaAppDbContext(DbContextOptions<CinemaAppDbContext> options) : base(options){}
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaMovie> CinemaMovies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

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
            
            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany()
                .HasForeignKey(um => um.MovieId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
