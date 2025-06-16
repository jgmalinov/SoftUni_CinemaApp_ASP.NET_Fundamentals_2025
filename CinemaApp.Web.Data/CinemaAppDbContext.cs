using CinemaApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Data
{
    public class CinemaAppDbContext: DbContext
    {
        public CinemaAppDbContext(DbContextOptions<CinemaAppDbContext> options) : base(options){}
        public DbSet<Movie> Movies { get; set; }
    }
}
