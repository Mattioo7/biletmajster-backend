using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace biletmajster_backend.Migrations
{
    /// <inheritdoc />
    public partial class updateOrganizer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Organizers");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Organizers",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Organizers",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Organizers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccountConfirmationCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizerId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConfirmationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountConfirmationCodes_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventId",
                table: "Reservations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlaceId",
                table: "Reservations",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfirmationCodes_OrganizerId",
                table: "AccountConfirmationCodes",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ModelEvents_EventId",
                table: "Reservations",
                column: "EventId",
                principalTable: "ModelEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Places_PlaceId",
                table: "Reservations",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ModelEvents_EventId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Places_PlaceId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "AccountConfirmationCodes");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_EventId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PlaceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Organizers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Organizers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Organizers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Organizers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
