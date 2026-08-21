using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResumeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EducationDegree",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EducationEndYear",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationField",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EducationStartYear",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResumeFilePath",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkDescription",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkEndYear",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkStartYear",
                table: "Resumes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkTitle",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "EducationDegree",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "EducationEndYear",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "EducationField",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "EducationStartYear",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ResumeFilePath",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "WorkDescription",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "WorkEndYear",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "WorkStartYear",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "WorkTitle",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");
        }
    }
}
