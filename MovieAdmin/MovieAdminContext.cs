using Microsoft.EntityFrameworkCore;

public class MovieAdminContext(DbContextOptions<MovieAdminContext> options) : DbContext(options)
{
    public DbSet<MovieAdmin.Models.Movie> Movie { get; set; } = default!;
    public DbSet<MovieAdmin.Models.Genre> Genre { get; set; } = default!;
    public DbSet<MovieAdmin.Models.AudienceRating> AudienceRating { get; set; } = default!;
}
