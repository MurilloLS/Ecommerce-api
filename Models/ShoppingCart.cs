namespace ECommerceApi.Models
{
  public class ShoppingCart
  {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
  }
}