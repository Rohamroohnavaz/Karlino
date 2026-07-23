using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FullUpdateRequestMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resumes_AttachmentId",
                table: "Resumes");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttachmentId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AttachmentId",
                table: "Resumes",
                column: "AttachmentId",
                unique: true,
                filter: "[AttachmentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resumes_AttachmentId",
                table: "Resumes");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttachmentId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AttachmentId",
                table: "Resumes",
                column: "AttachmentId",
                unique: true);
        }
    }
}
