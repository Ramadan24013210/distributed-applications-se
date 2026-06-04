namespace ReminderCalendar.Models;

public class Reminder
{
    public int Id { get; set; }

    public string Message { get; set; }

    public DateTime RemindAt { get; set; }

    public bool IsSent { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; } 
}