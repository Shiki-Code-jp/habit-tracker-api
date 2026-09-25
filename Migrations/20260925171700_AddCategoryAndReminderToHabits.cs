using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace habit_tracker_api.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndReminderToHabits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Habits",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Habits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReminderTime",
                table: "Habits",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "ReminderTime",
                table: "Habits");
        }
    }
}
