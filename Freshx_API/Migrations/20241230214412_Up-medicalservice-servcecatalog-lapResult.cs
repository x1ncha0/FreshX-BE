using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class UpmedicalserviceservcecataloglapResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCatalogs_MedicalServiceRequests_MedicalServiceRequestId",
                table: "ServiceCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCatalogs_MedicalServiceRequestId",
                table: "ServiceCatalogs");

            migrationBuilder.DropColumn(
                name: "DrugCatalogId",
                table: "TemplatePrescriptions");

            migrationBuilder.DropColumn(
                name: "MedicalServiceRequestId",
                table: "ServiceCatalogs");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "IsSampleCollectedAtHome",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "ResultTypeId",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "SampleCollectionDate",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "SampleCollectionLocationMedicalFacilityId",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "SampleQualityId",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "SampleReceivedDate",
                table: "LabResults");

            migrationBuilder.RenameColumn(
                name: "IsSuspended",
                table: "TemplatePrescriptions",
                newName: "MedicalExaminationId");

            migrationBuilder.RenameColumn(
                name: "HasStandardValue",
                table: "ServiceCatalogs",
                newName: "ServiceStandardValueId");

            migrationBuilder.RenameColumn(
                name: "SpouseYearOfBirth",
                table: "LabResults",
                newName: "SampleQuality");

            migrationBuilder.RenameColumn(
                name: "SpouseName",
                table: "LabResults",
                newName: "SampleCollectionLocation");

            migrationBuilder.AlterColumn<decimal>(
                name: "CreatedBy",
                table: "TemplatePrescriptions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TemplatePrescriptions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplatePrescriptionId",
                table: "PrescriptionDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Results",
                table: "MedicalServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "LabResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "BloodPressureDiastolic",
                table: "Examines",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BloodPressureSystolic",
                table: "Examines",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HeartRate",
                table: "Examines",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Examines",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherPhysicalFindings",
                table: "Examines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OxygenSaturation",
                table: "Examines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkinCondition",
                table: "Examines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Examines",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisionLeft",
                table: "Examines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisionRight",
                table: "Examines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Examines",
                type: "float",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetail_TemplatePrescriptionId",
                table: "PrescriptionDetail",
                column: "TemplatePrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionDetail_TemplatePrescriptions_TemplatePrescriptionId",
                table: "PrescriptionDetail",
                column: "TemplatePrescriptionId",
                principalTable: "TemplatePrescriptions",
                principalColumn: "TemplatePrescriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionDetail_TemplatePrescriptions_TemplatePrescriptionId",
                table: "PrescriptionDetail");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionDetail_TemplatePrescriptionId",
                table: "PrescriptionDetail");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "TemplatePrescriptions");

            migrationBuilder.DropColumn(
                name: "TemplatePrescriptionId",
                table: "PrescriptionDetail");

            migrationBuilder.DropColumn(
                name: "Results",
                table: "MedicalServiceRequests");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "BloodPressureDiastolic",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "BloodPressureSystolic",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "HeartRate",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "OtherPhysicalFindings",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "OxygenSaturation",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "SkinCondition",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "VisionLeft",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "VisionRight",
                table: "Examines");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Examines");

            migrationBuilder.RenameColumn(
                name: "MedicalExaminationId",
                table: "TemplatePrescriptions",
                newName: "IsSuspended");

            migrationBuilder.RenameColumn(
                name: "ServiceStandardValueId",
                table: "ServiceCatalogs",
                newName: "HasStandardValue");

            migrationBuilder.RenameColumn(
                name: "SampleQuality",
                table: "LabResults",
                newName: "SpouseYearOfBirth");

            migrationBuilder.RenameColumn(
                name: "SampleCollectionLocation",
                table: "LabResults",
                newName: "SpouseName");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "TemplatePrescriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrugCatalogId",
                table: "TemplatePrescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedicalServiceRequestId",
                table: "ServiceCatalogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "LabResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "LabResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsSampleCollectedAtHome",
                table: "LabResults",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "LabResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultTypeId",
                table: "LabResults",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SampleCollectionDate",
                table: "LabResults",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleCollectionLocationMedicalFacilityId",
                table: "LabResults",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleQualityId",
                table: "LabResults",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SampleReceivedDate",
                table: "LabResults",
                type: "datetime2",
                nullable: true);

            // Quay lại cột cũ nếu cần thiết
            migrationBuilder.DropColumn(
                name: "DrugCatalogId",
                table: "DrugCatalogs");

            // Tạo lại cột DrugCatalogId trong bảng TemplatePrescriptions
            migrationBuilder.AddColumn<int>(
                name: "DrugCatalogId",
                table: "TemplatePrescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_MedicalServiceRequestId",
                table: "ServiceCatalogs",
                column: "MedicalServiceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCatalogs_MedicalServiceRequests_MedicalServiceRequestId",
                table: "ServiceCatalogs",
                column: "MedicalServiceRequestId",
                principalTable: "MedicalServiceRequests",
                principalColumn: "MedicalServiceRequestId");
        }
    }
}
