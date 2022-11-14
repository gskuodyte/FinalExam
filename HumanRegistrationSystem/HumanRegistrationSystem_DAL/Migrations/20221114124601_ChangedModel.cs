using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanRegistrationSystem_DAL.Migrations
{
    public partial class ChangedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Humans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Humans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
