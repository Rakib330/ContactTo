using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSMS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activityStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    statusName = table.Column<string>(nullable: false),
                    statusNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activityStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    roleNature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    org = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "awards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    awardName = table.Column<string>(nullable: false),
                    awardNameBn = table.Column<string>(nullable: true),
                    awardShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_awards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    bookCategoey = table.Column<string>(nullable: false),
                    bookCategoeyBn = table.Column<string>(nullable: true),
                    bookCategoryShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bulkHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    number = table.Column<string>(nullable: true),
                    text = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    groupId = table.Column<int>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bulkHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cadres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    cadreName = table.Column<string>(nullable: false),
                    cadreNameBn = table.Column<string>(nullable: true),
                    cadreShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    countryCode = table.Column<string>(nullable: false),
                    countryName = table.Column<string>(nullable: false),
                    countryNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "courseTitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    nameEN = table.Column<string>(nullable: true),
                    nameBN = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbChangeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    entityName = table.Column<string>(maxLength: 300, nullable: true),
                    entityState = table.Column<string>(maxLength: 100, nullable: true),
                    fieldName = table.Column<string>(maxLength: 200, nullable: true),
                    fieldValue = table.Column<string>(nullable: true),
                    sessionId = table.Column<long>(nullable: false),
                    remarks = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbChangeHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    deptCode = table.Column<string>(nullable: false),
                    deptName = table.Column<string>(nullable: false),
                    deptNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    designationCode = table.Column<string>(nullable: false),
                    designationName = table.Column<string>(nullable: false),
                    designationNameBN = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    empType = table.Column<string>(nullable: false),
                    empTypeBn = table.Column<string>(nullable: true),
                    charge = table.Column<decimal>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "holidays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    weeklyHoliday = table.Column<string>(nullable: false),
                    holidayName = table.Column<string>(nullable: false),
                    holidayNameBn = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    languageName = table.Column<string>(nullable: false),
                    languageNameBn = table.Column<string>(nullable: true),
                    languageShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "levelofEducations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    levelofeducationName = table.Column<string>(nullable: false),
                    levelofeducationNameBn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_levelofEducations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "memberGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    membershipName = table.Column<string>(nullable: false),
                    membershipNameBn = table.Column<string>(nullable: true),
                    membershipShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    moduleName = table.Column<string>(nullable: true),
                    moduleNameBN = table.Column<string>(nullable: true),
                    shortOrder = table.Column<int>(nullable: true),
                    isTeam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "municipilityLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    locationName = table.Column<string>(nullable: false),
                    locationNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipilityLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "noticeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_noticeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "occupations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    occupationName = table.Column<string>(nullable: false),
                    occupationNameBn = table.Column<string>(nullable: true),
                    occupationShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occupations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    organizationName = table.Column<string>(nullable: false),
                    organizationNameBn = table.Column<string>(nullable: true),
                    organizationType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "participantHeads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participantHeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "participantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participantTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "relations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    relationName = table.Column<string>(nullable: false),
                    relationNameBn = table.Column<string>(nullable: true),
                    relationShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "religions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    nameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    resultName = table.Column<string>(nullable: false),
                    resultNameBn = table.Column<string>(nullable: true),
                    resultShortName = table.Column<string>(nullable: true),
                    resultMaxValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "salaryGrades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    gradeName = table.Column<string>(nullable: false),
                    basicAmount = table.Column<decimal>(nullable: false),
                    payScale = table.Column<string>(nullable: false),
                    currentBasic = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salaryGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "serviceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    statusName = table.Column<string>(nullable: false),
                    statusNameBn = table.Column<string>(nullable: true),
                    statusShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "spacialSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    skilName = table.Column<string>(nullable: false),
                    skilNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spacialSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    subjectName = table.Column<string>(nullable: false),
                    subjectNameBn = table.Column<string>(nullable: true),
                    subjectShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trainingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    trainingCategoryName = table.Column<string>(nullable: false),
                    trainingCategoryNameBn = table.Column<string>(nullable: true),
                    trainingCategoryShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trainingInstitutes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    trainingInstituteName = table.Column<string>(nullable: false),
                    trainingInstituteNameBn = table.Column<string>(nullable: true),
                    trainingInstituteShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingInstitutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "travelPurposes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    purposeName = table.Column<string>(nullable: false),
                    purposeNameBn = table.Column<string>(nullable: true),
                    purposeShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travelPurposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userBackups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    EmpCode = table.Column<string>(nullable: true),
                    userTypeId = table.Column<int>(nullable: true),
                    userId = table.Column<int>(nullable: true),
                    companyId = table.Column<int>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    AspnetId = table.Column<string>(nullable: true),
                    MaxAmount = table.Column<decimal>(nullable: true),
                    isActive = table.Column<int>(nullable: true),
                    org = table.Column<string>(nullable: true),
                    specialBranchUnitId = table.Column<int>(nullable: true),
                    deleteBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userBackups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userLogHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    userId = table.Column<string>(maxLength: 250, nullable: true),
                    logTime = table.Column<DateTime>(maxLength: 250, nullable: false),
                    status = table.Column<int>(nullable: true),
                    ipAddress = table.Column<string>(maxLength: 250, nullable: true),
                    browserName = table.Column<string>(maxLength: 250, nullable: true),
                    pcName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userLogHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userOTPLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    receiver = table.Column<string>(nullable: true),
                    otp = table.Column<string>(nullable: true),
                    expireDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userOTPLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    userTypeNameBn = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    userTypeName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    shortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    vehicleType = table.Column<string>(nullable: false),
                    vehicleTypeBn = table.Column<string>(nullable: true),
                    vehicleShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "years",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    year = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_years", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "divisions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    divisionCode = table.Column<string>(nullable: false),
                    divisionName = table.Column<string>(nullable: false),
                    divisionNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true),
                    countryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_divisions_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "degrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    degreeName = table.Column<string>(nullable: false),
                    degreeNameBn = table.Column<string>(nullable: true),
                    degreeShortName = table.Column<string>(nullable: true),
                    levelofeducationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_degrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_degrees_levelofEducations_levelofeducationId",
                        column: x => x.levelofeducationId,
                        principalTable: "levelofEducations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moduleAccessPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    eRPModuleId = table.Column<int>(nullable: true),
                    applicationRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moduleAccessPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_moduleAccessPages_AspNetRoles_applicationRoleId",
                        column: x => x.applicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moduleAccessPages_modules_eRPModuleId",
                        column: x => x.eRPModuleId,
                        principalTable: "modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "navbars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    nameOptionBangla = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    nameOption = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    controller = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    action = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    imageClass = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    activeLi = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    status = table.Column<bool>(nullable: false),
                    parentID = table.Column<int>(nullable: false),
                    isParent = table.Column<int>(nullable: true),
                    displayOrder = table.Column<int>(nullable: true),
                    moduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navbars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_navbars_modules_moduleId",
                        column: x => x.moduleId,
                        principalTable: "modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employeeInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeCode = table.Column<string>(maxLength: 50, nullable: false),
                    nationalID = table.Column<string>(maxLength: 100, nullable: true),
                    birthIdentificationNo = table.Column<string>(maxLength: 100, nullable: true),
                    govtID = table.Column<string>(maxLength: 250, nullable: true),
                    gpfNomineeName = table.Column<string>(nullable: true),
                    gpfAcNo = table.Column<string>(nullable: true),
                    nameEnglish = table.Column<string>(nullable: true),
                    nameBangla = table.Column<string>(nullable: true),
                    motherNameEnglish = table.Column<string>(nullable: true),
                    motherNameBangla = table.Column<string>(nullable: true),
                    fatherNameEnglish = table.Column<string>(nullable: true),
                    fatherNameBangla = table.Column<string>(nullable: true),
                    nationality = table.Column<string>(nullable: true),
                    disability = table.Column<string>(nullable: true),
                    tShirtSize = table.Column<string>(nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: true),
                    joiningDatePresentWorkstation = table.Column<DateTime>(nullable: true),
                    joiningDateGovtService = table.Column<DateTime>(nullable: true),
                    dateofregularity = table.Column<DateTime>(nullable: true),
                    dateOfPermanent = table.Column<DateTime>(nullable: true),
                    LPRDate = table.Column<string>(nullable: true),
                    PRLStartDate = table.Column<string>(nullable: true),
                    PRLEndDate = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    birthPlace = table.Column<string>(nullable: true),
                    maritalStatus = table.Column<string>(nullable: true),
                    religionId = table.Column<int>(nullable: true),
                    employeeTypeId = table.Column<int>(nullable: true),
                    tin = table.Column<string>(nullable: true),
                    batch = table.Column<string>(nullable: true),
                    bloodGroup = table.Column<string>(nullable: true),
                    freedomFighter = table.Column<bool>(nullable: false),
                    freedomFighterNo = table.Column<string>(nullable: true),
                    telephoneOffice = table.Column<string>(nullable: true),
                    telephoneResidence = table.Column<string>(nullable: true),
                    pabx = table.Column<string>(nullable: true),
                    emailAddress = table.Column<string>(nullable: true),
                    emailAddressPersonal = table.Column<string>(nullable: true),
                    mobileNumberOffice = table.Column<string>(maxLength: 50, nullable: true),
                    mobileNumberPersonal = table.Column<string>(maxLength: 50, nullable: true),
                    specialSkill = table.Column<string>(nullable: true),
                    seniorityNumber = table.Column<string>(maxLength: 50, nullable: true),
                    designation = table.Column<string>(nullable: true),
                    post = table.Column<int>(nullable: true),
                    designationCheck = table.Column<int>(nullable: false),
                    joiningDesignation = table.Column<string>(nullable: true),
                    natureOfRequitment = table.Column<string>(maxLength: 100, nullable: true),
                    homeDistrict = table.Column<string>(nullable: true),
                    spacialSkillIds = table.Column<string>(nullable: true),
                    spacialSkills = table.Column<string>(nullable: true),
                    orgType = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeInfos_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employeeInfos_employeeTypes_employeeTypeId",
                        column: x => x.employeeTypeId,
                        principalTable: "employeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employeeInfos_religions_religionId",
                        column: x => x.religionId,
                        principalTable: "religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    districtCode = table.Column<string>(nullable: false),
                    districtName = table.Column<string>(nullable: false),
                    districtNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true),
                    divisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_districts_divisions_divisionId",
                        column: x => x.divisionId,
                        principalTable: "divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "relDegreeSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    degreeId = table.Column<int>(nullable: false),
                    subjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relDegreeSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_relDegreeSubjects_degrees_degreeId",
                        column: x => x.degreeId,
                        principalTable: "degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_relDegreeSubjects_subjects_subjectId",
                        column: x => x.subjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userAccessPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    navbarId = table.Column<int>(nullable: true),
                    isAccess = table.Column<int>(nullable: true),
                    applicationRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAccessPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userAccessPages_AspNetRoles_applicationRoleId",
                        column: x => x.applicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userAccessPages_navbars_navbarId",
                        column: x => x.navbarId,
                        principalTable: "navbars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "childrens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    childName = table.Column<string>(nullable: true),
                    childNameBN = table.Column<string>(nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: true),
                    education = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    bin = table.Column<string>(nullable: true),
                    occupation = table.Column<string>(nullable: true),
                    designation = table.Column<string>(nullable: true),
                    organization = table.Column<string>(nullable: true),
                    nid = table.Column<string>(maxLength: 100, nullable: true),
                    bloodGroup = table.Column<string>(nullable: true),
                    disability = table.Column<int>(nullable: false),
                    spacialSkillIds = table.Column<string>(nullable: true),
                    spacialSkills = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_childrens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_childrens_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "drivingLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    licenseNumber = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    placeOfIssue = table.Column<string>(nullable: true),
                    dateOfIssue = table.Column<DateTime>(nullable: true),
                    dateOfExpair = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivingLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_drivingLicenses_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeAwards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    awardName = table.Column<string>(nullable: true),
                    purpose = table.Column<string>(nullable: true),
                    awardDate = table.Column<DateTime>(nullable: true),
                    issuedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeAwards_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeeLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    reading = table.Column<string>(maxLength: 50, nullable: true),
                    writing = table.Column<string>(maxLength: 50, nullable: true),
                    speaking = table.Column<string>(maxLength: 50, nullable: true),
                    languageId = table.Column<int>(nullable: true),
                    proficiency = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeLanguages_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeLanguages_languages_languageId",
                        column: x => x.languageId,
                        principalTable: "languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employeeMemberships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    nameOrganization = table.Column<string>(nullable: true),
                    membershipType = table.Column<string>(nullable: true),
                    membershipId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeMemberships_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeMemberships_memberships_membershipId",
                        column: x => x.membershipId,
                        principalTable: "memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "passportDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    passportNumber = table.Column<string>(nullable: true),
                    placeOfIssue = table.Column<string>(nullable: true),
                    dateOfIssue = table.Column<DateTime>(nullable: true),
                    dateOfExpair = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_passportDetails_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "photographs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    url = table.Column<string>(nullable: false),
                    remarks = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photographs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photographs_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    publicationName = table.Column<string>(nullable: true),
                    publicationNubmer = table.Column<string>(nullable: true),
                    publicationType = table.Column<string>(nullable: true),
                    publicationPlace = table.Column<string>(nullable: true),
                    publicationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_publications_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rlnMemberGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    memberGroupId = table.Column<int>(nullable: true),
                    employeeId = table.Column<int>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rlnMemberGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rlnMemberGroups_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rlnMemberGroups_memberGroups_memberGroupId",
                        column: x => x.memberGroupId,
                        principalTable: "memberGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "spouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    spouseName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    spouseNameBN = table.Column<string>(nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: true),
                    dateOfMarriage = table.Column<DateTime>(nullable: true),
                    occupation = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    designation = table.Column<string>(nullable: true),
                    organization = table.Column<string>(nullable: true),
                    bin = table.Column<string>(nullable: true),
                    nid = table.Column<string>(nullable: true),
                    bloodGroup = table.Column<string>(nullable: true),
                    contact = table.Column<string>(nullable: true),
                    highestEducation = table.Column<string>(nullable: true),
                    homeDistrict = table.Column<string>(nullable: true),
                    spacialSkillIds = table.Column<string>(nullable: true),
                    spacialSkills = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_spouses_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "traningLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    fromDate = table.Column<DateTime>(nullable: true),
                    toDate = table.Column<DateTime>(nullable: true),
                    countryId = table.Column<int>(nullable: true),
                    trainingCategoryId = table.Column<int>(nullable: true),
                    trainingInstituteId = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    trainingTitle = table.Column<string>(nullable: true),
                    sponsoringAgency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traningLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_traningLogs_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_traningLogs_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_traningLogs_trainingCategories_trainingCategoryId",
                        column: x => x.trainingCategoryId,
                        principalTable: "trainingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_traningLogs_trainingInstitutes_trainingInstituteId",
                        column: x => x.trainingInstituteId,
                        principalTable: "trainingInstitutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transferLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    workStation = table.Column<string>(nullable: true),
                    from = table.Column<DateTime>(nullable: true),
                    to = table.Column<DateTime>(nullable: true),
                    designation = table.Column<string>(nullable: true),
                    department = table.Column<string>(nullable: true),
                    salaryGradeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transferLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transferLogs_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transferLogs_salaryGrades_salaryGradeId",
                        column: x => x.salaryGradeId,
                        principalTable: "salaryGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "thanas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    thanaCode = table.Column<string>(nullable: false),
                    thanaName = table.Column<string>(nullable: false),
                    thanaNameBn = table.Column<string>(nullable: true),
                    shortName = table.Column<string>(nullable: true),
                    districtId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thanas_districts_districtId",
                        column: x => x.districtId,
                        principalTable: "districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "educationalQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    institution = table.Column<string>(nullable: true),
                    resultId = table.Column<int>(nullable: true),
                    majorGroup = table.Column<string>(nullable: true),
                    grade = table.Column<string>(nullable: true),
                    passingYear = table.Column<int>(nullable: true),
                    degreeId = table.Column<int>(nullable: true),
                    organizationId = table.Column<int>(nullable: true),
                    reldegreesubjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educationalQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_educationalQualifications_degrees_degreeId",
                        column: x => x.degreeId,
                        principalTable: "degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_educationalQualifications_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_educationalQualifications_organizations_organizationId",
                        column: x => x.organizationId,
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_educationalQualifications_relDegreeSubjects_reldegreesubjectId",
                        column: x => x.reldegreesubjectId,
                        principalTable: "relDegreeSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_educationalQualifications_results_resultId",
                        column: x => x.resultId,
                        principalTable: "results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(nullable: true),
                    updatedBy = table.Column<string>(nullable: true),
                    employeeId = table.Column<int>(nullable: false),
                    countryId = table.Column<int>(nullable: true),
                    divisionId = table.Column<int>(nullable: true),
                    districtId = table.Column<int>(nullable: true),
                    thanaId = table.Column<int>(nullable: true),
                    union = table.Column<string>(nullable: true),
                    postOffice = table.Column<string>(nullable: true),
                    postCode = table.Column<string>(nullable: true),
                    blockSector = table.Column<string>(nullable: true),
                    houseVillage = table.Column<string>(nullable: true),
                    roadNumber = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_addresses_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_addresses_districts_districtId",
                        column: x => x.districtId,
                        principalTable: "districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_addresses_divisions_divisionId",
                        column: x => x.divisionId,
                        principalTable: "divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_addresses_employeeInfos_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employeeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addresses_thanas_thanaId",
                        column: x => x.thanaId,
                        principalTable: "thanas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_countryId",
                table: "addresses",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_districtId",
                table: "addresses",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_divisionId",
                table: "addresses",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_employeeId",
                table: "addresses",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_thanaId",
                table: "addresses",
                column: "thanaId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_childrens_employeeId",
                table: "childrens",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_degrees_levelofeducationId",
                table: "degrees",
                column: "levelofeducationId");

            migrationBuilder.CreateIndex(
                name: "IX_districts_divisionId",
                table: "districts",
                column: "divisionId");

            migrationBuilder.CreateIndex(
                name: "IX_divisions_countryId",
                table: "divisions",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_drivingLicenses_employeeId",
                table: "drivingLicenses",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_educationalQualifications_degreeId",
                table: "educationalQualifications",
                column: "degreeId");

            migrationBuilder.CreateIndex(
                name: "IX_educationalQualifications_employeeId",
                table: "educationalQualifications",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_educationalQualifications_organizationId",
                table: "educationalQualifications",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_educationalQualifications_reldegreesubjectId",
                table: "educationalQualifications",
                column: "reldegreesubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_educationalQualifications_resultId",
                table: "educationalQualifications",
                column: "resultId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeAwards_employeeId",
                table: "employeeAwards",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeInfos_ApplicationUserId",
                table: "employeeInfos",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeInfos_employeeTypeId",
                table: "employeeInfos",
                column: "employeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeInfos_religionId",
                table: "employeeInfos",
                column: "religionId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeLanguages_employeeId",
                table: "employeeLanguages",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeLanguages_languageId",
                table: "employeeLanguages",
                column: "languageId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeMemberships_employeeId",
                table: "employeeMemberships",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeeMemberships_membershipId",
                table: "employeeMemberships",
                column: "membershipId");

            migrationBuilder.CreateIndex(
                name: "IX_moduleAccessPages_applicationRoleId",
                table: "moduleAccessPages",
                column: "applicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_moduleAccessPages_eRPModuleId",
                table: "moduleAccessPages",
                column: "eRPModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_navbars_moduleId",
                table: "navbars",
                column: "moduleId");

            migrationBuilder.CreateIndex(
                name: "IX_passportDetails_employeeId",
                table: "passportDetails",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_photographs_employeeId",
                table: "photographs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_publications_employeeId",
                table: "publications",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_relDegreeSubjects_degreeId",
                table: "relDegreeSubjects",
                column: "degreeId");

            migrationBuilder.CreateIndex(
                name: "IX_relDegreeSubjects_subjectId",
                table: "relDegreeSubjects",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_rlnMemberGroups_employeeId",
                table: "rlnMemberGroups",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_rlnMemberGroups_memberGroupId",
                table: "rlnMemberGroups",
                column: "memberGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_spouses_employeeId",
                table: "spouses",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_thanas_districtId",
                table: "thanas",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_traningLogs_countryId",
                table: "traningLogs",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_traningLogs_employeeId",
                table: "traningLogs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_traningLogs_trainingCategoryId",
                table: "traningLogs",
                column: "trainingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_traningLogs_trainingInstituteId",
                table: "traningLogs",
                column: "trainingInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_transferLogs_employeeId",
                table: "transferLogs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_transferLogs_salaryGradeId",
                table: "transferLogs",
                column: "salaryGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_userAccessPages_applicationRoleId",
                table: "userAccessPages",
                column: "applicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userAccessPages_navbarId",
                table: "userAccessPages",
                column: "navbarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activityStatuses");

            migrationBuilder.DropTable(
                name: "addresses");

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
                name: "awards");

            migrationBuilder.DropTable(
                name: "bookCategories");

            migrationBuilder.DropTable(
                name: "bulkHistories");

            migrationBuilder.DropTable(
                name: "cadres");

            migrationBuilder.DropTable(
                name: "childrens");

            migrationBuilder.DropTable(
                name: "courseTitles");

            migrationBuilder.DropTable(
                name: "DbChangeHistories");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "drivingLicenses");

            migrationBuilder.DropTable(
                name: "educationalQualifications");

            migrationBuilder.DropTable(
                name: "employeeAwards");

            migrationBuilder.DropTable(
                name: "employeeLanguages");

            migrationBuilder.DropTable(
                name: "employeeMemberships");

            migrationBuilder.DropTable(
                name: "eventCategories");

            migrationBuilder.DropTable(
                name: "holidays");

            migrationBuilder.DropTable(
                name: "moduleAccessPages");

            migrationBuilder.DropTable(
                name: "municipilityLocations");

            migrationBuilder.DropTable(
                name: "noticeTypes");

            migrationBuilder.DropTable(
                name: "occupations");

            migrationBuilder.DropTable(
                name: "participantHeads");

            migrationBuilder.DropTable(
                name: "participantTypes");

            migrationBuilder.DropTable(
                name: "passportDetails");

            migrationBuilder.DropTable(
                name: "photographs");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "relations");

            migrationBuilder.DropTable(
                name: "rlnMemberGroups");

            migrationBuilder.DropTable(
                name: "serviceStatuses");

            migrationBuilder.DropTable(
                name: "spacialSkills");

            migrationBuilder.DropTable(
                name: "spouses");

            migrationBuilder.DropTable(
                name: "traningLogs");

            migrationBuilder.DropTable(
                name: "transferLogs");

            migrationBuilder.DropTable(
                name: "travelPurposes");

            migrationBuilder.DropTable(
                name: "userAccessPages");

            migrationBuilder.DropTable(
                name: "userBackups");

            migrationBuilder.DropTable(
                name: "userLogHistories");

            migrationBuilder.DropTable(
                name: "userOTPLogs");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "years");

            migrationBuilder.DropTable(
                name: "thanas");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "relDegreeSubjects");

            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "memberships");

            migrationBuilder.DropTable(
                name: "memberGroups");

            migrationBuilder.DropTable(
                name: "trainingCategories");

            migrationBuilder.DropTable(
                name: "trainingInstitutes");

            migrationBuilder.DropTable(
                name: "employeeInfos");

            migrationBuilder.DropTable(
                name: "salaryGrades");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "navbars");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "degrees");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "employeeTypes");

            migrationBuilder.DropTable(
                name: "religions");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "divisions");

            migrationBuilder.DropTable(
                name: "levelofEducations");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
