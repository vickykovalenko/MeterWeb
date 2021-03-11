using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeterWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FLATS",
                columns: table => new
                {
                    FLAT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLAT_ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLATS", x => x.FLAT_ID);
                });

            migrationBuilder.CreateTable(
                name: "SERVICES",
                columns: table => new
                {
                    SERVICE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SERVICE_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICES", x => x.SERVICE_ID);
                });

            migrationBuilder.CreateTable(
                name: "METER_TYPES",
                columns: table => new
                {
                    METER_TYPE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    METER_TYPE_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    METER_SERVICE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_METER_TYPES", x => x.METER_TYPE_ID);
                    table.ForeignKey(
                        name: "FK_METER_TYPES_SERVICES",
                        column: x => x.METER_SERVICE_ID,
                        principalTable: "SERVICES",
                        principalColumn: "SERVICE_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TARIFFS",
                columns: table => new
                {
                    TARIFF_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TARIFF_PRICE = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TARIFF_SERVICE_ID = table.Column<int>(type: "int", nullable: false),
                    TARIFF_PRIVILEGE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TARIFFS", x => x.TARIFF_ID);
                    table.ForeignKey(
                        name: "FK_TARIFFS_SERVICES",
                        column: x => x.TARIFF_SERVICE_ID,
                        principalTable: "SERVICES",
                        principalColumn: "SERVICE_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "METERS",
                columns: table => new
                {
                    METER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    METER_NUMBERS = table.Column<int>(type: "int", nullable: false),
                    METER_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    METER_FLAT_ID = table.Column<int>(type: "int", nullable: false),
                    METER_DATA_LAST_REPLACEMENT = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_METERS", x => x.METER_ID);
                    table.ForeignKey(
                        name: "FK_METERS_FLATS",
                        column: x => x.METER_FLAT_ID,
                        principalTable: "FLATS",
                        principalColumn: "FLAT_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_METERS_METER_TYPES",
                        column: x => x.METER_TYPE_ID,
                        principalTable: "METER_TYPES",
                        principalColumn: "METER_TYPE_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENTS",
                columns: table => new
                {
                    PAYMENT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAYMENT_DATA_OF_CURRRENT_PAYMENT = table.Column<DateTime>(type: "date", nullable: false),
                    PAYMENT_SUM_OF_CURRENT_MONTH_PAYMENT = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PAYMENT_DISCOUNT = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PAYMENT_TARIFF_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENTS", x => x.PAYMENT_ID);
                    table.ForeignKey(
                        name: "FK_PAYMENTS_TARIFFS",
                        column: x => x.PAYMENT_TARIFF_ID,
                        principalTable: "TARIFFS",
                        principalColumn: "TARIFF_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "READINGS",
                columns: table => new
                {
                    READING_ID = table.Column<int>(type: "int", nullable: false),
                    READING_DATA_OF_CURRENT_READING = table.Column<DateTime>(type: "date", nullable: true),
                    READING_METER_ID = table.Column<int>(type: "int", nullable: false),
                    READING_PAYMENT_ID = table.Column<int>(type: "int", nullable: false),
                    ReadingNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_READINGS", x => x.READING_ID);
                    table.ForeignKey(
                        name: "FK_READINGS_METERS",
                        column: x => x.READING_METER_ID,
                        principalTable: "METERS",
                        principalColumn: "METER_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_READINGS_PAYMENTS",
                        column: x => x.READING_PAYMENT_ID,
                        principalTable: "PAYMENTS",
                        principalColumn: "PAYMENT_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_METER_TYPES_METER_SERVICE_ID",
                table: "METER_TYPES",
                column: "METER_SERVICE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_METERS_METER_FLAT_ID",
                table: "METERS",
                column: "METER_FLAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_METERS_METER_TYPE_ID",
                table: "METERS",
                column: "METER_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENTS_PAYMENT_TARIFF_ID",
                table: "PAYMENTS",
                column: "PAYMENT_TARIFF_ID");

            migrationBuilder.CreateIndex(
                name: "IX_READINGS_READING_METER_ID",
                table: "READINGS",
                column: "READING_METER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_READINGS_READING_PAYMENT_ID",
                table: "READINGS",
                column: "READING_PAYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TARIFFS_TARIFF_SERVICE_ID",
                table: "TARIFFS",
                column: "TARIFF_SERVICE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "READINGS");

            migrationBuilder.DropTable(
                name: "METERS");

            migrationBuilder.DropTable(
                name: "PAYMENTS");

            migrationBuilder.DropTable(
                name: "FLATS");

            migrationBuilder.DropTable(
                name: "METER_TYPES");

            migrationBuilder.DropTable(
                name: "TARIFFS");

            migrationBuilder.DropTable(
                name: "SERVICES");
        }
    }
}
