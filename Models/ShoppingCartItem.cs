using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}