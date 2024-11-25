using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class CreateCustomerRq
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }
    }
}
