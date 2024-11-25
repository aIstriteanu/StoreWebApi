using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;
using System.Text.Json;

namespace StoreWebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: v1/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: v1/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            if (id <= 0)
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
        // POST: v1/Articles
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(CreateArticleRq articleRq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = new Article() 
            {
                Name = articleRq.Name,
                Inventory = articleRq.Inventory,
                Price = articleRq.Price
            };

            var newArticle = await _service.Create(articleRq);

            return CreatedAtAction(nameof(GetArticle), new { id = newArticle.Id }, newArticle);
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT: v1/Articles/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, ArticleDto articleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articleDto.Id)
            {
                return BadRequest();
            }

            if (!(await _service.GetAll()).Any(e => e.Id == id))
            {
                return NotFound();
            }

            await _service.Update(id, articleDto);

            return NoContent();
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: v1/Articles/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _service.GetById(id);
            if (article == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return NoContent();
        }

    }
}
