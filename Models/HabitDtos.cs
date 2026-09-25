using System.ComponentModel.DataAnnotations;

namespace habit_tracker_api.Models
{
    public class CreateHabitDto
    {
        [Required(ErrorMessage = "習慣のタイトルは必須です。")]
        [StringLength(100, ErrorMessage ="タイトルは100文字以内で入力してください。")]
        public string Title { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "カテゴリは50文字以内で入力してください。")]
        public string Category { get; set; } ="全般";
        public string? ReminderTime { get; set; }
    }

    public class UpdateHabitDto
    {
        [Required(ErrorMessage = "習慣のタイトルは必須です。")]
        [StringLength(100, ErrorMessage ="タイトルは100文字以内で入力してください。")]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        [StringLength(50, ErrorMessage = "カテゴリは50文字以内で入力してください。")]
        public string Category { get; set; } = "全般";

        public string? ReminderTime { get; set; }
    }
}