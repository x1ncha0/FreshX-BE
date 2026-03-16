using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class upemamine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examines_DiagnosisDictionaries_DiagnosisDictionaryId",
                table: "Examines");

            migrationBuilder.DropForeignKey(
                name: "FK_Examines_TemplatePrescriptions_TemplatePrescriptionId",
                table: "Examines");


            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_InventoryTypes_InventoryTypeId",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_DepartmentId",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_InventoryTypeId",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Examines_DiagnosisDictionaryId",
                table: "Examines");

            migrationBuilder.DropIndex(
                name: "IX_Examines_TemplatePrescriptionId",
                table: "Examines");

            migrationBuilder.RenameColumn(
                name: "MedicalExaminationId",
                table: "TemplatePrescriptions",
                newName: "DiagnosisDictionaryId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ServiceTypes",
                type: "nvarchar(max)",
                nullable: true);



            migrationBuilder.AddColumn<int>(
                name: "TemplatePrescriptionId",
                table: "DiagnosisDictionaries",
                type: "int",
                nullable: true);



            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisDictionaries_TemplatePrescriptionId",
                table: "DiagnosisDictionaries",
                column: "TemplatePrescriptionId");



            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisDictionaries_TemplatePrescriptions_TemplatePrescriptionId",
                table: "DiagnosisDictionaries",
                column: "TemplatePrescriptionId",
                principalTable: "TemplatePrescriptions",
                principalColumn: "TemplatePrescriptionId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisDictionaries_TemplatePrescriptions_TemplatePrescriptionId",
                table: "DiagnosisDictionaries");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTypes_Pharmacies_InventoryTypeId",
                table: "InventoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisDictionaries_TemplatePrescriptionId",
                table: "DiagnosisDictionaries");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "TemplatePrescriptionId",
                table: "DiagnosisDictionaries");

            migrationBuilder.RenameColumn(
                name: "DiagnosisDictionaryId",
                table: "TemplatePrescriptions",
                newName: "MedicalExaminationId");

        

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_DepartmentId",
                table: "Pharmacies",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_InventoryTypeId",
                table: "Pharmacies",
                column: "InventoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_DiagnosisDictionaryId",
                table: "Examines",
                column: "DiagnosisDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_TemplatePrescriptionId",
                table: "Examines",
                column: "TemplatePrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examines_DiagnosisDictionaries_DiagnosisDictionaryId",
                table: "Examines",
                column: "DiagnosisDictionaryId",
                principalTable: "DiagnosisDictionaries",
                principalColumn: "DiagnosisDictionaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examines_TemplatePrescriptions_TemplatePrescriptionId",
                table: "Examines",
                column: "TemplatePrescriptionId",
                principalTable: "TemplatePrescriptions",
                principalColumn: "TemplatePrescriptionId");


           
        }
    }
}
