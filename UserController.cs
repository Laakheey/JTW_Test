using Jwt.Model;
using Jwt.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Jwt.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]

    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private IGenericRepository<Movie> genericRepository = null;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.genericRepository = new GenericRepository<Movie>();
        }


        [HttpGet("getmovies")]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(genericRepository.GetAll());

        }


        [HttpGet("booked")]
        public async Task<IActionResult> Booked()
        {
            return Ok(dbContext.Bookings.ToList());

        }


    

        [HttpPost("Booking")]
        public async Task<IActionResult> Booking(Booking booking)
        {
            var movie = await dbContext.Movies.FindAsync(booking.Id);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            var existingBooking = await dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == booking.Id);
            if (existingBooking != null)
            {
                return BadRequest("Already booked");
            }

            booking.Name = movie.Name;
            booking.Date = movie.Date;
            booking.Genre = movie.Genre;
            booking.Cast = movie.Cast;

            dbContext.Bookings.Add(booking);
            await dbContext.SaveChangesAsync();

            return Ok(movie);
        }









    }
}
