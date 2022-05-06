using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stay.Data.Migrations
{
    public partial class init : Migration
    {
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
                name: "RealtorFirms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealtorLogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealtorFirms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RealtorRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealtorRequests", x => x.Id);
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealtorFirmId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_AspNetUsers_RealtorFirms_RealtorFirmId",
                        column: x => x.RealtorFirmId,
                        principalTable: "RealtorFirms",
                        principalColumn: "Id");
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "RealEstateObjectViewmodel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinRooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxRooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinLivingArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxLivingArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewConstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balcony = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseCheckBox = table.Column<bool>(type: "bit", nullable: false),
                    RowHouseCheckBox = table.Column<bool>(type: "bit", nullable: false),
                    ApartmentCheckbox = table.Column<bool>(type: "bit", nullable: false),
                    HolidayHomeCheckBox = table.Column<bool>(type: "bit", nullable: false),
                    LotCheckBox = table.Column<bool>(type: "bit", nullable: false),
                    FarmCheckBox = table.Column<bool>(type: "bit", nullable: false),
                    OtherCheckBox = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SearchTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateObjectViewmodel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateObjectViewmodel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealtorRequestUser",
                columns: table => new
                {
                    RealtorRequestsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRequestsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealtorRequestUser", x => new { x.RealtorRequestsId, x.UserRequestsId });
                    table.ForeignKey(
                        name: "FK_RealtorRequestUser_AspNetUsers_UserRequestsId",
                        column: x => x.UserRequestsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealtorRequestUser_RealtorRequests_RealtorRequestsId",
                        column: x => x.RealtorRequestsId,
                        principalTable: "RealtorRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropType = table.Column<int>(type: "int", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    NrOfRooms = table.Column<int>(type: "int", nullable: false),
                    LivingArea = table.Column<double>(type: "float", nullable: false),
                    GrossArea = table.Column<double>(type: "float", nullable: false),
                    PlotSize = table.Column<double>(type: "float", nullable: false),
                    ConstructionYear = table.Column<int>(type: "int", nullable: true),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    DateForViewing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Sublocality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrOfViews = table.Column<int>(type: "int", nullable: false),
                    RealtorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RealEstateObjectViewModelId = table.Column<int>(type: "int", nullable: true),
                    RealtorFirmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateObjects_AspNetUsers_RealtorId",
                        column: x => x.RealtorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstateObjects_RealEstateObjectViewmodel_RealEstateObjectViewModelId",
                        column: x => x.RealEstateObjectViewModelId,
                        principalTable: "RealEstateObjectViewmodel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RealEstateObjects_RealtorFirms_RealtorFirmId",
                        column: x => x.RealtorFirmId,
                        principalTable: "RealtorFirms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RealEstateObjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.FileName);
                    table.ForeignKey(
                        name: "FK_Images_RealEstateObjects_RealEstateObjectId",
                        column: x => x.RealEstateObjectId,
                        principalTable: "RealEstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateObjectUser",
                columns: table => new
                {
                    FavoritesId = table.Column<int>(type: "int", nullable: false),
                    UsersFavoritedId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateObjectUser", x => new { x.FavoritesId, x.UsersFavoritedId });
                    table.ForeignKey(
                        name: "FK_RealEstateObjectUser_AspNetUsers_UsersFavoritedId",
                        column: x => x.UsersFavoritedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateObjectUser_RealEstateObjects_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "RealEstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateObjectUser1",
                columns: table => new
                {
                    InterestedUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterestingListingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateObjectUser1", x => new { x.InterestedUsersId, x.InterestingListingsId });
                    table.ForeignKey(
                        name: "FK_RealEstateObjectUser1_AspNetUsers_InterestedUsersId",
                        column: x => x.InterestedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateObjectUser1_RealEstateObjects_InterestingListingsId",
                        column: x => x.InterestingListingsId,
                        principalTable: "RealEstateObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09249631-383f-40fa-8d53-cd84b975e5f6", "1", "Administratör", "Administratör" },
                    { "283043bf-091a-44ea-b389-23ab247d3541", "1", "Mäklare", "Mäklare" },
                    { "3b3d4061-48cc-463b-95c4-eece508245a4", "1", "Chef", "Chef" },
                    { "60aaa647-e0ec-4680-9851-f0293ad22a6b", "1", "Användare", "Användare" }
                });

            migrationBuilder.InsertData(
                table: "RealEstateObjects",
                columns: new[] { "Id", "Address", "Balcony", "City", "ConstructionYear", "ContractType", "DateForViewing", "DateUploaded", "Description", "GrossArea", "Latitude", "LivingArea", "Longitude", "NrOfRooms", "NrOfViews", "PlotSize", "PostalTown", "Price", "PropType", "RealEstateObjectViewModelId", "RealtorFirmId", "RealtorId", "Sublocality", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Östmans väg 2", true, "Haparanda", 1990, 1, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1859), new DateTime(2022, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bästa lägenhet", 120.0, 65.838300000000004, 60.0, 24.112200000000001, 2, 20, 60.0, "Stockholm", 1000000, 0, null, null, null, "Norrmalm", 95334 },
                    { 2, "Pipons väg 2", false, "Haparanda", 1940, 0, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1894), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dåligt lägenhet", 130.0, 65.836100000000002, 60.0, 24.1081, 2, 100, 70.0, "Stockholm", 400000, 0, null, null, null, "Norrmalm", 95334 },
                    { 3, "Sågaregatan 65", false, "Haparanda", null, 1, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1897), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bästa lägenhet", 220.0, 65.827200000000005, 120.0, 24.121600000000001, 4, 99, 100.0, "Stockholm", 4000000, 0, null, null, null, "Norrmalm", 95333 },
                    { 4, "Vindstigen 7", true, "Spånga", 1964, 0, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1900), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dåligt lägenhet", 90.0, 59.3825, 60.0, 17.885400000000001, 1, 1000, 30.0, "Stockholm", 100000, 0, null, null, null, "Norrmalm", 16351 },
                    { 5, "Solhems hagväg 168", true, "Spånga", null, 1, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1904), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bästa lägenhet", 220.0, 59.388599999999997, 60.0, 17.897600000000001, 5, 10, 160.0, "Märsta", 40000000, 3, null, null, null, "Märsta Norra", 16356 },
                    { 6, "Silverdalsvägen 39", false, "Ekerö", 2014, 1, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1906), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dåligt lägenhet", 70.0, 59.281199999999998, 60.0, 17.798200000000001, 3, 22, 10.0, "Märsta", 2100000, 0, null, null, null, "Märsta Norra", 17834 },
                    { 7, "Växthusvägen 1", false, "Ekerö", 1960, 1, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1909), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bästa lägenhet", 1200.0, 59.281199999999998, 200.0, 17.788699999999999, 6, 12, 1000.0, "Märsta", 2900000, 1, null, null, null, "Märsta Norra", 17834 },
                    { 8, "Bostadsvägen 4", false, "Hemma", 1960, 1, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1912), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporär default av min bostad, så att det går att se hur det ser ut när man laddar in sidan", 1200.0, 59.539400000000001, 200.0, 18.428599999999999, 6, 1, 1000.0, "Märsta", 12345678, 1, null, null, null, "Märsta Norra", 12345 },
                    { 9, "Alsättersgatan 13B", false, "Linköping", 1960, 0, new DateTime(2022, 4, 21, 13, 56, 31, 399, DateTimeKind.Local).AddTicks(1914), new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporär default av en bostad på en annan ort, så att det går att se hur auto zoom och auto pan funkar.", 25.0, 58.410431699999997, 25.0, 15.5621776, 1, 5000, 25.0, "Linköping", 5000, 0, null, null, null, "Ryd", 58435 }
                });

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
                name: "IX_AspNetUsers_RealtorFirmId",
                table: "AspNetUsers",
                column: "RealtorFirmId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RealEstateObjectId",
                table: "Images",
                column: "RealEstateObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjects_RealEstateObjectViewModelId",
                table: "RealEstateObjects",
                column: "RealEstateObjectViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjects_RealtorFirmId",
                table: "RealEstateObjects",
                column: "RealtorFirmId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjects_RealtorId",
                table: "RealEstateObjects",
                column: "RealtorId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjectUser_UsersFavoritedId",
                table: "RealEstateObjectUser",
                column: "UsersFavoritedId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjectUser1_InterestingListingsId",
                table: "RealEstateObjectUser1",
                column: "InterestingListingsId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjectViewmodel_UserId",
                table: "RealEstateObjectViewmodel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RealtorRequestUser_UserRequestsId",
                table: "RealtorRequestUser",
                column: "UserRequestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Images");

            migrationBuilder.DropTable(
                name: "RealEstateObjectUser");

            migrationBuilder.DropTable(
                name: "RealEstateObjectUser1");

            migrationBuilder.DropTable(
                name: "RealtorRequestUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "RealEstateObjects");

            migrationBuilder.DropTable(
                name: "RealtorRequests");

            migrationBuilder.DropTable(
                name: "RealEstateObjectViewmodel");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RealtorFirms");
        }
    }
}
