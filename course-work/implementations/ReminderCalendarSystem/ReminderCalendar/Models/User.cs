using System.ComponentModel.DataAnnotations;


namespace ReminderCalendar.Models
{
    public class User
    {

        public int Id { get; set; } 

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}