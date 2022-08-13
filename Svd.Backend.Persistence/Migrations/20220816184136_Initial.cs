using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Svd.Backend.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Airport = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipmentNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FlightNumber = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finalized = table.Column<bool>(type: "bit", nullable: false),
                    AirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    ParcelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    RecipientName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.ParcelId);
                    table.ForeignKey(
                        name: "FK_Parcels_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    BagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ItemsCount = table.Column<double>(type: "float", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.BagId);
                    table.ForeignKey(
                        name: "FK_Bags_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "Airport", "Code" },
                values: new object[,]
                {
                    { new Guid("0f9d2ea1-fd26-404a-9cdc-72eb09a9f191"), 3, "RIX" },
                    { new Guid("13904aa6-9f47-4579-9c1b-fc5a0a1f3961"), 1, "TLL" },
                    { new Guid("4641bcfd-a227-4227-ad4d-ebdd9c65d185"), 2, "HEL" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Code", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("446f7076-0590-483c-9a9a-96c9e62a972a"), "FI", 2, "Finland" },
                    { new Guid("6deb7ba4-cba3-4b82-ab68-9456bfa8eaec"), "EE", 1, "Estonia" },
                    { new Guid("ccf08179-22e9-4239-b02b-cc9042dc517f"), "LV", 3, "Latvia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bags_BagNumber",
                table: "Bags",
                column: "BagNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bags_ShipmentId",
                table: "Bags",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CountryId",
                table: "Parcels",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels",
                column: "ParcelNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_AirportId",
                table: "Shipments",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments",
                column: "ShipmentNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bags");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
