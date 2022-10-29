using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class usuarioseedd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Discriminator", "Email", "HashedPassword", "PasswordSalt", "UserName" },
                values: new object[] { 50, "Usuario", "user123@example.com", new byte[] { 153, 148, 216, 121, 132, 166, 219, 84, 199, 74, 223, 21, 206, 104, 41, 80, 159, 33, 184, 203, 104, 1, 107, 181, 246, 180, 162, 144, 178, 220, 202, 145, 188, 224, 218, 142, 17, 160, 124, 210, 223, 123, 193, 132, 59, 118, 174, 129, 190, 74, 110, 243, 237, 235, 225, 237, 67, 22, 126, 213, 210, 13, 213, 92 }, new byte[] { 226, 213, 193, 138, 196, 8, 96, 194, 171, 33, 34, 161, 114, 134, 224, 87, 210, 54, 215, 215, 180, 143, 244, 68, 68, 7, 132, 220, 118, 30, 182, 96, 127, 135, 107, 29, 176, 100, 109, 67, 237, 72, 200, 254, 125, 115, 21, 155, 69, 148, 49, 60, 45, 142, 47, 78, 186, 3, 151, 191, 22, 250, 187, 174, 220, 84, 250, 240, 126, 220, 35, 83, 240, 91, 108, 2, 84, 50, 37, 33, 200, 186, 79, 248, 130, 166, 52, 98, 65, 30, 48, 48, 161, 159, 240, 95, 79, 17, 82, 156, 75, 163, 225, 235, 147, 203, 10, 229, 132, 225, 114, 15, 15, 38, 252, 103, 191, 30, 128, 26, 226, 67, 145, 199, 151, 3, 136, 22 }, "user123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: 50);
        }
    }
}
