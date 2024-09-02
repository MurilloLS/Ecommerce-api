using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceApi.Dtos
{
  public class CategoryDto
  {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid? Id { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Name { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
  }

  public class CategoryCreateUpdateDto
  {
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 100 characters long.")]
    public string? Name { get; set; }
  }
}