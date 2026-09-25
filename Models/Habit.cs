using System.ComponentModel.DataAnnotations;

namespace habit_tracker_api.Models;

public class Habit
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

[StringLength(50)]
    public string Category { get; set; } = "全般";

    public string? ReminderTime { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}