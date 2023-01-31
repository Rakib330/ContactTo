using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSMS.Migrations
{
    public partial class addreceiverdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rlnreceiverGroups_Receiver_receiverId",
                table: "rlnreceiverGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receiver",
                table: "Receiver");

            migrationBuilder.RenameTable(
                name: "Receiver",
                newName: "receivers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_receivers",
                table: "receivers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rlnreceiverGroups_receivers_receiverId",
                table: "rlnreceiverGroups",
                column: "receiverId",
                principalTable: "receivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rlnreceiverGroups_receivers_receiverId",
                table: "rlnreceiverGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_receivers",
                table: "receivers");

            migrationBuilder.RenameTable(
                name: "receivers",
                newName: "Receiver");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receiver",
                table: "Receiver",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rlnreceiverGroups_Receiver_receiverId",
                table: "rlnreceiverGroups",
                column: "receiverId",
                principalTable: "Receiver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
