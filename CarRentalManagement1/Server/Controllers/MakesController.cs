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
    public class MakesController : ControllerBase
    {
        //refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //refactored
        //public MakesController(ApplicationDbContext context)
        public MakesController(IUnitOfWork unitOfWork)
        {
            //refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Makes
        [HttpGet]
        //refactored
        //public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        public async Task<IActionResult> GetMakes()
        {
            //refactored
            //if (_context.Makes == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Makes.ToListAsync();
            var makes = await _unitOfWork.Makes.GetAll();
            return Ok(makes);
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //refactored
        //public async Task<ActionResult<Make>> GetMake(int id)
        public async Task<IActionResult> GetMakes(int id)
        {
          /*if (_context.Makes == null)
          {
              return NotFound();
          }*/
            var make = await _unitOfWork.Makes.Get(q => q.id == id);

            if (make == null)
            {
                return NotFound();
            }

            return Ok(make);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id, Make make)
        {
            if (id != make.id)
            {
                return BadRequest();
            }

            //refactored
            //_context.Entry(make).State = EntityState.Modified;
            _unitOfWork.Makes.Update(make);

            try
            {
                //refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //refactored
                //if (!MakeExists(id))
                if (!await MakeExists(id))
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

        // POST: api/Makes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Make>> PostMake(Make make)
        {
          //refactored
          /*if (_context.Makes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Makes'  is null.");
          }
            _context.Makes.Add(make);
            await _context.SaveChangesAsync();*/
            await _unitOfWork.Makes.Insert(make);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMake", new { make.id }, make);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            /*if (_context.Makes == null)
            {
                return NotFound();
            }*/
            //refactored
            //var make = await _context.Makes.FindAsync(id);
            var make = await _unitOfWork.Makes.Get(q => q.id == id);
            if (make == null)
            {
                return NotFound();
            }

            //refactored
            //_context.Makes.Remove(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Makes.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //refactored
        //private bool MakeExists(int id)
        private async Task<bool> MakeExists(int id)
        {
            //refactored
            //return (_context.Makes?.Any(e => e.id == id)).GetValueOrDefault();
            var make = await _unitOfWork.Makes.Get(q => q.id == id);
            return make != null;
        }
    }
}
