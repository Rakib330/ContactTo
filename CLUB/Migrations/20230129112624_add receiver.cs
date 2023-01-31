using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSMS.Migrations
{
    public partial class addreceiver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    mobile = table.Column<string>(nullable: true),
                    receiverCode = table.Column<string>(nullable: true),
                    receiverType = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "receiverGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receiverGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sendHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    number = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    text = table.Column<string>(nullable: true),
                    receiverId = table.Column<int>(nullable: true),
                    groupId = table.Column<int>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sendHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rlnreceiverGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    receiverGroupId = table.Column<int>(nullable: true),
                    receiverId = table.Column<int>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rlnreceiverGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rlnreceiverGroups_receiverGroups_receiverGroupId",
                        column: x => x.receiverGroupId,
                        principalTable: "receiverGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rlnreceiverGroups_Receiver_receiverId",
                        column: x => x.receiverId,
                        principalTable: "Receiver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rlnreceiverGroups_receiverGroupId",
                table: "rlnreceiverGroups",
                column: "receiverGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_rlnreceiverGroups_receiverId",
                table: "rlnreceiverGroups",
                column: "receiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rlnreceiverGroups");

            migrationBuilder.DropTable(
                name: "sendHistorys");

            migrationBuilder.DropTable(
                name: "receiverGroups");

            migrationBuilder.DropTable(
                name: "Receiver");
        }
    }
}
