using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Dtos;
using ECommerceApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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
          .ThenInclude(p => p.Category)
          .ToListAsync();

      var itemDtos = items.Select(item
      =>
      {
        var itemDto = ItemToDto(item);
        itemDto.Product.Category.Products = null;
        itemDto.Product.CategoryId = null;
        return itemDto;
      }).ToList();
      return Ok(itemDtos);
    }

    // GET: api/ShoppingCartItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingCartItemDto>> GetShoppingCartItem(Guid id)
    {
      var item = await _context.ShoppingCartItems
          .Include(sci => sci.Product)
          .ThenInclude(p => p.Category) 
          .FirstOrDefaultAsync(sci => sci.Id == id);

      if (item == null)
      {
        return NotFound();
      }
      
      var itemDto = ItemToDto(item);
      itemDto.Product.Category.Products = null;
      itemDto.Product.CategoryId = null;
      return Ok(itemDto);
    }

    // POST: api/ShoppingCartItem
    [HttpPost]
    public async Task<ActionResult<ShoppingCartItemDto>> PostShoppingCartItem([FromBody] ShoppingCartItemCreateDto shoppingCartItemCreateDto)
    {
      if (shoppingCartItemCreateDto == null)
      {
        return BadRequest("The shoppingCartItemCreateDto field is required.");
      }

      var product = await _context.Products.FindAsync(shoppingCartItemCreateDto.ProductId);
      if (product == null)
      {
        return BadRequest("Invalid ProductId. Product does not exist.");
      }

      var shoppingCart = await _context.ShoppingCarts.FindAsync(shoppingCartItemCreateDto.ShoppingCartId);
      if (shoppingCart == null)
      {
        return BadRequest("Invalid ShoppingCartId. Shopping cart does not exist.");
      }

      var shoppingCartItem = new ShoppingCartItem
      {
        Id = Guid.NewGuid(),
        ProductId = shoppingCartItemCreateDto.ProductId,
        ShoppingCartId = shoppingCartItemCreateDto.ShoppingCartId,
        Quantity = shoppingCartItemCreateDto.Quantity
      };

      _context.ShoppingCartItems.Add(shoppingCartItem);
      await _context.SaveChangesAsync();

      var itemDto = ItemToDto(shoppingCartItem);
      return CreatedAtAction(nameof(GetShoppingCartItem), new { id = itemDto.Id }, itemDto);
    }

    // PUT: api/ShoppingCartItem/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShoppingCartItem(Guid id, [FromBody] ShoppingCartItemCreateDto shoppingCartItemCreateDto)
    {
      if (shoppingCartItemCreateDto == null)
      {
        return BadRequest("The shoppingCartItemCreateDto field is required.");
      }

      var existingItem = await _context.ShoppingCartItems.FindAsync(id);
      if (existingItem == null)
      {
        return NotFound();
      }

      var product = await _context.Products.FindAsync(shoppingCartItemCreateDto.ProductId);
      if (product == null)
      {
        return BadRequest("Invalid ProductId. Product does not exist.");
      }

      var shoppingCart = await _context.ShoppingCarts.FindAsync(shoppingCartItemCreateDto.ShoppingCartId);
      if (shoppingCart == null)
      {
        return BadRequest("Invalid ShoppingCartId. Shopping cart does not exist.");
      }

      existingItem.ProductId = shoppingCartItemCreateDto.ProductId;
      existingItem.ShoppingCartId = shoppingCartItemCreateDto.ShoppingCartId;
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

    // DELETE: api/ShoppingCartItem/5
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
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item), "ShoppingCartItem cannot be null");
      }

      return new ShoppingCartItemDto
      {
        Id = item.Id,
        Product = item.Product != null ? new ProductDto
        {
          Id = item.Product.Id,
          Name = item.Product.Name,
          Price = item.Product.Price,
          Category = item.Product.Category != null ? new CategoryDto
          {
            Id = item.Product.Category.Id,
            Name = item.Product.Category.Name
          } : null
        } : null,
        Quantity = item.Quantity,
        ShoppingCartId = item.ShoppingCartId
      };
    }
  
  }
}
