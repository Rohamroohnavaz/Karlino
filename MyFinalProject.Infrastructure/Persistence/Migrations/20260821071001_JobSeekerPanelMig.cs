using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobSeekerPanelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Resumes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GitHubUrl",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "GitHubUrl",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Resumes");
        }
    }
}
