using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}