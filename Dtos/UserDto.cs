using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Dtos
{
  public class UserDto
  {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public ShoppingCartDto? ShoppingCart { get; set; }
  }

  public class UserCreateDto
  {
    [Required(ErrorMessage = "The Name field is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress]
    public string? Email { get; set; }
  }

  public class UserUpdateDto
  {
    [Required(ErrorMessage = "The Name field is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress]
    public string? Email { get; set; }
  }
}