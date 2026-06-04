namespace ReminderCalendar.Dtos.Events;

public class EventCreateDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDate { get; set; }
    public int Priority { get; set; }
    public bool IsCompleted { get; set; }
    public int CategoryId { get; set; }
}