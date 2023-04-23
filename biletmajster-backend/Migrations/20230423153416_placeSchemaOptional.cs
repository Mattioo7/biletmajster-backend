using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biletmajster_backend.Migrations
{
    /// <inheritdoc />
    public partial class placeSchemaOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelEvents_Organizers_OrganizerId",
                table: "ModelEvents");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceSchema",
                table: "ModelEvents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "OrganizerId",
                table: "ModelEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModelEvents_Organizers_OrganizerId",
                table: "ModelEvents",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelEvents_Organizers_OrganizerId",
                table: "ModelEvents");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceSchema",
                table: "ModelEvents",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrganizerId",
                table: "ModelEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelEvents_Organizers_OrganizerId",
                table: "ModelEvents",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id");
        }
    }
}
