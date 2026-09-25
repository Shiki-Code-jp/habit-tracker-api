using Microsoft.EntityFrameworkCore;
using habit_tracker_api.Data; // ← これを追加

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// --- SQLite / EF Core の設定 ---

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextjs",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Next.js の URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Habit Tracker API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowNextjs");

app.UseAuthorization();

app.MapControllers();

app.Run();