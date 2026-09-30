using System.ComponentModel.DataAnnotations;

namespace habit_tracker_api.DTOs;

public class CreateHabitDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string? ReminderTime { get; set; }
}