using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMovieApp_4_10.Data.Migrations
{
    public partial class movieGenresAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieID",
                table: "MovieGenres",
                column: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");
        }
    }
}
