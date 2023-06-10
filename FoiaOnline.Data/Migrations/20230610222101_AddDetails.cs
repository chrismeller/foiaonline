using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoiaOnline.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agency",
                table: "FoundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "FoundRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FoundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "FoundRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExemptionsUsed",
                table: "FoundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinalDisposition",
                table: "FoundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                table: "FoundRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "FoundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Requester",
                table: "FoundRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agency",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "ExemptionsUsed",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "FinalDisposition",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "FoundRequests");

            migrationBuilder.DropColumn(
                name: "Requester",
                table: "FoundRequests");
        }
    }
}
