using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;
using System.Text.Json;

namespace StoreWebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: v1/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: v1/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var payment = await _service.GetById(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: v1/Payments
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(CreatePaymentRq paymentRq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = new Payment()
            {
                Amount = paymentRq.Amount,
                Type = paymentRq.Type
            };

            var newPayment = await _service.Create(paymentRq);

            return CreatedAtAction(nameof(GetPayment), new { id = newPayment.Id }, newPayment);
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // PUT: v1/Payments/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentDto.Id)
            {
                return BadRequest();
            }

            if (!(await _service.GetAll()).Any(e => e.Id == id))
            {
                return NotFound();
            }

            await _service.Update(id, paymentDto);

            return NoContent();
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: v1/Payments/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _service.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return NoContent();
        }

    }
}
