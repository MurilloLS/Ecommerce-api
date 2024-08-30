

using ECommerceApi.Data;
using ECommerceApi.Dtos;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ECommerceContext _context;

    public ProductsController(ECommerceContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
      var products = await _context.Products
          .Include(p => p.Category)
          .Select(p => new ProductDto
          {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryId = p.CategoryId,
            Category = new CategoryDto
            {
              Name = p.Category.Name,
              Id = p.Category.Id,
            }
          })
          .ToListAsync();

      return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
      var product = await _context.Products
      .Include(p => p.Category)
      .Where(p => p.Id == id)
      .Select(p => new ProductDto
      {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        CategoryId = p.CategoryId,
        Category = new CategoryDto
        {
          Name = p.Category.Name,
          Id = p.Category.Id,
        }
      })
      .FirstOrDefaultAsync();

      if (product == null)
      {
        return NotFound();
      }

      return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductCreateUpdateDto productDto)
    {
      var product = new Product
      {
        Id = Guid.NewGuid(),
        Name = productDto.Name,
        Price = productDto.Price,
        CategoryId = Guid.NewGuid()
      };

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      var productDtoResult = new ProductDto
      {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        CategoryId = product.CategoryId,
        Category = new CategoryDto
        {
          Id = (await _context.Categories.FindAsync(product.CategoryId))?.Id ?? default,
          Name = (await _context.Categories.FindAsync(product.CategoryId))?.Name
        }
      };

      return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDtoResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, ProductCreateUpdateDto productDto)
    {
      if (id == Guid.Empty || productDto == null)
      {
        return BadRequest();
      }

      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      
      product.Name = productDto.Name;
      product.Price = productDto.Price;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductExists(id))
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
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProductExists(Guid id)
    {
      return _context.Products.Any(e => e.Id == id);
    }
  }
}