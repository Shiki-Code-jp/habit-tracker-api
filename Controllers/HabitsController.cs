using Microsoft.AspNetCore.Mvc;
using habit_tracker_api.Models;

namespace habit_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitController : ControllerBase
{
    private static readonly List<Habit> DummyHabits = new()
    {
        new Habit { Id = 1, Title = "C#の勉強", Description = "Web APIの作成を1時間進める", IsCompleted = false },
        new Habit { Id = 2, Title = "運動", Description = "20分間散歩する", IsCompleted = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Habit>> GetHabits()
    {
        return Ok(DummyHabits);
    }

}
