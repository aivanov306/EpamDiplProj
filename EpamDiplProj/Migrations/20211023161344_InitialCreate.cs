using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatlesTracksDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItunesTrackslist",
                columns: table => new
                {
                    trackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    collectionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trackName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    collectionPrice = table.Column<double>(type: "float", nullable: false),
                    trackPrice = table.Column<double>(type: "float", nullable: false),
                    primaryGenreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trackCount = table.Column<int>(type: "int", nullable: false),
                    trackNumber = table.Column<int>(type: "int", nullable: false),
                    releaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    artistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItunesTrackslist", x => x.trackId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItunesTrackslist");
        }
    }
}
