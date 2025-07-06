using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futebol_Semanal_API.Migrations
{
    public partial class AddFotoUrlToTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Times",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Times");
        }
    }
}
