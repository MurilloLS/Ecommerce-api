using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Dtos;
using ECommerceApi.Models;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ShoppingCartController : ControllerBase
  {
    private readonly ECommerceContext _context;

    public ShoppingCartController(ECommerceContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShoppingCartDto>>> GetShoppingCarts()
    {
      var carts = await _context.ShoppingCarts
          .Include(sc => sc.Items)
          .ThenInclude(sci => sci.Product)
          .ToListAsync();

      var cartDtos = carts.Select(ItemToDto).ToList();
      return Ok(cartDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(Guid id)
    {
      var cart = await _context.ShoppingCarts
          .Include(sc => sc.Items)
          .ThenInclude(sci => sci.Product)
          .FirstOrDefaultAsync(sc => sc.Id == id);

      if (cart == null)
      {
        return NotFound();
      }

      return Ok(ItemToDto(cart));
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartDto>> PostShoppingCart(ShoppingCartCreateDto shoppingCartCreateDto)
    {
      var user = await _context.Users.FindAsync(shoppingCartCreateDto.UserId);
      if (user == null)
      {
        return BadRequest("Invalid UserId. User does not exist.");
      }

      var shoppingCart = new ShoppingCart
      {
        Id = Guid.NewGuid(),
        UserId = shoppingCartCreateDto.UserId
      };

      _context.ShoppingCarts.Add(shoppingCart);
      await _context.SaveChangesAsync();

      var cartDto = ItemToDto(shoppingCart);
      return CreatedAtAction(nameof(GetShoppingCart), new { id = cartDto.Id }, cartDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShoppingCart(Guid id, ShoppingCartCreateDto shoppingCartCreateDto)
    {
      var existingCart = await _context.ShoppingCarts.FindAsync(id);
      if (existingCart == null)
      {
        return NotFound();
      }

      existingCart.UserId = shoppingCartCreateDto.UserId;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ShoppingCartExists(id))
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
    public async Task<IActionResult> DeleteShoppingCart(Guid id)
    {
      var cart = await _context.ShoppingCarts.FindAsync(id);
      if (cart == null)
      {
        return NotFound();
      }

      _context.ShoppingCarts.Remove(cart);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ShoppingCartExists(Guid id)
    {
      return _context.ShoppingCarts.Any(e => e.Id == id);
    }

    private ShoppingCartDto ItemToDto(ShoppingCart cart)
    {
      return new ShoppingCartDto
      {
        Id = cart.Id,
        Items = cart.Items.Select(i => new ShoppingCartItemDto
        {
          Id = i.Id,
          ProductId = i.ProductId,
          Product = new ProductDto
          {
            Id = i.Product.Id,
            Name = i.Product.Name,
            Price = i.Product.Price,
            CategoryId = i.Product.CategoryId,
            Category = new CategoryDto
            {
              Id = i.Product.Category.Id,
              Name = i.Product.Category.Name
            }
          },
          Quantity = i.Quantity
        }).ToList()
      };
    }
  }
}
