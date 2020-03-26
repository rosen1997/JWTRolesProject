using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTRolesTestApp.Migrations
{
    public partial class AddEmployeeToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Employees");
        }
    }
}
