using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTRolesTestApp.Migrations
{
    public partial class ADDPasswordHash_PasswordSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "LoginInfos");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "LoginInfos",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "LoginInfos",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.UpdateData(
                table: "LoginInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] {  }, new byte[] {  } });

            migrationBuilder.UpdateData(
                table: "LoginInfos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] {  }, new byte[] {  } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "LoginInfos");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "LoginInfos");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "LoginInfos",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LoginInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "rosen");

            migrationBuilder.UpdateData(
                table: "LoginInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "neli");
        }
    }
}
