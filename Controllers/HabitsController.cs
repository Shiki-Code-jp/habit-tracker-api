using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using habit_tracker_api.Data;
using habit_tracker_api.Models;

namespace habit_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitController : ControllerBase
{
    private readonly AppDbContext _context;

    public HabitController(AppDbContext context)
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
    public async Task<ActionResult<Habit>> CreateHabit(Habit habit)
    {
        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHabits), new { id = habit.Id }, habit);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHabit(int id, Habit habit)
    {
        if (id != habit.Id)
        {
            return BadRequest();
        }

        _context.Entry(habit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Habits.Any(e => e.Id == id))
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
