using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class updatemedicalsevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "MedicalServiceRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "discount",
                table: "MedicalServiceRequests",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_DepartmentId",
                table: "MedicalServiceRequests",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServiceRequests_Departments_DepartmentId",
                table: "MedicalServiceRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServiceRequests_Departments_DepartmentId",
                table: "MedicalServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MedicalServiceRequests_DepartmentId",
                table: "MedicalServiceRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "MedicalServiceRequests");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "MedicalServiceRequests");
        }
    }
}
