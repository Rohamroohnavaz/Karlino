using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinalProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DetailChangeMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_UserId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Companies",
                newName: "ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_UserId1",
                table: "Companies",
                newName: "IX_Companies_ModifiedById");

            migrationBuilder.AddColumn<Guid>(
                name: "AdvertisementId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Feature",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Feature",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Feature",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "CompanyFeature",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Attaches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "Advertisements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Advertisements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedById",
                table: "Advertisements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_CreateById",
                table: "Resumes",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_DeletedById",
                table: "Resumes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_ModifiedById",
                table: "Resumes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreateById",
                table: "Payment",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_DeletedById",
                table: "Payment",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ModifiedById",
                table: "Payment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreateById",
                table: "Notification",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_DeletedById",
                table: "Notification",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ModifiedById",
                table: "Notification",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_CreateById",
                table: "Feature",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_DeletedById",
                table: "Feature",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ModifiedById",
                table: "Feature",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFeature_CreateById",
                table: "CompanyFeature",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFeature_DeletedById",
                table: "CompanyFeature",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFeature_ModifiedById",
                table: "CompanyFeature",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreateById",
                table: "Companies",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_DeletedById",
                table: "Companies",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreateById",
                table: "Categories",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedById",
                table: "Categories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_CreateById",
                table: "Attaches",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_DeletedById",
                table: "Attaches",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attaches_ModifiedById",
                table: "Attaches",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreateById",
                table: "AspNetUsers",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeletedById",
                table: "AspNetUsers",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ModifiedById",
                table: "AspNetUsers",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CreateById",
                table: "Advertisements",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_DeletedById",
                table: "Advertisements",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ModifiedById",
                table: "Advertisements",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_CreateById",
                table: "Advertisements",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_DeletedById",
                table: "Advertisements",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_ModifiedById",
                table: "Advertisements",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_CreateById",
                table: "AspNetUsers",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DeletedById",
                table: "AspNetUsers",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ModifiedById",
                table: "AspNetUsers",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_AspNetUsers_CreateById",
                table: "Attaches",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_AspNetUsers_DeletedById",
                table: "Attaches",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaches_AspNetUsers_ModifiedById",
                table: "Attaches",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreateById",
                table: "Categories",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_DeletedById",
                table: "Categories",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_ModifiedById",
                table: "Categories",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreateById",
                table: "Companies",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_DeletedById",
                table: "Companies",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_ModifiedById",
                table: "Companies",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFeature_AspNetUsers_CreateById",
                table: "CompanyFeature",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFeature_AspNetUsers_DeletedById",
                table: "CompanyFeature",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFeature_AspNetUsers_ModifiedById",
                table: "CompanyFeature",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_AspNetUsers_CreateById",
                table: "Feature",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_AspNetUsers_DeletedById",
                table: "Feature",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_AspNetUsers_ModifiedById",
                table: "Feature",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_CreateById",
                table: "Notification",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_DeletedById",
                table: "Notification",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_ModifiedById",
                table: "Notification",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_CreateById",
                table: "Payment",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_DeletedById",
                table: "Payment",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_ModifiedById",
                table: "Payment",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AspNetUsers_CreateById",
                table: "Resumes",
                column: "CreateById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AspNetUsers_DeletedById",
                table: "Resumes",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AspNetUsers_ModifiedById",
                table: "Resumes",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_CreateById",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_DeletedById",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_ModifiedById",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_CreateById",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DeletedById",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ModifiedById",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_AspNetUsers_CreateById",
                table: "Attaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_AspNetUsers_DeletedById",
                table: "Attaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaches_AspNetUsers_ModifiedById",
                table: "Attaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreateById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_DeletedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_ModifiedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreateById",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_DeletedById",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_ModifiedById",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFeature_AspNetUsers_CreateById",
                table: "CompanyFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFeature_AspNetUsers_DeletedById",
                table: "CompanyFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFeature_AspNetUsers_ModifiedById",
                table: "CompanyFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Feature_AspNetUsers_CreateById",
                table: "Feature");

            migrationBuilder.DropForeignKey(
                name: "FK_Feature_AspNetUsers_DeletedById",
                table: "Feature");

            migrationBuilder.DropForeignKey(
                name: "FK_Feature_AspNetUsers_ModifiedById",
                table: "Feature");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_CreateById",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_DeletedById",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_ModifiedById",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_CreateById",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_DeletedById",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_ModifiedById",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AspNetUsers_CreateById",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AspNetUsers_DeletedById",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AspNetUsers_ModifiedById",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_CreateById",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_DeletedById",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_ModifiedById",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Payment_CreateById",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_DeletedById",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_ModifiedById",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Notification_CreateById",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_DeletedById",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_ModifiedById",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Feature_CreateById",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_DeletedById",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_ModifiedById",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFeature_CreateById",
                table: "CompanyFeature");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFeature_DeletedById",
                table: "CompanyFeature");

            migrationBuilder.DropIndex(
                name: "IX_CompanyFeature_ModifiedById",
                table: "CompanyFeature");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CreateById",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_DeletedById",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreateById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ModifiedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_CreateById",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_DeletedById",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_Attaches_ModifiedById",
                table: "Attaches");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreateById",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DeletedById",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ModifiedById",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CreateById",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_DeletedById",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ModifiedById",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "CompanyFeature");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "CompanyFeature");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "CompanyFeature");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Attaches");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Companies",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_ModifiedById",
                table: "Companies",
                newName: "IX_Companies_UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_UserId1",
                table: "Companies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
