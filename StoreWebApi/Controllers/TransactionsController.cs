using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Models;
using System.Text.Json;

namespace StoreWebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<PaymentsController> _logger;

        public TransactionsController(StoreDbContext context, ILogger<PaymentsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: v1/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            _logger.LogInformation("{Method} id = {Id}", nameof(GetTransaction), id);

            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                _logger.LogInformation("{Method}({Id}) NOT FOUND", nameof(GetTransaction), id);
                return NotFound();
            }

            return transaction;
        }


        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            _logger.LogInformation("{Method}({TransactionDetails})", nameof(CreateTransaction), JsonSerializer.Serialize(transaction));

            foreach (var a in transaction.TransactionArticles)
            {
                var article = await _context.Articles.FindAsync(a.ArticleId);
                if (article == null || article.Inventory < 1)
                    return BadRequest("Article not available in inventory");

                article.Inventory--; // Decrease inventory
            }

            foreach (var payment in transaction.Payments)
            {
                // Add business logic for payment processing if needed
                _context.Payments.Add(payment);
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateTransaction), new { id = transaction.Id }, transaction);
        }
    }
}
