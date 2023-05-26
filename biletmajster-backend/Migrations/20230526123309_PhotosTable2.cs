using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biletmajster_backend.Migrations
{
    /// <inheritdoc />
    public partial class PhotosTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventPhotos_ModelEvents_ModelEventId",
                table: "EventPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventPhotos",
                table: "EventPhotos");

            migrationBuilder.RenameTable(
                name: "EventPhotos",
                newName: "EventsPhotos");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "EventsPhotos",
                newName: "DownloadLink");

            migrationBuilder.RenameIndex(
                name: "IX_EventPhotos_ModelEventId",
                table: "EventsPhotos",
                newName: "IX_EventsPhotos_ModelEventId");

            migrationBuilder.AlterColumn<long>(
                name: "ModelEventId",
                table: "EventsPhotos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsPhotos",
                table: "EventsPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsPhotos_ModelEvents_ModelEventId",
                table: "EventsPhotos",
                column: "ModelEventId",
                principalTable: "ModelEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsPhotos_ModelEvents_ModelEventId",
                table: "EventsPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsPhotos",
                table: "EventsPhotos");

            migrationBuilder.RenameTable(
                name: "EventsPhotos",
                newName: "EventPhotos");

            migrationBuilder.RenameColumn(
                name: "DownloadLink",
                table: "EventPhotos",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_EventsPhotos_ModelEventId",
                table: "EventPhotos",
                newName: "IX_EventPhotos_ModelEventId");

            migrationBuilder.AlterColumn<long>(
                name: "ModelEventId",
                table: "EventPhotos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventPhotos",
                table: "EventPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventPhotos_ModelEvents_ModelEventId",
                table: "EventPhotos",
                column: "ModelEventId",
                principalTable: "ModelEvents",
                principalColumn: "Id");
        }
    }
}
