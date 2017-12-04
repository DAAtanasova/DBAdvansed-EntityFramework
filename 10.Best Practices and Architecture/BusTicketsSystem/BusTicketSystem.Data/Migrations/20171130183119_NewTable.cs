using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BusTicketSystem.Data.Migrations
{
    public partial class NewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArrivalTrips",
                columns: table => new
                {
                    ArrivalTripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualArrivalTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DestinationBusStationName = table.Column<string>(nullable: true),
                    OriginBusStationName = table.Column<string>(nullable: true),
                    PassengerCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrivalTrips", x => x.ArrivalTripId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArrivalTrips");
        }
    }
}
