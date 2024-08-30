using System;
using System.ComponentModel.DataAnnotations;
using ECommerceApi.Dtos;
using ECommerceApi.Models;

namespace ECommerceApi.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }

    public class ProductCreateUpdateDto
    {
        [Required]
        public string? Name { get; set; }
        public decimal Price { get; set; } = 0;
    }

}