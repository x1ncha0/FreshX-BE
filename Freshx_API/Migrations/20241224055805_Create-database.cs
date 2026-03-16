using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freshx_API.Migrations
{
    /// <inheritdoc />
    public partial class Createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameLatin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTypes",
                columns: table => new
                {
                    DepartmentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTypes", x => x.DepartmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisDictionaries",
                columns: table => new
                {
                    DiagnosisDictionaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SequenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateDueDate = table.Column<bool>(type: "bit", nullable: true),
                    MedicalAdvice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisDictionaries", x => x.DiagnosisDictionaryId);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseGroups",
                columns: table => new
                {
                    DiseaseGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseGroups", x => x.DiseaseGroupId);
                });

            migrationBuilder.CreateTable(
                name: "DrugTypes",
                columns: table => new
                {
                    DrugTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugTypes", x => x.DrugTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ICDcatalogs",
                columns: table => new
                {
                    ICDCatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameRussian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICDCatalogGroupId = table.Column<int>(type: "int", nullable: true),
                    Subgroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: true),
                    IsInfectious = table.Column<bool>(type: "bit", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: true),
                    NameUnaccented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    LegacyCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICDcatalogs", x => x.ICDCatalogId);
                    table.ForeignKey(
                        name: "FK_ICDcatalogs_ICDcatalogs_ICDCatalogGroupId",
                        column: x => x.ICDCatalogGroupId,
                        principalTable: "ICDcatalogs",
                        principalColumn: "ICDCatalogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Icdchapters",
                columns: table => new
                {
                    IcdchapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameVietNamese = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    NameUnaccented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icdchapters", x => x.IcdchapterId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTypes",
                columns: table => new
                {
                    InventoryTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTypes", x => x.InventoryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MenuPermissions",
                columns: table => new
                {
                    MenuParentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermissions", x => x.MenuParentId);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalExaminationId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AdministrativeUnitId = table.Column<int>(type: "int", nullable: true),
                    AdministrativeRegionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Savefiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savefiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceGroups",
                columns: table => new
                {
                    ServiceGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceGroups", x => x.ServiceGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.TechnicianId);
                });

            migrationBuilder.CreateTable(
                name: "TemplatePrescriptions",
                columns: table => new
                {
                    TemplatePrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugCatalogId = table.Column<int>(type: "int", nullable: false),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatePrescriptions", x => x.TemplatePrescriptionId);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                columns: table => new
                {
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConversionValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.UnitOfMeasureId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentTypeId = table.Column<int>(type: "int", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_DepartmentTypes_DepartmentTypeId",
                        column: x => x.DepartmentTypeId,
                        principalTable: "DepartmentTypes",
                        principalColumn: "DepartmentTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMenuId = table.Column<int>(type: "int", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menus_MenuPermissions_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "MenuPermissions",
                        principalColumn: "MenuParentId");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdministrativeUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvataId = table.Column<int>(type: "int", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_Savefiles_AvataId",
                        column: x => x.AvataId,
                        principalTable: "Savefiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCatalogs",
                columns: table => new
                {
                    ServiceCatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HasStandardValue = table.Column<bool>(type: "bit", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    IsParentService = table.Column<bool>(type: "bit", nullable: true),
                    ParentServiceId = table.Column<int>(type: "int", nullable: true),
                    ServiceGroupId = table.Column<int>(type: "int", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: true),
                    ServiceCatalogId1 = table.Column<int>(type: "int", nullable: true),
                    ServiceGroupId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCatalogs", x => x.ServiceCatalogId);
                    table.ForeignKey(
                        name: "FK_ServiceCatalogs_ServiceCatalogs_ParentServiceId",
                        column: x => x.ParentServiceId,
                        principalTable: "ServiceCatalogs",
                        principalColumn: "ServiceCatalogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceCatalogs_ServiceCatalogs_ServiceCatalogId1",
                        column: x => x.ServiceCatalogId1,
                        principalTable: "ServiceCatalogs",
                        principalColumn: "ServiceCatalogId");
                    table.ForeignKey(
                        name: "FK_ServiceCatalogs_ServiceGroups_ServiceGroupId",
                        column: x => x.ServiceGroupId,
                        principalTable: "ServiceGroups",
                        principalColumn: "ServiceGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceCatalogs_ServiceGroups_ServiceGroupId1",
                        column: x => x.ServiceGroupId1,
                        principalTable: "ServiceGroups",
                        principalColumn: "ServiceGroupId");
                    table.ForeignKey(
                        name: "FK_ServiceCatalogs_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    InventoryTypeId = table.Column<int>(type: "int", nullable: true),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: true),
                    NameUnaccented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.PharmacyId);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Pharmacies_InventoryTypes_InventoryTypeId",
                        column: x => x.InventoryTypeId,
                        principalTable: "InventoryTypes",
                        principalColumn: "InventoryTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdministrativeUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "Districts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConclusionDictionaries",
                columns: table => new
                {
                    ConclusionDictionaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SequenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateDueDate = table.Column<bool>(type: "bit", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalAdvice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    ServiceCatalogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConclusionDictionaries", x => x.ConclusionDictionaryId);
                    table.ForeignKey(
                        name: "FK_ConclusionDictionaries_ServiceCatalogs_ServiceCatalogId",
                        column: x => x.ServiceCatalogId,
                        principalTable: "ServiceCatalogs",
                        principalColumn: "ServiceCatalogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceStandardValues",
                columns: table => new
                {
                    ServiceStandardValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCatalogId = table.Column<int>(type: "int", nullable: true),
                    CommonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaleMaximum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaleMinimum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FemaleMaximum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FemaleMinimum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildrenMaximum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildrenMinimum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    IsLessThanOrEqualToMinimum = table.Column<bool>(type: "bit", nullable: true),
                    IsGreaterThanOrEqualToMaximum = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceStandardValues", x => x.ServiceStandardValueId);
                    table.ForeignKey(
                        name: "FK_ServiceStandardValues_ServiceCatalogs_ServiceCatalogId",
                        column: x => x.ServiceCatalogId,
                        principalTable: "ServiceCatalogs",
                        principalColumn: "ServiceCatalogId");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvataId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessionalCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuanceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IssuancePlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Employees_Districts_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "Districts",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Employees_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Employees_Savefiles_AvataId",
                        column: x => x.AvataId,
                        principalTable: "Savefiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Wards_WardCode",
                        column: x => x.WardCode,
                        principalTable: "Wards",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.ClinicId);
                    table.ForeignKey(
                        name: "FK_Hospitals_Districts_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "Districts",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Hospitals_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Hospitals_Wards_WardCode",
                        column: x => x.WardCode,
                        principalTable: "Wards",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    Ethnicity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Districts_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "Districts",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Patients_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Patients_Savefiles_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Savefiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patients_Wards_WardCode",
                        column: x => x.WardCode,
                        principalTable: "Wards",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameVietNam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsForeign = table.Column<bool>(type: "bit", nullable: true),
                    IsStateOwned = table.Column<bool>(type: "bit", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    NameUnaccented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPharmaceuticalSupplier = table.Column<bool>(type: "bit", nullable: false),
                    IsMedicalConsumableSupplier = table.Column<bool>(type: "bit", nullable: false),
                    IsAssetSupplier = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Suppliers_Districts_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "Districts",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Suppliers_Provinces_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "Provinces",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Suppliers_Wards_WardCode",
                        column: x => x.WardCode,
                        principalTable: "Wards",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "EmailContents",
                columns: table => new
                {
                    EmailContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailContents", x => x.EmailContentId);
                    table.ForeignKey(
                        name: "FK_EmailContents_Employees_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    IdentityCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    AvatarId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    WardId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DistrictId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    ReceptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SequenceNumber = table.Column<int>(type: "int", nullable: true),
                    IsPriority = table.Column<bool>(type: "bit", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    ReceptionLocationId = table.Column<int>(type: "int", nullable: true),
                    ReceptionistId = table.Column<int>(type: "int", nullable: true),
                    ReceptionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedDoctorId = table.Column<int>(type: "int", nullable: true),
                    MedicalServiceRequestId = table.Column<int>(type: "int", nullable: true),
                    ServiceTotalAmount = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptions", x => x.ReceptionId);
                    table.ForeignKey(
                        name: "FK_Receptions_Doctors_AssignedDoctorId",
                        column: x => x.AssignedDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_Receptions_Employees_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Receptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "DrugCatalogs",
                columns: table => new
                {
                    DrugCatalogId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameUnaccented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveIngredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugTypeId = table.Column<int>(type: "int", nullable: true),
                    DrugClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteOfAdministration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuspended = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuantityImported = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QuantityInStock = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepartmentPharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugCatalogs", x => x.DrugCatalogId);
                    table.ForeignKey(
                        name: "FK_DrugCatalogs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_DrugCatalogs_DrugTypes_DrugTypeId",
                        column: x => x.DrugTypeId,
                        principalTable: "DrugTypes",
                        principalColumn: "DrugTypeId");
                    table.ForeignKey(
                        name: "FK_DrugCatalogs_Pharmacies_DepartmentPharmacyId",
                        column: x => x.DepartmentPharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyId");
                    table.ForeignKey(
                        name: "FK_DrugCatalogs_Suppliers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId");
                    table.ForeignKey(
                        name: "FK_DrugCatalogs_TemplatePrescriptions_DrugCatalogId",
                        column: x => x.DrugCatalogId,
                        principalTable: "TemplatePrescriptions",
                        principalColumn: "TemplatePrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugCatalogs_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "UnitOfMeasureId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptionId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "ReceptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examines",
                columns: table => new
                {
                    ExamineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespiratoryRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICDCatalogId = table.Column<int>(type: "int", nullable: true),
                    DiagnosisDictionaryId = table.Column<int>(type: "int", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalAdvice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: true),
                    TemplatePrescriptionId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    PrescriptionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowUpAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comorbidities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComorbidityCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComorbidityNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowUpAppointmentNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    ExaminationNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examines", x => x.ExamineId);
                    table.ForeignKey(
                        name: "FK_Examines_DiagnosisDictionaries_DiagnosisDictionaryId",
                        column: x => x.DiagnosisDictionaryId,
                        principalTable: "DiagnosisDictionaries",
                        principalColumn: "DiagnosisDictionaryId");
                    table.ForeignKey(
                        name: "FK_Examines_ICDcatalogs_ICDCatalogId",
                        column: x => x.ICDCatalogId,
                        principalTable: "ICDcatalogs",
                        principalColumn: "ICDCatalogId");
                    table.ForeignKey(
                        name: "FK_Examines_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_Examines_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId");
                    table.ForeignKey(
                        name: "FK_Examines_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "ReceptionId");
                    table.ForeignKey(
                        name: "FK_Examines_TemplatePrescriptions_TemplatePrescriptionId",
                        column: x => x.TemplatePrescriptionId,
                        principalTable: "TemplatePrescriptions",
                        principalColumn: "TemplatePrescriptionId");
                });

            migrationBuilder.CreateTable(
                name: "LabResults",
                columns: table => new
                {
                    LabResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    TechnicianId = table.Column<int>(type: "int", nullable: true),
                    ConcludingDoctorId = table.Column<int>(type: "int", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultTypeId = table.Column<int>(type: "int", nullable: true),
                    SampleReceivedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleTypeId = table.Column<int>(type: "int", nullable: true),
                    SampleQualityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseYearOfBirth = table.Column<int>(type: "int", nullable: true),
                    SampleCollectionLocationMedicalFacilityId = table.Column<int>(type: "int", nullable: true),
                    IsSampleCollectedAtHome = table.Column<int>(type: "int", nullable: true),
                    SampleReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleCollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleCollectionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabResults", x => x.LabResultId);
                    table.ForeignKey(
                        name: "FK_LabResults_Doctors_ConcludingDoctorId",
                        column: x => x.ConcludingDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_LabResults_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_LabResults_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "ReceptionId");
                    table.ForeignKey(
                        name: "FK_LabResults_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianId");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionDetail",
                columns: table => new
                {
                    PrescriptionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    DrugCatalogId = table.Column<int>(type: "int", nullable: false),
                    MorningDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoonDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AfternoonDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EveningDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaysOfSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDetail", x => x.PrescriptionDetailId);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetail_DrugCatalogs_DrugCatalogId",
                        column: x => x.DrugCatalogId,
                        principalTable: "DrugCatalogs",
                        principalColumn: "DrugCatalogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetail_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    BillDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    ServiceCatalogId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.BillDetailId);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_ServiceCatalogs_ServiceCatalogId",
                        column: x => x.ServiceCatalogId,
                        principalTable: "ServiceCatalogs",
                        principalColumn: "ServiceCatalogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationId = table.Column<int>(type: "int", nullable: true),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    AppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Examines_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examines",
                        principalColumn: "ExamineId");
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_Appointments_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "ReceptionId");
                });

            migrationBuilder.CreateTable(
                name: "DrugBookings",
                columns: table => new
                {
                    DrugBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamineId = table.Column<int>(type: "int", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: true),
                    DrugCatalogId = table.Column<int>(type: "int", nullable: true),
                    MorningDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoonDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AfternoonDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EveningDose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaysOfSupply = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugBookings", x => x.DrugBookingId);
                    table.ForeignKey(
                        name: "FK_DrugBookings_DrugCatalogs_DrugCatalogId",
                        column: x => x.DrugCatalogId,
                        principalTable: "DrugCatalogs",
                        principalColumn: "DrugCatalogId");
                    table.ForeignKey(
                        name: "FK_DrugBookings_Examines_ExamineId",
                        column: x => x.ExamineId,
                        principalTable: "Examines",
                        principalColumn: "ExamineId");
                    table.ForeignKey(
                        name: "FK_DrugBookings_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId");
                });

            migrationBuilder.CreateTable(
                name: "MedicalServiceRequests",
                columns: table => new
                {
                    MedicalServiceRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ServiceTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    AssignedById = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    AssignedByEmployeeEmployeeId = table.Column<int>(type: "int", nullable: true),
                    AssignedByDoctorDoctorId = table.Column<int>(type: "int", nullable: true),
                    ParentMedicalServiceRequestMedicalServiceRequestId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    ExamineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServiceRequests", x => x.MedicalServiceRequestId);
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_Doctors_AssignedByDoctorDoctorId",
                        column: x => x.AssignedByDoctorDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_Employees_AssignedByEmployeeEmployeeId",
                        column: x => x.AssignedByEmployeeEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_Examines_ExamineId",
                        column: x => x.ExamineId,
                        principalTable: "Examines",
                        principalColumn: "ExamineId");
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_MedicalServiceRequests_ParentMedicalServiceRequestMedicalServiceRequestId",
                        column: x => x.ParentMedicalServiceRequestMedicalServiceRequestId,
                        principalTable: "MedicalServiceRequests",
                        principalColumn: "MedicalServiceRequestId");
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "ReceptionId");
                    table.ForeignKey(
                        name: "FK_MedicalServiceRequests_ServiceCatalogs_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceCatalogs",
                        principalColumn: "ServiceCatalogId");
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticImagingResults",
                columns: table => new
                {
                    DiagnosticImagingResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedicalServiceRequestId = table.Column<int>(type: "int", nullable: true),
                    TechnicianId = table.Column<int>(type: "int", nullable: true),
                    ConclusionDictionaryId = table.Column<int>(type: "int", nullable: true),
                    ConcludingDoctorId = table.Column<int>(type: "int", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultTypeId = table.Column<int>(type: "int", nullable: true),
                    SampleReceivedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleTypeId = table.Column<int>(type: "int", nullable: true),
                    SampleQualityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    GpbmacroDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GpbmicroDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseYearOfBirth = table.Column<int>(type: "int", nullable: true),
                    SampleCollectionLocationMedicalFacilityId = table.Column<int>(type: "int", nullable: true),
                    IsSampleCollectedAtHome = table.Column<int>(type: "int", nullable: true),
                    SampleReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleCollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleCollectionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OtherComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleCollectionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleCollectorId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    ReceptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticImagingResults", x => x.DiagnosticImagingResultId);
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_ConclusionDictionaries_ConclusionDictionaryId",
                        column: x => x.ConclusionDictionaryId,
                        principalTable: "ConclusionDictionaries",
                        principalColumn: "ConclusionDictionaryId");
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_Doctors_ConcludingDoctorId",
                        column: x => x.ConcludingDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_Employees_SampleCollectorId",
                        column: x => x.SampleCollectorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_MedicalServiceRequests_MedicalServiceRequestId",
                        column: x => x.MedicalServiceRequestId,
                        principalTable: "MedicalServiceRequests",
                        principalColumn: "MedicalServiceRequestId");
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_Receptions_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Receptions",
                        principalColumn: "ReceptionId");
                    table.ForeignKey(
                        name: "FK_DiagnosticImagingResults_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExaminationId",
                table: "Appointments",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ReceptionId",
                table: "Appointments",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DistrictId",
                table: "AspNetUsers",
                column: "DistrictId");

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProvinceId",
                table: "AspNetUsers",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WardId",
                table: "AspNetUsers",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ServiceCatalogId",
                table: "BillDetails",
                column: "ServiceCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ReceptionId",
                table: "Bills",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId",
                table: "ChatMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConclusionDictionaries_ServiceCatalogId",
                table: "ConclusionDictionaries",
                column: "ServiceCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentTypeId",
                table: "Departments",
                column: "DepartmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_ConcludingDoctorId",
                table: "DiagnosticImagingResults",
                column: "ConcludingDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_ConclusionDictionaryId",
                table: "DiagnosticImagingResults",
                column: "ConclusionDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_MedicalServiceRequestId",
                table: "DiagnosticImagingResults",
                column: "MedicalServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_PatientId",
                table: "DiagnosticImagingResults",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_ReceptionId",
                table: "DiagnosticImagingResults",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_SampleCollectorId",
                table: "DiagnosticImagingResults",
                column: "SampleCollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImagingResults_TechnicianId",
                table: "DiagnosticImagingResults",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceCode",
                table: "Districts",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AvataId",
                table: "Doctors",
                column: "AvataId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugBookings_DrugCatalogId",
                table: "DrugBookings",
                column: "DrugCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugBookings_ExamineId",
                table: "DrugBookings",
                column: "ExamineId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugBookings_PrescriptionId",
                table: "DrugBookings",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCatalogs_CountryId",
                table: "DrugCatalogs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCatalogs_DepartmentPharmacyId",
                table: "DrugCatalogs",
                column: "DepartmentPharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCatalogs_DrugTypeId",
                table: "DrugCatalogs",
                column: "DrugTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCatalogs_ManufacturerId",
                table: "DrugCatalogs",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugCatalogs_UnitOfMeasureId",
                table: "DrugCatalogs",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailContents_SenderId",
                table: "EmailContents",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AvataId",
                table: "Employees",
                column: "AvataId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

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
                name: "IX_Examines_DiagnosisDictionaryId",
                table: "Examines",
                column: "DiagnosisDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_ICDCatalogId",
                table: "Examines",
                column: "ICDCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_PatientId",
                table: "Examines",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_PrescriptionId",
                table: "Examines",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_ReceptionId",
                table: "Examines",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Examines_TemplatePrescriptionId",
                table: "Examines",
                column: "TemplatePrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_DistrictCode",
                table: "Hospitals",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_ProvinceCode",
                table: "Hospitals",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_WardCode",
                table: "Hospitals",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_ICDcatalogs_ICDCatalogGroupId",
                table: "ICDcatalogs",
                column: "ICDCatalogGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_ConcludingDoctorId",
                table: "LabResults",
                column: "ConcludingDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_PatientId",
                table: "LabResults",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_ReceptionId",
                table: "LabResults",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_TechnicianId",
                table: "LabResults",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_AssignedByDoctorDoctorId",
                table: "MedicalServiceRequests",
                column: "AssignedByDoctorDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_AssignedByEmployeeEmployeeId",
                table: "MedicalServiceRequests",
                column: "AssignedByEmployeeEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_ExamineId",
                table: "MedicalServiceRequests",
                column: "ExamineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_ParentMedicalServiceRequestMedicalServiceRequestId",
                table: "MedicalServiceRequests",
                column: "ParentMedicalServiceRequestMedicalServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_PatientId",
                table: "MedicalServiceRequests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_ReceptionId",
                table: "MedicalServiceRequests",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServiceRequests_ServiceId",
                table: "MedicalServiceRequests",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentMenuId",
                table: "Menus",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DistrictCode",
                table: "Patients",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ImageId",
                table: "Patients",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ProvinceCode",
                table: "Patients",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_WardCode",
                table: "Patients",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_DepartmentId",
                table: "Pharmacies",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_InventoryTypeId",
                table: "Pharmacies",
                column: "InventoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetail_DrugCatalogId",
                table: "PrescriptionDetail",
                column: "DrugCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetail_PrescriptionId",
                table: "PrescriptionDetail",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_AssignedDoctorId",
                table: "Receptions",
                column: "AssignedDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_PatientId",
                table: "Receptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_ReceptionistId",
                table: "Receptions",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_ParentServiceId",
                table: "ServiceCatalogs",
                column: "ParentServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_ServiceCatalogId1",
                table: "ServiceCatalogs",
                column: "ServiceCatalogId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_ServiceGroupId",
                table: "ServiceCatalogs",
                column: "ServiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_ServiceGroupId1",
                table: "ServiceCatalogs",
                column: "ServiceGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalogs_ServiceTypeId",
                table: "ServiceCatalogs",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceStandardValues_ServiceCatalogId",
                table: "ServiceStandardValues",
                column: "ServiceCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_DistrictCode",
                table: "Suppliers",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ProvinceCode",
                table: "Suppliers",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_WardCode",
                table: "Suppliers",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictCode",
                table: "Wards",
                column: "DistrictCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "DiagnosticImagingResults");

            migrationBuilder.DropTable(
                name: "DiseaseGroups");

            migrationBuilder.DropTable(
                name: "DrugBookings");

            migrationBuilder.DropTable(
                name: "EmailContents");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Icdchapters");

            migrationBuilder.DropTable(
                name: "LabResults");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PrescriptionDetail");

            migrationBuilder.DropTable(
                name: "ServiceStandardValues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "ConclusionDictionaries");

            migrationBuilder.DropTable(
                name: "MedicalServiceRequests");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.DropTable(
                name: "MenuPermissions");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "DrugCatalogs");

            migrationBuilder.DropTable(
                name: "Examines");

            migrationBuilder.DropTable(
                name: "ServiceCatalogs");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DrugTypes");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures");

            migrationBuilder.DropTable(
                name: "DiagnosisDictionaries");

            migrationBuilder.DropTable(
                name: "ICDcatalogs");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "TemplatePrescriptions");

            migrationBuilder.DropTable(
                name: "ServiceGroups");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "InventoryTypes");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Savefiles");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "DepartmentTypes");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
