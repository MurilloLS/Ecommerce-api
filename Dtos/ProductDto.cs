using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }

    public class ProductCreateDto
    {
        [Required(ErrorMessage = "The CategoryId field is required.")]
        public Guid CategoryId { get; set; }
       
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 100 characters long.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 1.")]
        public decimal Price { get; set; }
    }

    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 100 characters long.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 1.")]
        public decimal Price { get; set; }
    }

}