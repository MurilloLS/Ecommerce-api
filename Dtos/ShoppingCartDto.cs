using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Dtos
{
  public class ShoppingCartDto
  {
    public Guid Id { get; set; }
    public ICollection<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
  }

  public class ShoppingCartCreateDto
  {
    [Required]
    public Guid UserId { get; set; }
  }
}