using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMovieApp_4_10.Data.Migrations
{
    public partial class addedPublicToNavigationalProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MovieID",
                table: "AspNetUsers",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Movie_MovieID",
                table: "AspNetUsers",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Movie_MovieID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MovieID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "AspNetUsers");
        }
    }
}
