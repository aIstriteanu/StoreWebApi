using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

    }
}
