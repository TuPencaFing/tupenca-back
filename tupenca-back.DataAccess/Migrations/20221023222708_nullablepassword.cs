using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class nullablepassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Personas",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<byte[]>(
                name: "HashedPassword",
                table: "Personas",
                type: "varbinary(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(64)",
                oldMaxLength: 64);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Personas",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "HashedPassword",
                table: "Personas",
                type: "varbinary(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(64)",
                oldMaxLength: 64,
                oldNullable: true);
        }
    }
}
