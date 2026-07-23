using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SmallMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_AspNetUsers_UserId1",
                table: "Attaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_Companies_CompanyId1",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_CompanyId1",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_UserId1",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Attaches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId1",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_CompanyId1",
                table: "Attaches",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_UserId1",
                table: "Attaches",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_AspNetUsers_UserId1",
                table: "Attaches",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_Companies_CompanyId1",
                table: "Attaches",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
