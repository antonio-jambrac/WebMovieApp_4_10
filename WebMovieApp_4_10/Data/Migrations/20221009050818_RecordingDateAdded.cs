using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMovieApp_4_10.Data.Migrations
{
    public partial class RecordingDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecordingFinish",
                table: "Movie",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordingStar",
                table: "Movie",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordingFinish",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "RecordingStar",
                table: "Movie");
        }
    }
}
