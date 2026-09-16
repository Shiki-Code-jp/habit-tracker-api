using Microsoft.EntityFrameworkCore;
using habit_tracker_api.Models;

namespace habit_tracker_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DB内の「Habits」テーブルを表す
    public DbSet<Habit> Habits => Set<Habit>();
}