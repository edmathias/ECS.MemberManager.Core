using Microsoft.EntityFrameworkCore.Migrations;

namespace ECS.MemberManager.Core.EF.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_EMails_EMailId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "EMailOrganization");

            migrationBuilder.DropIndex(
                name: "IX_Persons_EMailId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "EMailId",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "EMailAddress",
                table: "EMails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EMails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMails_OrganizationId",
                table: "EMails",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EMails_Organizations_OrganizationId",
                table: "EMails",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMails_Organizations_OrganizationId",
                table: "EMails");

            migrationBuilder.DropIndex(
                name: "IX_EMails_OrganizationId",
                table: "EMails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EMails");

            migrationBuilder.AddColumn<int>(
                name: "EMailId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMailAddress",
                table: "EMails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EMailOrganization",
                columns: table => new
                {
                    EMailsId = table.Column<int>(type: "int", nullable: false),
                    OrganizationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMailOrganization", x => new { x.EMailsId, x.OrganizationsId });
                    table.ForeignKey(
                        name: "FK_EMailOrganization_EMails_EMailsId",
                        column: x => x.EMailsId,
                        principalTable: "EMails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EMailOrganization_Organizations_OrganizationsId",
                        column: x => x.OrganizationsId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EMailId",
                table: "Persons",
                column: "EMailId");

            migrationBuilder.CreateIndex(
                name: "IX_EMailOrganization_OrganizationsId",
                table: "EMailOrganization",
                column: "OrganizationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_EMails_EMailId",
                table: "Persons",
                column: "EMailId",
                principalTable: "EMails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
