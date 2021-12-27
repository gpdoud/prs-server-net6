using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prs_server_net6.Migrations
{
    public partial class Addedapikeytouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apikey",
                table: "Users",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apikey",
                table: "Users");
        }
    }
}
