using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieAdmin.Models;
using MovieAdmin.Services;

[Authorize(Roles = "Administrator, Critic")]
public class MoviesController : Controller
{
    private readonly MovieAdminContext _context;
    private readonly IAzureStorageService _azureStorage;

    public MoviesController(MovieAdminContext context, IAzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }

    // GET: MOVIES/Index
    [Authorize(Roles = "Administrator, Critic")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .OrderBy(t => t.ReleaseDate)
            .ToListAsync());
    }

    // GET: MOVIES/Details/5
    [Authorize(Roles = "Administrator, Critic")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    [Authorize(Roles = "Administrator")]
    public IActionResult Create()
    {
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title");
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title");

        return View();
    }

    // POST: MOVIES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Create([Bind("Id,Title,Synopsis,GenreId,AudienceRatingId,Runtime,ReleaseDate")] Movie movie, IFormFile? ImageUpload)
    {
        if (ModelState.IsValid)
        {
            // Upload file to Azure Storage (after input validation occurs)
            if (ImageUpload != null)
            {
                string filename = ImageUpload.FileName;
                string url = await _azureStorage.UploadFileAsync(ImageUpload, filename);

                movie.ImageUrl = url;
            }

            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title", movie.GenreId);
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title", movie.AudienceRatingId);
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title", movie.GenreId);
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title", movie.AudienceRatingId);
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Synopsis,GenreId,AudienceRatingId,Runtime,ReleaseDate,ImageUrl")] Movie movie, IFormFile? ImageUpload)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (ImageUpload != null)
            {
                string filename = ImageUpload.FileName;
                string url = await _azureStorage.UploadFileAsync(ImageUpload, filename);

                movie.ImageUrl = url;
            }

            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title", movie.GenreId);
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title", movie.AudienceRatingId);
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}