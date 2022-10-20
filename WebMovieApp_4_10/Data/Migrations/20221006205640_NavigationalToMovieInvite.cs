using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMovieApp_4_10.Data.Migrations
{
    public partial class NavigationalToMovieInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieInviteId",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieInviteId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_MovieInviteId",
                table: "Movie",
                column: "MovieInviteId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MovieInviteId",
                table: "AspNetUsers",
                column: "MovieInviteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MovieInvites_MovieInviteId",
                table: "AspNetUsers",
                column: "MovieInviteId",
                principalTable: "MovieInvites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_MovieInvites_MovieInviteId",
                table: "Movie",
                column: "MovieInviteId",
                principalTable: "MovieInvites",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MovieInvites_MovieInviteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_MovieInvites_MovieInviteId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_MovieInviteId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MovieInviteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MovieInviteId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MovieInviteId",
                table: "AspNetUsers");
        }
    }
}
