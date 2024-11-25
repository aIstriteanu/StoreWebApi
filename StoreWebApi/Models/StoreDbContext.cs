using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace StoreWebApi.Models
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionArticle> TransactionArticles { get; set; }


        public StoreDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlite($"Data Source=store.db") // create database file directly inside the project folder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TransactionArticle>().HasKey(t => new { t.TransactionId, t.ArticleId });
            modelBuilder.Entity<Transaction>()
                        .HasMany(e => e.Articles)
                        .WithMany(e => e.Transactions)
                        .UsingEntity<TransactionArticle>();
        }
    }

}
