using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Dtos;
using ECommerceApi.Models;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ShoppingCartItemController : ControllerBase
  {
    private readonly ECommerceContext _context;

    public ShoppingCartItemController(ECommerceContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> GetShoppingCartItems()
    {
      var items = await _context.ShoppingCartItems
          .Include(sci => sci.Product)
          .ToListAsync();

      var itemDtos = items.Select(ItemToDto).ToList();
      return Ok(itemDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingCartItemDto>> GetShoppingCartItem(Guid id)
    {
      var item = await _context.ShoppingCartItems
          .Include(sci => sci.Product)
          .FirstOrDefaultAsync(sci => sci.Id == id);

      if (item == null)
      {
        return NotFound();
      }

      return Ok(ItemToDto(item));
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartItemDto>> PostShoppingCartItem(ShoppingCartItemCreateDto shoppingCartItemCreateDto)
    {
      var product = await _context.Products.FindAsync(shoppingCartItemCreateDto.ProductId);
      if (product == null)
      {
        return BadRequest("Invalid ProductId. Product does not exist.");
      }

      var shoppingCartItem = new ShoppingCartItem
      {
        Id = Guid.NewGuid(),
        ProductId = shoppingCartItemCreateDto.ProductId,
        Quantity = shoppingCartItemCreateDto.Quantity
      };

      _context.ShoppingCartItems.Add(shoppingCartItem);
      await _context.SaveChangesAsync();

      var itemDto = ItemToDto(shoppingCartItem);
      return CreatedAtAction(nameof(GetShoppingCartItem), new { id = itemDto.Id }, itemDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShoppingCartItem(Guid id, ShoppingCartItemCreateDto shoppingCartItemCreateDto)
    {
      var existingItem = await _context.ShoppingCartItems.FindAsync(id);
      if (existingItem == null)
      {
        return NotFound();
      }

      existingItem.ProductId = shoppingCartItemCreateDto.ProductId;
      existingItem.Quantity = shoppingCartItemCreateDto.Quantity;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ShoppingCartItemExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShoppingCartItem(Guid id)
    {
      var item = await _context.ShoppingCartItems.FindAsync(id);
      if (item == null)
      {
        return NotFound();
      }

      _context.ShoppingCartItems.Remove(item);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ShoppingCartItemExists(Guid id)
    {
      return _context.ShoppingCartItems.Any(e => e.Id == id);
    }

    private ShoppingCartItemDto ItemToDto(ShoppingCartItem item)
    {
      return new ShoppingCartItemDto
      {
        Id = item.Id,
        ProductId = item.ProductId,
        Product = new ProductDto
        {
          Id = item.Product.Id,
          Name = item.Product.Name,
          Price = item.Product.Price,
          CategoryId = item.Product.CategoryId,
          Category = new CategoryDto
          {
            Id = item.Product.Category.Id,
            Name = item.Product.Category.Name
          }
        },
        Quantity = item.Quantity
      };
    }
  }
}
