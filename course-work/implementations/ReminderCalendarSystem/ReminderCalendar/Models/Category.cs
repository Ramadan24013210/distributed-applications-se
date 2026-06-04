namespace ReminderCalendar.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Color { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
}