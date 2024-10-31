using System.ComponentModel.DataAnnotations;

namespace Actividadenclase.DTOs
{
    public class CreateProductDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
