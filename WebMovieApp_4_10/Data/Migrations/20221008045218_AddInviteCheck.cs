using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMovieApp_4_10.Data.Migrations
{
    public partial class AddInviteCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "MovieInvites",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieInvites_ApplicationUserId",
                table: "MovieInvites",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieInvites_MovieID",
                table: "MovieInvites",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieInvites_AspNetUsers_ApplicationUserId",
                table: "MovieInvites",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieInvites_Movie_MovieID",
                table: "MovieInvites",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieInvites_AspNetUsers_ApplicationUserId",
                table: "MovieInvites");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieInvites_Movie_MovieID",
                table: "MovieInvites");

            migrationBuilder.DropIndex(
                name: "IX_MovieInvites_ApplicationUserId",
                table: "MovieInvites");

            migrationBuilder.DropIndex(
                name: "IX_MovieInvites_MovieID",
                table: "MovieInvites");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "MovieInvites",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
