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
    public class ColoursController : ControllerBase
    {
        //refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //refactored
        //public ColoursController(ApplicationDbContext context)
        public ColoursController(IUnitOfWork unitOfWork)
        {
            //refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Colours
        [HttpGet]
        //refactored
        //public async Task<ActionResult<IEnumerable<Colour>>> GetColours()
        public async Task<IActionResult> GetColours()
        {
            //refactored
            //if (_context.Colours == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Colours.ToListAsync();
            var Colours = await _unitOfWork.Colours.GetAll();
            return Ok(Colours);
        }

        // GET: api/Colours/5
        [HttpGet("{id}")]
        //refactored
        //public async Task<ActionResult<Colour>> GetColour(int id)
        public async Task<IActionResult> GetColours(int id)
        {
            /*if (_context.Colours == null)
            {
                return NotFound();
            }*/
            var Colour = await _unitOfWork.Colours.Get(q => q.id == id);

            if (Colour == null)
            {
                return NotFound();
            }

            return Ok(Colour);
        }

        // PUT: api/Colours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColour(int id, Colour Colour)
        {
            if (id != Colour.id)
            {
                return BadRequest();
            }

            //refactored
            //_context.Entry(Colour).State = EntityState.Modified;
            _unitOfWork.Colours.Update(Colour);

            try
            {
                //refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //refactored
                //if (!ColourExists(id))
                if (!await ColourExists(id))
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

        // POST: api/Colours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colour>> PostColour(Colour Colour)
        {
            //refactored
            /*if (_context.Colours == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Colours'  is null.");
            }
              _context.Colours.Add(Colour);
              await _context.SaveChangesAsync();*/
            await _unitOfWork.Colours.Insert(Colour);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetColour", new { Colour.id }, Colour);
        }

        // DELETE: api/Colours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColour(int id)
        {
            /*if (_context.Colours == null)
            {
                return NotFound();
            }*/
            //refactored
            //var Colour = await _context.Colours.FindAsync(id);
            var Colour = await _unitOfWork.Colours.Get(q => q.id == id);
            if (Colour == null)
            {
                return NotFound();
            }

            //refactored
            //_context.Colours.Remove(Colour);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Colours.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //refactored
        //private bool ColourExists(int id)
        private async Task<bool> ColourExists(int id)
        {
            //refactored
            //return (_context.Colours?.Any(e => e.id == id)).GetValueOrDefault();
            var Colour = await _unitOfWork.Colours.Get(q => q.id == id);
            return Colour != null;
        }
    }
}
