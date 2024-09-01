using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Dtos
{
    public class ShoppingCartItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartItemCreateDto
    {
        [Required(ErrorMessage = "The ProductId field is required.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }

    // public class ShoppingCartItemUpdateDto
    // {
    //     [Required(ErrorMessage = "The Quantity field is required.")]
    //     [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    //     public int Quantity { get; set; }
    // }
}