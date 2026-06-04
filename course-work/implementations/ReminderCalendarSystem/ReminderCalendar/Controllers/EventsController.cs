using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReminderCalendar.Data;
using ReminderCalendar.Dtos.Events;
using ReminderCalendar.Models;
using System.Security.Claims;

namespace ReminderCalendar.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var events = await _context.Events
            .Where(e => e.UserId == userId)
            .ToListAsync();

        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
        if (!categoryExists)
            return BadRequest("Category not found");

        var ev = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            EventDate = dto.EventDate,
            Priority = dto.Priority,
            IsCompleted = dto.IsCompleted,
            CategoryId = dto.CategoryId,
            UserId = userId
        };

        _context.Events.Add(ev);
        await _context.SaveChangesAsync();

        return Ok(ev);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EventCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (ev == null)
            return NotFound();

        ev.Title = dto.Title;
        ev.Description = dto.Description;
        ev.EventDate = dto.EventDate;
        ev.Priority = dto.Priority;
        ev.IsCompleted = dto.IsCompleted;
        ev.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return Ok(ev);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (ev == null)
            return NotFound();

        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();

        return Ok();
    }
}