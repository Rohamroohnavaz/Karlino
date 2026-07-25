using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SolveBugMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserId_AdvertisementId",
                table: "Resumes",
                columns: new[] { "UserId", "AdvertisementId" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resumes_UserId_AdvertisementId",
                table: "Resumes");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes",
                column: "UserId");
        }
    }
}
