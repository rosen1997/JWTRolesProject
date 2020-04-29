using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTRolesTestApp.Migrations
{
    public partial class RemoveTokenPropertyFromEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
