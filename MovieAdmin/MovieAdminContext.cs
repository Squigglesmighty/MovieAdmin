using Microsoft.EntityFrameworkCore;

public class MovieAdminContext(DbContextOptions<MovieAdminContext> options) : DbContext(options)
{
    public DbSet<MovieAdmin.Models.Movie> Movie { get; set; } = default!;
}
