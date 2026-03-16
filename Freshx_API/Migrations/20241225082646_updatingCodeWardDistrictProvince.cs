using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class updatingCodeWardDistrictProvince : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Employees_Districts_DistrictCode",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Position_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Provinces_ProvinceCode",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Wards_WardCode",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Districts_DistrictCode",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Provinces_ProvinceCode",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Wards_WardCode",
                table: "Patients");

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

            migrationBuilder.DropIndex(
                name: "IX_Technicians_DistrictCode",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_ProvinceCode",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_WardCode",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DistrictCode",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ProvinceCode",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_WardCode",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DistrictCode",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProvinceCode",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WardCode",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DistrictCode",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ProvinceCode",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_WardCode",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Position",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Position",
                newName: "Positions");

            migrationBuilder.AlterColumn<string>(
                name: "WardId",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WardId",
                table: "Patients",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Patients",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Patients",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WardId",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WardId",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceId",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictId",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_DistrictId",
                table: "Technicians",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_ProvinceId",
                table: "Technicians",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_WardId",
                table: "Technicians",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DistrictId",
                table: "Patients",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ProvinceId",
                table: "Patients",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_WardId",
                table: "Patients",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WardId",
                table: "Employees",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DistrictId",
                table: "Doctors",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ProvinceId",
                table: "Doctors",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_WardId",
                table: "Doctors",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Districts_DistrictId",
                table: "Doctors",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Positions_PositionId",
                table: "Doctors",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Provinces_ProvinceId",
                table: "Doctors",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Wards_WardId",
                table: "Doctors",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Districts_DistrictId",
                table: "Employees",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Provinces_ProvinceId",
                table: "Employees",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Wards_WardId",
                table: "Employees",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Districts_DistrictId",
                table: "Patients",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Provinces_ProvinceId",
                table: "Patients",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Wards_WardId",
                table: "Patients",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Districts_DistrictId",
                table: "Technicians",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Positions_PositionId",
                table: "Technicians",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Provinces_ProvinceId",
                table: "Technicians",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Wards_WardId",
                table: "Technicians",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Districts_DistrictId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Positions_PositionId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Provinces_ProvinceId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Wards_WardId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Districts_DistrictId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Provinces_ProvinceId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Wards_WardId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Districts_DistrictId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Provinces_ProvinceId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Wards_WardId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Districts_DistrictId",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Positions_PositionId",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Provinces_ProvinceId",
                table: "Technicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Wards_WardId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_DistrictId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_ProvinceId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_WardId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DistrictId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ProvinceId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_WardId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WardId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DistrictId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ProvinceId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_WardId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Position");

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "Technicians",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Technicians",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Technicians",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Technicians",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Patients",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Patients",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Patients",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Employees",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Doctors",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Position",
                table: "Position",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_DistrictCode",
                table: "Technicians",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_ProvinceCode",
                table: "Technicians",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_WardCode",
                table: "Technicians",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DistrictCode",
                table: "Patients",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ProvinceCode",
                table: "Patients",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_WardCode",
                table: "Patients",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictCode",
                table: "Employees",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceCode",
                table: "Employees",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WardCode",
                table: "Employees",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DistrictCode",
                table: "Doctors",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ProvinceCode",
                table: "Doctors",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_WardCode",
                table: "Doctors",
                column: "WardCode");

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
                name: "FK_Employees_Districts_DistrictCode",
                table: "Employees",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Position_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Provinces_ProvinceCode",
                table: "Employees",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Wards_WardCode",
                table: "Employees",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Districts_DistrictCode",
                table: "Patients",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Provinces_ProvinceCode",
                table: "Patients",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Wards_WardCode",
                table: "Patients",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");

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
    }
}
