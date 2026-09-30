using System.ComponentModel.DataAnnotations;

namespace habit_tracker_api.DTOs;

public class UpdateHabitDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public string? ReminderTime { get; set; }
}