using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoIngegneriaSoftware.Migrations
{
    public partial class AuthenticationDb_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(128)", maxLength: 128, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ConfirmationToken = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
