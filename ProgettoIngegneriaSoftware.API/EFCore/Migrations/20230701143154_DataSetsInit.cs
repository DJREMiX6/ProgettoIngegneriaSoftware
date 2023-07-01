using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoIngegneriaSoftware.API.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class DataSetsInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatsZones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatsZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatsZones_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatsRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatsZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatsRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatsRows_SeatsZones_SeatsZoneId",
                        column: x => x.SeatsZoneId,
                        principalTable: "SeatsZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    SeatsRowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_SeatsRows_SeatsRowId",
                        column: x => x.SeatsRowId,
                        principalTable: "SeatsRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookedSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventEntityModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SeatEntityModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserEntityModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookedSeats_Events_EventEntityModelId",
                        column: x => x.EventEntityModelId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookedSeats_Seats_SeatEntityModelId",
                        column: x => x.SeatEntityModelId,
                        principalTable: "Seats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookedSeats_Users_UserEntityModelId",
                        column: x => x.UserEntityModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_EventEntityModelId",
                table: "BookedSeats",
                column: "EventEntityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_SeatEntityModelId",
                table: "BookedSeats",
                column: "SeatEntityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_UserEntityModelId",
                table: "BookedSeats",
                column: "UserEntityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlaceId",
                table: "Events",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatsRowId",
                table: "Seats",
                column: "SeatsRowId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatsRows_SeatsZoneId",
                table: "SeatsRows",
                column: "SeatsZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatsZones_PlaceId",
                table: "SeatsZones",
                column: "PlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedSeats");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "SeatsRows");

            migrationBuilder.DropTable(
                name: "SeatsZones");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
