using EcommerceApi.Models;

namespace ECommerceAPI.Models
{
  public class ShoppingCartItem
  {
    public Guid Id { get; set; }  // Identificador único do item no carrinho
    public Product Product { get; set; }  // Produto associado ao item
    public int Quantity { get; set; }  // Quantidade do produto no carrinho
    public Order Order { get; set; }  // Relacionamento com o pedido
  }
}