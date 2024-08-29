using System;
using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

namespace ECommerceApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}