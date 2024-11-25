using Microsoft.EntityFrameworkCore;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;

namespace StoreWebApi.Service
{
    public class ArticleService : IArticleService
    {
        private readonly StoreDbContext _dbContext;

        public ArticleService(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Article>> GetAll()
        {
            return await _dbContext.Articles.AsNoTracking().ToListAsync();
        }

        public async Task<Article?> GetById(int id)
        {
            return await _dbContext.Articles.FindAsync(id);
        }

        public async Task<ArticleDto> Create(CreateArticleRq articleRq)
        {
            var article = new Article()
            {
                Name = articleRq.Name,
                Inventory = articleRq.Inventory,
                Price = articleRq.Price
            };

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            var art = new ArticleDto()
            {
                Id = article.Id,
                Name = article.Name,
                Inventory = article.Inventory,
                Price = article.Price
            };

            return art;
        }
        
        public async Task Update(int id, ArticleDto articleRq)
        {
            var article = new Article()
            {
                Id = articleRq.Id,
                Name = articleRq.Name,
                Inventory = articleRq.Inventory,
                Price = articleRq.Price
            };

            _dbContext.Entry(article).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _dbContext.Articles.FindAsync(id);
            _dbContext.Articles.Remove(item);
            await _dbContext.SaveChangesAsync(); ;
        }
    }
}
