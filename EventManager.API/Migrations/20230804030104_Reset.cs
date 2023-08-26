using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.API.Migrations
{
    /// <inheritdoc />
    public partial class Reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoggedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondryMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowedSubEventIdsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "EventAttendances",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAttendances", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "EventPlanners",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackGroundUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondryMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPlanners", x => x.UniqueId);
                    table.ForeignKey(
                        name: "FK_EventPlanners_Contact_PointOfContactId",
                        column: x => x.PointOfContactId,
                        principalTable: "Contact",
                        principalColumn: "UniqueId");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventOrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.UniqueId);
                    table.ForeignKey(
                        name: "FK_Events_EventPlanners_EventOrganizerId",
                        column: x => x.EventOrganizerId,
                        principalTable: "EventPlanners",
                        principalColumn: "UniqueId");
                    table.ForeignKey(
                        name: "FK_Events_Events_ParentEventId",
                        column: x => x.ParentEventId,
                        principalTable: "Events",
                        principalColumn: "UniqueId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPlanners_PointOfContactId",
                table: "EventPlanners",
                column: "PointOfContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventOrganizerId",
                table: "Events",
                column: "EventOrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ParentEventId",
                table: "Events",
                column: "ParentEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "EventAttendances");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventPlanners");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
