using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEATHER.API.Migrations
{
    public partial class AddWeatherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "weather");

            migrationBuilder.CreateTable(
                name: "Weather",
                schema: "weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather",
                schema: "weather");
        }
    }
}
