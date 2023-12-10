using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagement1.Server.Data;
using CarRentalManagement1.Shared.Domain;
using CarRentalManagement1.Server.IRepository;
using CarRentalManagement1.Server.Repository;

namespace CarRentalManagement1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        //refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //refactored
        //public BookingsController(ApplicationDbContext context)
        public BookingsController(IUnitOfWork unitOfWork)
        {
            //refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Bookings
        [HttpGet]
        //refactored
        //public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        public async Task<IActionResult> GetBookings()
        {
            //refactored
            //if (_context.Bookings == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Bookings.ToListAsync();
            var Bookings = await _unitOfWork.Bookings.GetAll(includes: q => q.Include(x => x.Vehicle).Include(x => x.Customer));
            return Ok(Bookings);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        //refactored
        //public async Task<ActionResult<Booking>> GetBooking(int id)
        public async Task<IActionResult> GetBookings(int id)
        {
            /*if (_context.Bookings == null)
            {
                return NotFound();
            }*/
            var Booking = await _unitOfWork.Bookings.Get(q => q.id == id);

            if (Booking == null)
            {
                return NotFound();
            }

            return Ok(Booking);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking Booking)
        {
            if (id != Booking.id)
            {
                return BadRequest();
            }

            //refactored
            //_context.Entry(Booking).State = EntityState.Modified;
            _unitOfWork.Bookings.Update(Booking);

            try
            {
                //refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //refactored
                //if (!BookingExists(id))
                if (!await BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking Booking)
        {
            //refactored
            /*if (_context.Bookings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bookings'  is null.");
            }
              _context.Bookings.Add(Booking);
              await _context.SaveChangesAsync();*/
            await _unitOfWork.Bookings.Insert(Booking);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBooking", new { Booking.id }, Booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            /*if (_context.Bookings == null)
            {
                return NotFound();
            }*/
            //refactored
            //var Booking = await _context.Bookings.FindAsync(id);
            var Booking = await _unitOfWork.Bookings.Get(q => q.id == id);
            if (Booking == null)
            {
                return NotFound();
            }

            //refactored
            //_context.Bookings.Remove(Booking);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Bookings.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //refactored
        //private bool BookingExists(int id)
        private async Task<bool> BookingExists(int id)
        {
            //refactored
            //return (_context.Bookings?.Any(e => e.id == id)).GetValueOrDefault();
            var Booking = await _unitOfWork.Bookings.Get(q => q.id == id);
            return Booking != null;
        }
    }
}
