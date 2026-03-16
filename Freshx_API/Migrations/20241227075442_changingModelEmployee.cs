using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class changingModelEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuanceDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IssuancePlace",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProfessionalCertificate",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "IssuanceDate",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuancePlace",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalCertificate",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
