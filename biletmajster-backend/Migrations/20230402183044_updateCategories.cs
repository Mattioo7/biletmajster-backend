using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biletmajster_backend.Migrations
{
    /// <inheritdoc />
    public partial class updateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ModelEvents_ModelEventId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ModelEventId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModelEventId",
                table: "Categories");

            migrationBuilder.AddColumn<long>(
                name: "SeatNumber",
                table: "Place",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CategoryModelEvent",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    EventsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModelEvent", x => new { x.CategoriesId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_CategoryModelEvent_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryModelEvent_ModelEvents_EventsId",
                        column: x => x.EventsId,
                        principalTable: "ModelEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryModelEvent_EventsId",
                table: "CategoryModelEvent",
                column: "EventsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryModelEvent");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Place");

            migrationBuilder.AddColumn<long>(
                name: "ModelEventId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModelEventId",
                table: "Categories",
                column: "ModelEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ModelEvents_ModelEventId",
                table: "Categories",
                column: "ModelEventId",
                principalTable: "ModelEvents",
                principalColumn: "Id");
        }
    }
}
