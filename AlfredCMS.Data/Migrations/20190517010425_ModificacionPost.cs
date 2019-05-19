using Microsoft.EntityFrameworkCore.Migrations;

namespace AlfredCMS.Data.Migrations
{
    public partial class ModificacionPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Posts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Posts");
        }
    }
}
