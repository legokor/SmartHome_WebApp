using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartHome.Persistence.Migrations
{
    public partial class Concurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterUnits_AspNetUsers_OwnerId",
                table: "MasterUnits");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "MasterUnits",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MasterUnits_OwnerId",
                table: "MasterUnits",
                newName: "IX_MasterUnits_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ConcurrencyLock",
                table: "MasterUnits",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_MasterUnits_AspNetUsers_UserId",
                table: "MasterUnits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterUnits_AspNetUsers_UserId",
                table: "MasterUnits");

            migrationBuilder.DropColumn(
                name: "ConcurrencyLock",
                table: "MasterUnits");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MasterUnits",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_MasterUnits_UserId",
                table: "MasterUnits",
                newName: "IX_MasterUnits_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterUnits_AspNetUsers_OwnerId",
                table: "MasterUnits",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
