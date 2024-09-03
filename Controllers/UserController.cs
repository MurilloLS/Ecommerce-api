using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Dtos;
using ECommerceApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly ECommerceContext _context;

    public UserController(ECommerceContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
      var users = await _context.Users
          .Include(u => u.ShoppingCart)
          .ThenInclude(sc => sc.Items)
          .ThenInclude(sci => sci.Product)
          .ToListAsync();

      var userDto = users.Select(u => new UserDto
      {
        Id = u.Id,
        Name = u.Name,
        Email = u.Email,
        ShoppingCart = u.ShoppingCart != null ? new ShoppingCartDto
        {
          Id = u.ShoppingCart.Id,
          Items = u.ShoppingCart.Items != null ? u.ShoppingCart.Items
                  .Select(sc => new ShoppingCartItemDto
                  {
                    Product = sc.Product != null ? new ProductDto
                    {
                      Name = sc.Product.Name,
                      Price = sc.Product.Price,
                      Category = sc.Product.Category != null ? new CategoryDto
                      {
                        Id = sc.Product.Category.Id,
                        Name = sc.Product.Category.Name
                      } : null
                    } : null,
                    Quantity = sc.Quantity
                  }).ToList() : new List<ShoppingCartItemDto>()
        } : null
      }).ToList();

      return Ok(userDto); // Certifique-se de retornar `userDto` aqui
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
      var user = await _context.Users
          .Include(u => u.ShoppingCart)
          .ThenInclude(sc => sc.Items)
          .ThenInclude(sci => sci.Product)
          .FirstOrDefaultAsync(u => u.Id == id);

      if (user == null)
      {
        return NotFound();
      }

      // Construção direta do DTO
      var userDto = new UserDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        ShoppingCart = user.ShoppingCart != null ? new ShoppingCartDto
        {
          Id = user.ShoppingCart.Id,
          Items = user.ShoppingCart.Items?
                  .Where(i => i != null && i.Product != null) // Verifica se i e Product não são null
                  .Select(i => new ShoppingCartItemDto
                  {
                    Product = new ProductDto
                    {
                      Name = i.Product.Name,
                      Price = i.Product.Price
                    },
                    Quantity = i.Quantity
                  }).ToList()
        } : null
      };

      return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserCreateDto userCreateDto)
    {
      var user = new User
      {
        Id = Guid.NewGuid(),
        Name = userCreateDto.Name,
        Email = userCreateDto.Email
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var userDto = ItemToDto(user);
      return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, UserUpdateDto userUpdateDto)
    {
      var existingUser = await _context.Users.FindAsync(id);
      if (existingUser == null)
      {
        return NotFound();
      }

      existingUser.Name = userUpdateDto.Name;
      existingUser.Email = userUpdateDto.Email;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
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
    public async Task<IActionResult> DeleteUser(Guid id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return NoContent();
    }
    private bool UserExists(Guid id)
    {
      return _context.Users.Any(e => e.Id == id);
    }
    private UserDto ItemToDto(User user)
    {
      return new UserDto
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        ShoppingCart = user.ShoppingCart != null ? new ShoppingCartDto
        {
          Id = user.ShoppingCart.Id,
          Items = user.ShoppingCart.Items.Select(i => new ShoppingCartItemDto
          {
            Id = i.Id,
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
        } : null
      };
    }

  }
}
