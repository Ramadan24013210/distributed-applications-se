using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReminderCalendar.Data;
using ReminderCalendar.Dtos.Reminders;
using ReminderCalendar.Models;
using System.Security.Claims;

namespace ReminderCalendar.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RemindersController : ControllerBase
{
    private readonly AppDbContext _context;

    public RemindersController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var reminders = await _context.Reminders
            .Where(r => r.UserId == userId)
            .ToListAsync();

        return Ok(reminders);
    }


    [HttpPost]
    public async Task<IActionResult> Create(ReminderCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var eventExists = await _context.Events.AnyAsync(e => e.Id == dto.EventId);
        if (!eventExists)
            return BadRequest("Event not found");

        var reminder = new Reminder
        {
            Message = dto.Message,
            RemindAt = dto.RemindAt,
            EventId = dto.EventId,
            UserId = userId,
            IsSent = false
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        return Ok(reminder);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ReminderCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var reminder = await _context.Reminders.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
        if (reminder == null)
            return NotFound();

        reminder.Message = dto.Message;
        reminder.RemindAt = dto.RemindAt;
        reminder.EventId = dto.EventId;

        await _context.SaveChangesAsync();

        return Ok(reminder);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var reminder = await _context.Reminders.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
        if (reminder == null)
            return NotFound();

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync();

        return Ok();
    }
}