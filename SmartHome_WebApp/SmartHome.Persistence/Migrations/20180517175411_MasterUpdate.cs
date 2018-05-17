using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartHome.Persistence.Migrations
{
    public partial class MasterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSamples_MasterUnit_MasterUnitId",
                table: "DataSamples");

            migrationBuilder.DropForeignKey(
                name: "FK_Designs_MasterUnit_MasterUnitId",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterUnit_AspNetUsers_OwnerId",
                table: "MasterUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterUnit",
                table: "MasterUnit");

            migrationBuilder.RenameTable(
                name: "MasterUnit",
                newName: "MasterUnits");

            migrationBuilder.RenameIndex(
                name: "IX_MasterUnit_OwnerId",
                table: "MasterUnits",
                newName: "IX_MasterUnits_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterUnits",
                table: "MasterUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSamples_MasterUnits_MasterUnitId",
                table: "DataSamples",
                column: "MasterUnitId",
                principalTable: "MasterUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_MasterUnits_MasterUnitId",
                table: "Designs",
                column: "MasterUnitId",
                principalTable: "MasterUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterUnits_AspNetUsers_OwnerId",
                table: "MasterUnits",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSamples_MasterUnits_MasterUnitId",
                table: "DataSamples");

            migrationBuilder.DropForeignKey(
                name: "FK_Designs_MasterUnits_MasterUnitId",
                table: "Designs");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterUnits_AspNetUsers_OwnerId",
                table: "MasterUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterUnits",
                table: "MasterUnits");

            migrationBuilder.RenameTable(
                name: "MasterUnits",
                newName: "MasterUnit");

            migrationBuilder.RenameIndex(
                name: "IX_MasterUnits_OwnerId",
                table: "MasterUnit",
                newName: "IX_MasterUnit_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterUnit",
                table: "MasterUnit",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MasterUnit_AspNetUsers_OwnerId",
                table: "MasterUnit",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
