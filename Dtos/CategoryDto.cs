using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

namespace ECommerceApi.Dtos
{
  public class CategoryDto
  {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
  }

  public class CategoryCreateUpdateDto
  {
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 100 characters long.")]
    public string? Name { get; set; }
  }
}