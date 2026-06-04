using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReminderCalendar.Data;
using ReminderCalendar.Dtos.Categories;
using ReminderCalendar.Models;
using System.Security.Claims;

namespace ReminderCalendar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Categories.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            Color = dto.Color,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Ok();
    }
}