namespace ReminderCalendar.Models;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime EventDate { get; set; }

    public int Priority { get; set; }

    public bool IsCompleted { get; set; }

    public int CategoryId { get; set; }

    public int UserId { get; set; }   
}