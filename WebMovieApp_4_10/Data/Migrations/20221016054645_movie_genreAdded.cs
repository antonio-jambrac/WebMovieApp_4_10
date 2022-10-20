using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMovieApp_4_10.Data.Migrations
{
    public partial class movie_genreAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movie_MovieID",
                table: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenres_MovieID",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "MovieGenres");

            migrationBuilder.CreateTable(
                name: "Movie_Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    MovieGenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Genres_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movie_Genres_MovieGenres_MovieGenresId",
                        column: x => x.MovieGenresId,
                        principalTable: "MovieGenres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Genres_MovieGenresId",
                table: "Movie_Genres",
                column: "MovieGenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Genres_MovieId",
                table: "Movie_Genres",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie_Genres");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "MovieGenres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieID",
                table: "MovieGenres",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movie_MovieID",
                table: "MovieGenres",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID");
        }
    }
}
