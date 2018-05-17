using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartHome.Persistence.Migrations
{
    public partial class Master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "DataSamples");

            migrationBuilder.RenameColumn(
                name: "SamplingId",
                table: "DataSamples",
                newName: "SampleId");

            migrationBuilder.AddColumn<Guid>(
                name: "MasterUnitId",
                table: "Designs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterUnitId",
                table: "DataSamples",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MasterUnit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomName = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterUnit_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designs_MasterUnitId",
                table: "Designs",
                column: "MasterUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSamples_MasterUnitId",
                table: "DataSamples",
                column: "MasterUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterUnit_OwnerId",
                table: "MasterUnit",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSamples_MasterUnit_MasterUnitId",
                table: "DataSamples",
                column: "MasterUnitId",
                principalTable: "MasterUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_MasterUnit_MasterUnitId",
                table: "Designs",
                column: "MasterUnitId",
                principalTable: "MasterUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSamples_MasterUnit_MasterUnitId",
                table: "DataSamples");

            migrationBuilder.DropForeignKey(
                name: "FK_Designs_MasterUnit_MasterUnitId",
                table: "Designs");

            migrationBuilder.DropTable(
                name: "MasterUnit");

            migrationBuilder.DropIndex(
                name: "IX_Designs_MasterUnitId",
                table: "Designs");

            migrationBuilder.DropIndex(
                name: "IX_DataSamples_MasterUnitId",
                table: "DataSamples");

            migrationBuilder.DropColumn(
                name: "MasterUnitId",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "MasterUnitId",
                table: "DataSamples");

            migrationBuilder.RenameColumn(
                name: "SampleId",
                table: "DataSamples",
                newName: "SamplingId");

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "DataSamples",
                nullable: false,
                defaultValue: 0);
        }
    }
}
