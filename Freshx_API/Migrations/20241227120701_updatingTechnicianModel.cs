using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class updatingTechnicianModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvataId",
                table: "Technicians",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Technicians",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Technicians",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_AvataId",
                table: "Technicians",
                column: "AvataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Savefiles_AvataId",
                table: "Technicians",
                column: "AvataId",
                principalTable: "Savefiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Savefiles_AvataId",
                table: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Technicians_AvataId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "AvataId",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Technicians");
        }
    }
}
