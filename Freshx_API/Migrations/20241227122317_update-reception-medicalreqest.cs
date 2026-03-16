using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class updatereceptionmedicalreqest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalServiceRequestId",
                table: "ServiceCatalogs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Receptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CashierId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_MedicalServiceRequestId",
                table: "ServiceCatalogs",
                column: "MedicalServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CashierId",
                table: "Bills",
                column: "CashierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Employees_CashierId",
                table: "Bills",
                column: "CashierId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCatalogs_MedicalServiceRequests_MedicalServiceRequestId",
                table: "ServiceCatalogs",
                column: "MedicalServiceRequestId",
                principalTable: "MedicalServiceRequests",
                principalColumn: "MedicalServiceRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Employees_CashierId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCatalogs_MedicalServiceRequests_MedicalServiceRequestId",
                table: "ServiceCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCatalogs_MedicalServiceRequestId",
                table: "ServiceCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_Bills_CashierId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "MedicalServiceRequestId",
                table: "ServiceCatalogs");

            migrationBuilder.DropColumn(
                name: "CashierId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Receptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
