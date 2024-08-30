using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Data;
using ECommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Dtos;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly ECommerceContext _context;

    public CategoryController(ECommerceContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
      var categories = await _context.Categories
        .Select(category => new CategoryDto
        {
          Id = category.Id,
          Name = category.Name,
          Products = category.Products.Select(product => new ProductDto
          {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Category = new CategoryDto
            {
              Id = product.Category.Id,
              Name = product.Category.Name
            }
          }).ToList()
        })
        .ToListAsync();
      return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
    {
      var category = await _context
      .Categories
      .Include(c => c.Products)
      .FirstOrDefaultAsync(c => c.Id == id);

      if (category == null)
      {
        return NotFound();
      }

      var categoryDto = new CategoryDto
      {
        Id = category.Id,
        Name = category.Name,
        Products = category.Products
        .Select(x => new ProductDto
        {
          Id = x.Id,
          Name = x.Name,
          Price = x.Price,
          CategoryId = x.CategoryId,
          Category = new CategoryDto
          {
            Id = x.Category.Id,
            Name = x.Category.Name,
          }
        }).ToList()
      };

      return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
      var category = new Category
      {
        Id = Guid.NewGuid(),
        Name = categoryCreateUpdateDto.Name
      };

      _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      var categoryDto = new CategoryDto
      {
        Id = category.Id,
        Name = category.Name,
        Products = new List<ProductDto>()
      };
  
      return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(Guid id, CategoryCreateUpdateDto categoryUpdateDto)
    {
      var existingCategory = await _context.Categories.FindAsync(id);
      if (existingCategory == null)
      {
        return NotFound();
      }

      existingCategory.Name = categoryUpdateDto.Name;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CategoryExists(id))
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
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
      var category = await _context.Categories.FindAsync(id);
      if (category == null)
      {
        return NotFound();
      }

      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool CategoryExists(Guid id)
    {
      return _context.Categories.Any(e => e.Id == id);
    }
  }
}