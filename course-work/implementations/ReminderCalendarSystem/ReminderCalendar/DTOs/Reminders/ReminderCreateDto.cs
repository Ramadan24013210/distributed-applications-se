namespace ReminderCalendar.Dtos.Reminders;

public class ReminderCreateDto
{
    public string Message { get; set; }
    public DateTime RemindAt { get; set; }
    public int EventId { get; set; }
}