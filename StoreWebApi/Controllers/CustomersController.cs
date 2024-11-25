using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;
using StoreWebApi.Service;
using System.Text.Json;

namespace StoreWebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service) 
        {
            _service = service;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: v1/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: v1/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var customer = await _service.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: v1/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CreateCustomerRq customerRq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCustomer = await _service.Create(customerRq);

            return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.Id }, newCustomer);
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT: v1/Customers/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            if (!(await _service.GetAll()).Any(e => e.Id == id))
            {
                return NotFound();
            }

            await _service.Update(id, customerDto);

            return NoContent();
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: v1/Customers/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _service.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return NoContent();
        }

    }
}
