using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class PaymentDto
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Amount needs to be equal or greater than {1}")]
        public decimal Amount { get; set; }

        [Required, StringLength(15)]
        public string Type { get; set; }

    }
}
