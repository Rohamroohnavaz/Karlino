using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BigMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Resumes_RequestResumeId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_Advertisements_AdvertisementId1",
                table: "Attaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_Resumes_RequestResumeId",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_AdvertisementId1",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_RequestResumeId",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_RequestResumeId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "AdvertisementId1",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "RequestResumeId",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "RequestResumeId",
                table: "Advertisements");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AttachmentId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AdvertisementId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FeatureId",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AdvertisementId",
                table: "Resumes",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_AttachmentId",
                table: "Resumes",
                column: "AttachmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Advertisements_AdvertisementId",
                table: "Resumes",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Attaches_AttachmentId",
                table: "Resumes",
                column: "AttachmentId",
                principalTable: "Attaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Advertisements_AdvertisementId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Attaches_AttachmentId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_AdvertisementId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_AttachmentId",
                table: "Resumes");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttachmentId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdvertisementId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeatureId",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AdvertisementId1",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestResumeId",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestResumeId",
                table: "Advertisements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_AdvertisementId1",
                table: "Attaches",
                column: "AdvertisementId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_RequestResumeId",
                table: "Attaches",
                column: "RequestResumeId",
                unique: true,
                filter: "[RequestResumeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_RequestResumeId",
                table: "Advertisements",
                column: "RequestResumeId",
                unique: true,
                filter: "[RequestResumeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Resumes_RequestResumeId",
                table: "Advertisements",
                column: "RequestResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_Advertisements_AdvertisementId1",
                table: "Attaches",
                column: "AdvertisementId1",
                principalTable: "Advertisements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_Resumes_RequestResumeId",
                table: "Attaches",
                column: "RequestResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");
        }
    }
}
