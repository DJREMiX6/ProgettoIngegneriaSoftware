using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoIngegneriaSoftware.Migrations
{
    public partial class UpdatedLoginTokenAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "LoginTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.RenameColumn(
                name: "StoredPassword",
                newName: "PasswordHash",
                table: "Users");
            migrationBuilder.RenameColumn(
                name: "StoredSalt",
                newName: "Salt",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "LoginTokens");
        }
    }
}
