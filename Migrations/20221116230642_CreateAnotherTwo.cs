using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalShelter.Migrations
{
    public partial class CreateAnotherTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");
        }
    }
}
