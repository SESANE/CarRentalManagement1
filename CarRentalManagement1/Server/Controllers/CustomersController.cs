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
    public class CustomersController : ControllerBase
    {
        //refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //refactored
        //public CustomersController(ApplicationDbContext context)
        public CustomersController(IUnitOfWork unitOfWork)
        {
            //refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //refactored
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetCustomers()
        {
            //refactored
            //if (_context.Customers == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Customers.ToListAsync();
            var Customers = await _unitOfWork.Customers.GetAll();
            return Ok(Customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //refactored
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetCustomers(int id)
        {
            /*if (_context.Customers == null)
            {
                return NotFound();
            }*/
            var Customer = await _unitOfWork.Customers.Get(q => q.id == id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Ok(Customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer Customer)
        {
            if (id != Customer.id)
            {
                return BadRequest();
            }

            //refactored
            //_context.Entry(Customer).State = EntityState.Modified;
            _unitOfWork.Customers.Update(Customer);

            try
            {
                //refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //refactored
                //if (!CustomerExists(id))
                if (!await CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer Customer)
        {
            //refactored
            /*if (_context.Customers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Customers'  is null.");
            }
              _context.Customers.Add(Customer);
              await _context.SaveChangesAsync();*/
            await _unitOfWork.Customers.Insert(Customer);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCustomer", new { Customer.id }, Customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            /*if (_context.Customers == null)
            {
                return NotFound();
            }*/
            //refactored
            //var Customer = await _context.Customers.FindAsync(id);
            var Customer = await _unitOfWork.Customers.Get(q => q.id == id);
            if (Customer == null)
            {
                return NotFound();
            }

            //refactored
            //_context.Customers.Remove(Customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Customers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //refactored
        //private bool CustomerExists(int id)
        private async Task<bool> CustomerExists(int id)
        {
            //refactored
            //return (_context.Customers?.Any(e => e.id == id)).GetValueOrDefault();
            var Customer = await _unitOfWork.Customers.Get(q => q.id == id);
            return Customer != null;
        }
    }
}
