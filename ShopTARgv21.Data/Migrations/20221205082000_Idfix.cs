using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARgv21.Data.Migrations
{
    public partial class Idfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Spaceship",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Spaceship",
                newName: "ID");
        }
    }
}
