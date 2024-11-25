using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class CreateArticleRq
    {

        [Required, StringLength(100)] 
        public string Name { get; set; }
        
        public int Inventory { get; set; } = 0;

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Price needs to be equal or greater than {1}")]
        public decimal Price { get; set; }
    }
}
