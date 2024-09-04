using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Dtos;
using ECommerceApi.Models;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly ECommerceContext _context;

    public ProductController(ECommerceContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
      var products = await _context.Products
          .Include(p => p.Category)
          .ToListAsync();

      var productDtos = products.Select(ItemToDto).ToList();
      ClearCategoryReferences(productDtos);

      return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
      var product = await _context.Products
          .Include(p => p.Category)
          .FirstOrDefaultAsync(p => p.Id == id);

      if (product == null)
      {
        return NotFound();
      }

      var productDto = ItemToDto(product);
      ClearCategoryReferences(new List<ProductDto> { productDto });

      return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductCreateDto productCreateDto)
    {
      var category = await _context.Categories.FindAsync(productCreateDto.CategoryId);
      if (category == null)
      {
        return BadRequest("Invalid CategoryId. Category does not exist.");
      }

      var product = new Product
      {
        Id = Guid.NewGuid(),
        Name = productCreateDto.Name,
        Price = productCreateDto.Price,
        CategoryId = productCreateDto.CategoryId
      };

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      var productDto = ItemToDto(product);
      return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, ProductUpdateDto productUpdateDto)
    {
      var existingProduct = await _context.Products.FindAsync(id);
      if (existingProduct == null)
      {
        return NotFound();
      }

      existingProduct.Name = productUpdateDto.Name;
      existingProduct.Price = productUpdateDto.Price;

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

    private ProductDto ItemToDto(Product product)
    {
      return new ProductDto
      {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Category = product.Category != null ? new CategoryDto
        {
          Id = product.Category.Id,
          Name = product.Category.Name
        } : null
      };
    }

    private void ClearCategoryReferences(IEnumerable<ProductDto> productDtos)
    {
      foreach (var productDto in productDtos)
      {
        if (productDto.Category != null)
        {
          productDto.Category.Products = null;
        }
      }
    }
  }
}
