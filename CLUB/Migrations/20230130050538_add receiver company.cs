using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSMS.Migrations
{
    public partial class addreceivercompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "company",
                table: "receivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "receivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "designation",
                table: "receivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profession",
                table: "receivers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "company",
                table: "receivers");

            migrationBuilder.DropColumn(
                name: "department",
                table: "receivers");

            migrationBuilder.DropColumn(
                name: "designation",
                table: "receivers");

            migrationBuilder.DropColumn(
                name: "profession",
                table: "receivers");
        }
    }
}
