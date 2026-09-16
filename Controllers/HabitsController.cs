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

    [HttpPost]
    public async Task<ActionResult<Habit>> CreateHabit(Habit habit)
    {
        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHabits), new { id = habit.Id }, habit);
    }

}
