using Jwt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Controller
{
    [ApiController]
    [Authorize(Roles="Admin")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private IWebHostEnvironment _webHostEnvironment;

        public AdminController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getmovie")]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await dbContext.Movies.ToListAsync());

        }




        [HttpPost("addmovie")]
        public async Task<IActionResult> AddMovie(AddMovieRequest addMovieRequest)
        {
            var movie = new Movie()
            {
                Id = Guid.NewGuid(),
                Name = addMovieRequest.Name,
                Date= addMovieRequest.Date,
                Genre = addMovieRequest.Genre,
                Cast = addMovieRequest.Cast,
            };
            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
            return Ok(movie);
        }











    }
}
