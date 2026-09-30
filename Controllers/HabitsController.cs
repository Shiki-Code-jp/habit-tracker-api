using System.Security.Claims;
using habit_tracker_api.Data;
using habit_tracker_api.DTOs;
using habit_tracker_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace habit_tracker_api.Controllers;

[Authorize] // 認証が必要なエンドポイントにする
[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly AppDbContext _context;

    public HabitsController(AppDbContext context)
    {
        _context = context;
    }

    // ログインユーザーの ID をトークンから取得するヘルパーメソッド
    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }

    // GET: api/Habits (自分の習慣のみ取得)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Habit>>> GetHabits()
    {
        var userId = GetCurrentUserId();
        return await _context.Habits
            .Where(h => h.UserId == userId)
            .ToListAsync();
    }

    // GET: api/Habits/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Habit>> GetHabit(int id)
    {
        var userId = GetCurrentUserId();
        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null)
        {
            return NotFound();
        }

        return habit;
    }

    // POST: api/Habits
    [HttpPost]
    public async Task<ActionResult<Habit>> CreateHabit(CreateHabitDto dto)
    {
        var userId = GetCurrentUserId();

        var habit = new Habit
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            ReminderTime = dto.ReminderTime,
            UserId = userId // 自動で自分の UserId を設定
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHabit), new { id = habit.Id }, habit);
    }

    // PUT: api/Habits/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHabit(int id, UpdateHabitDto dto)
    {
        var userId = GetCurrentUserId();
        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null)
        {
            return NotFound();
        }

        habit.Title = dto.Title;
        habit.Description = dto.Description;
        habit.Category = dto.Category;
        habit.IsCompleted = dto.IsCompleted;
        habit.ReminderTime = dto.ReminderTime;

        if (dto.IsCompleted && habit.CompletedAt == null)
        {
            habit.CompletedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Habits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabit(int id)
    {
        var userId = GetCurrentUserId();
        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null)
        {
            return NotFound();
        }

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}