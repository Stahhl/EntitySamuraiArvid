using Microsoft.EntityFrameworkCore.Migrations;

namespace MySamurai.Data.Migrations
{
    public partial class AddedNameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Samurais",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Samurais");
        }
    }
}
