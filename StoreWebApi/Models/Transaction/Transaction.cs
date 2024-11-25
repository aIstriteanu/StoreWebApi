namespace StoreWebApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }


        public Customer Customer { get; set; }

        public List<Payment> Payments { get; set; }

        public List<Article> Articles { get; } = [];
        public List<TransactionArticle> TransactionArticles { get; } = [];

    }
}
