using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biletmajster_backend.Migrations
{
    /// <inheritdoc />
    public partial class PlacesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Place_ModelEvents_ModelEventId",
                table: "Place");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.DropIndex(
                name: "IX_Place_ModelEventId",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "ModelEventId",
                table: "Place");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Places",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Places_EventId",
                table: "Places",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_ModelEvents_EventId",
                table: "Places",
                column: "EventId",
                principalTable: "ModelEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_ModelEvents_EventId",
                table: "Places");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_EventId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.AddColumn<long>(
                name: "ModelEventId",
                table: "Place",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Place_ModelEventId",
                table: "Place",
                column: "ModelEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Place_ModelEvents_ModelEventId",
                table: "Place",
                column: "ModelEventId",
                principalTable: "ModelEvents",
                principalColumn: "Id");
        }
    }
}
