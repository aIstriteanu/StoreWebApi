using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(15)]
        public string Type { get; set; }
    }
}
