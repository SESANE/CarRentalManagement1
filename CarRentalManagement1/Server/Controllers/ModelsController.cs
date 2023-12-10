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
    public class ModelsController : ControllerBase
    {
        //refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //refactored
        //public ModelsController(ApplicationDbContext context)
        public ModelsController(IUnitOfWork unitOfWork)
        {
            //refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Models
        [HttpGet]
        //refactored
        //public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        public async Task<IActionResult> GetModels()
        {
            //refactored
            //if (_context.Models == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Models.ToListAsync();
            var Models = await _unitOfWork.Models.GetAll();
            return Ok(Models);
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        //refactored
        //public async Task<ActionResult<Model>> GetModel(int id)
        public async Task<IActionResult> GetModels(int id)
        {
            /*if (_context.Models == null)
            {
                return NotFound();
            }*/
            var Model = await _unitOfWork.Models.Get(q => q.id == id);

            if (Model == null)
            {
                return NotFound();
            }

            return Ok(Model);
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(int id, Model Model)
        {
            if (id != Model.id)
            {
                return BadRequest();
            }

            //refactored
            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Models.Update(Model);

            try
            {
                //refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //refactored
                //if (!ModelExists(id))
                if (!await ModelExists(id))
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

        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel(Model Model)
        {
            //refactored
            /*if (_context.Models == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Models'  is null.");
            }
              _context.Models.Add(Model);
              await _context.SaveChangesAsync();*/
            await _unitOfWork.Models.Insert(Model);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetModel", new { Model.id }, Model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            /*if (_context.Models == null)
            {
                return NotFound();
            }*/
            //refactored
            //var Model = await _context.Models.FindAsync(id);
            var Model = await _unitOfWork.Models.Get(q => q.id == id);
            if (Model == null)
            {
                return NotFound();
            }

            //refactored
            //_context.Models.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Models.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //refactored
        //private bool ModelExists(int id)
        private async Task<bool> ModelExists(int id)
        {
            //refactored
            //return (_context.Models?.Any(e => e.id == id)).GetValueOrDefault();
            var Model = await _unitOfWork.Models.Get(q => q.id == id);
            return Model != null;
        }
    }
}
