using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicApp.Api.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_Enrollees_EnrolleeId",
                table: "Diseases");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollees_Diseases_DiseaseId",
                table: "Enrollees");

            migrationBuilder.DropIndex(
                name: "IX_Enrollees_DiseaseId",
                table: "Enrollees");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_EnrolleeId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "Enrollees");

            migrationBuilder.DropColumn(
                name: "DateDiagnosed",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "Diseases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "Enrollees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDiagnosed",
                table: "Diseases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EnrolleeId",
                table: "Diseases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollees_DiseaseId",
                table: "Enrollees",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_EnrolleeId",
                table: "Diseases",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_Enrollees_EnrolleeId",
                table: "Diseases",
                column: "EnrolleeId",
                principalTable: "Enrollees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollees_Diseases_DiseaseId",
                table: "Enrollees",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
