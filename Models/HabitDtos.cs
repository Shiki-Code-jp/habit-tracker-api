using System.ComponentModel.DataAnnotations;

namespace habit_tracker_api.Models
{
    public class CreateHabitDto
    {
        [Required(ErrorMessage = "習慣のタイトルは必須です。")]
        [StringLength(100, ErrorMessage ="タイトルは100文字以内で入力してください。")]
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateHabitDto
    {
        [Required(ErrorMessage = "習慣のタイトルは必須です。")]
        [StringLength(100, ErrorMessage ="タイトルは100文字以内で入力してください。")]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}