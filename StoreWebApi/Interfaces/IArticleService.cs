using StoreWebApi.Models;

namespace StoreWebApi.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAll();
        Task<Article?> GetById(int id);
        Task<ArticleDto> Create(CreateArticleRq request);
        Task Update(int id, ArticleDto request);
        Task Delete(int id);
    }
}
