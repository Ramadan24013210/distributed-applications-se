using Microsoft.EntityFrameworkCore;
using ReminderCalendar.Models;

namespace ReminderCalendar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Reminder> Reminders => Set<Reminder>();
}