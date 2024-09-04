using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceApi.Dtos
{
  public class ShoppingCartDto
  {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ICollection<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public UserDto? User { get; set; }
  }

  public class ShoppingCartCreateDto
  {
    [Required]
    public Guid UserId { get; set; }
  }
}