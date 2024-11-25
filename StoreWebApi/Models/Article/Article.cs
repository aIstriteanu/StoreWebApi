using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class Article
    {
        public int Id { get; set; }

        [MaxLength(100)] 
        public string Name { get; set; }
        
        public int Inventory { get; set; } = 0;
                
        public decimal Price { get; set; }


        public List<Transaction> Transactions { get; } = [];
        public List<TransactionArticle> TransactionArticles { get; } = [];
    }
}
