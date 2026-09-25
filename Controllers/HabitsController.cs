using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using habit_tracker_api.Data;
using habit_tracker_api.Models;

namespace habit_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly AppDbContext _context;

    public HabitsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Habit>>> GetHabits()
    {
        return await _context.Habits.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Habit>> GetHabit(int id)
    {
        var habit = await _context.Habits.FindAsync(id);

        if (habit == null)
        {
            return NotFound();
        }

        return habit;
    }

    [HttpPost]
    public async Task<ActionResult<Habit>> CreateHabit(CreateHabitDto dto)
    {
        var habit = new Habit
        {
            Title = dto.Title.Trim(),
            Category = string.IsNullOrWhiteSpace(dto.Category) ? "全般" : dto.Category.Trim(),
            ReminderTime = dto.ReminderTime,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHabit), new { id = habit.Id }, habit);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHabit(int id, UpdateHabitDto dto)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit == null)
        {
            return NotFound();
        }

        habit.Title = dto.Title.Trim();
        habit.Category = string.IsNullOrWhiteSpace(dto.Category) ? "全般" : dto.Category.Trim();
        habit.ReminderTime = dto.ReminderTime;

        if (!habit.IsCompleted && dto.IsCompleted)
        {
            habit.CompletedAt = DateTime.UtcNow;
        }
        else if (!dto.IsCompleted)
        {
            habit.CompletedAt = null;
        }

        habit.IsCompleted = dto.IsCompleted;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Habits.AnyAsync(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabit(int id)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit == null)
        {
            return NotFound();
        }

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
