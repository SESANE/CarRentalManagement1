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
    public class VehiclesController : ControllerBase
    {
        //refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //refactored
        //public VehiclesController(ApplicationDbContext context)
        public VehiclesController(IUnitOfWork unitOfWork)
        {
            //refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Vehicles
        [HttpGet]
        //refactored
        //public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        public async Task<IActionResult> GetVehicles()
        {
            //refactored
            //if (_context.Vehicles == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Vehicles.ToListAsync();
            var Vehicles = await _unitOfWork.Vehicles.GetAll(includes: q => q.Include(x => x.Make).Include(x => x.Model).Include(x => x.Colour));
            return Ok(Vehicles);
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        //refactored
        //public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        public async Task<IActionResult> GetVehicles(int id)
        {
            /*if (_context.Vehicles == null)
            {
                return NotFound();
            }*/
            var Vehicle = await _unitOfWork.Vehicles.Get(q => q.id == id);

            if (Vehicle == null)
            {
                return NotFound();
            }

            return Ok(Vehicle);
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle Vehicle)
        {
            if (id != Vehicle.id)
            {
                return BadRequest();
            }

            //refactored
            //_context.Entry(Vehicle).State = EntityState.Modified;
            _unitOfWork.Vehicles.Update(Vehicle);

            try
            {
                //refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //refactored
                //if (!VehicleExists(id))
                if (!await VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle Vehicle)
        {
            //refactored
            /*if (_context.Vehicles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
            }
              _context.Vehicles.Add(Vehicle);
              await _context.SaveChangesAsync();*/
            await _unitOfWork.Vehicles.Insert(Vehicle);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetVehicle", new { Vehicle.id }, Vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            /*if (_context.Vehicles == null)
            {
                return NotFound();
            }*/
            //refactored
            //var Vehicle = await _context.Vehicles.FindAsync(id);
            var Vehicle = await _unitOfWork.Vehicles.Get(q => q.id == id);
            if (Vehicle == null)
            {
                return NotFound();
            }

            //refactored
            //_context.Vehicles.Remove(Vehicle);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Vehicles.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //refactored
        //private bool VehicleExists(int id)
        private async Task<bool> VehicleExists(int id)
        {
            //refactored
            //return (_context.Vehicles?.Any(e => e.id == id)).GetValueOrDefault();
            var Vehicle = await _unitOfWork.Vehicles.Get(q => q.id == id);
            return Vehicle != null;
        }
    }
}
