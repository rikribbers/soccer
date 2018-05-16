using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Poule.Data.Migrations
{
    public partial class UpdatedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Users_UserId",
                table: "Predictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "MyUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyUsers",
                table: "MyUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_MyUsers_UserId",
                table: "Predictions",
                column: "UserId",
                principalTable: "MyUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_MyUsers_UserId",
                table: "Predictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyUsers",
                table: "MyUsers");

            migrationBuilder.RenameTable(
                name: "MyUsers",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Users_UserId",
                table: "Predictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
