using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class changingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_DrugCatalogs_Pharmacies_DepartmentPharmacyId",
                table: "DrugCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_DrugCatalogs_DepartmentPharmacyId",
                table: "DrugCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PatientId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentPharmacyId",
                table: "DrugCatalogs");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "IdentityCardNumber");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Technicians",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Technicians",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Technicians",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardNumber",
                table: "Technicians",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Technicians",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Technicians",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Technicians",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardNumber",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_AccountId",
                table: "Technicians",
                column: "AccountId"
                );

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_DepartmentId",
                table: "Technicians",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_DistrictCode",
                table: "Technicians",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_PositionId",
                table: "Technicians",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_ProvinceCode",
                table: "Technicians",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_WardCode",
                table: "Technicians",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId"
               );

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountId",
                table: "Employees",
                column: "AccountId"
               );

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors",
                column: "AccountId"
                );

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DistrictCode",
                table: "Doctors",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PositionId",
                table: "Doctors",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ProvinceCode",
                table: "Doctors",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_WardCode",
                table: "Doctors",
                column: "WardCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_AccountId",
                table: "Doctors",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Districts_DistrictCode",
                table: "Doctors",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Position_PositionId",
                table: "Doctors",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Provinces_ProvinceCode",
                table: "Doctors",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Wards_WardCode",
                table: "Doctors",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_AccountId",
                table: "Employees",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Position_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_AccountId",
                table: "Patients",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_AspNetUsers_AccountId",
                table: "Technicians",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Departments_DepartmentId",
                table: "Technicians",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Districts_DistrictCode",
                table: "Technicians",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Position_PositionId",
                table: "Technicians",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Provinces_ProvinceCode",
                table: "Technicians",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Wards_WardCode",
                table: "Technicians",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_AccountId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Districts_DistrictCode",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Position_PositionId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Provinces_ProvinceCode",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Wards_WardCode",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_AccountId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Position_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_AccountId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_AspNetUsers_AccountId",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Departments_DepartmentId",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Districts_DistrictCode",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Position_PositionId",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Provinces_ProvinceCode",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Wards_WardCode",
                table: "Technicians");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_AccountId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_DepartmentId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_DistrictCode",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_PositionId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_ProvinceCode",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_WardCode",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AccountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AccountId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DistrictCode",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_PositionId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ProvinceCode",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_WardCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "IdentityCardNumber",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IdentityCardNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "IdentityCardNumber",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentPharmacyId",
                table: "DrugCatalogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrugCatalogs_DepartmentPharmacyId",
                table: "DrugCatalogs",
                column: "DepartmentPharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PatientId",
                table: "AspNetUsers",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId",
                table: "AspNetUsers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugCatalogs_Pharmacies_DepartmentPharmacyId",
                table: "DrugCatalogs",
                column: "DepartmentPharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "PharmacyId");
        }
    }
}
