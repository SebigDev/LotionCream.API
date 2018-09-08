using Microsoft.EntityFrameworkCore.Migrations;

namespace LotionCream.API.Migrations
{
    public partial class dateOfBirthModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataOfBirth",
                table: "Users",
                newName: "DateOfBirth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "DataOfBirth");
        }
    }
}
