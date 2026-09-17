using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhaTro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    MaPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhongKyHieu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DienTich = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    LoaiPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GiaPhong = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.MaPhong);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    MaDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.MaDichVu);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    MaNguoiThue = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    QueQuan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.MaNguoiThue);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
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
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtilityReadings",
                columns: table => new
                {
                    MaGhiChiSo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    KyGhi = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    ChiSoDienCu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChiSoDienMoi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGiaDien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ChiSoNuocCu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChiSoNuocMoi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGiaNuoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityReadings", x => x.MaGhiChiSo);
                    table.CheckConstraint("CK_Utility_Dien", "[ChiSoDienMoi] >= [ChiSoDienCu]");
                    table.CheckConstraint("CK_Utility_Nuoc", "[ChiSoNuocMoi] >= [ChiSoNuocCu]");
                    table.ForeignKey(
                        name: "FK_UtilityReadings_Rooms_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Rooms",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUsages",
                columns: table => new
                {
                    MaSuDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    KySuDung = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    SoLuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGiaApDung = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUsages", x => x.MaSuDung);
                    table.ForeignKey(
                        name: "FK_ServiceUsages_Rooms_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Rooms",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceUsages_Services_MaDichVu",
                        column: x => x.MaDichVu,
                        principalTable: "Services",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    MaHopDong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHopDongKyHieu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    MaNguoiThue = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiaThue = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienCoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.MaHopDong);
                    table.CheckConstraint("CK_Contract_Ngay", "[NgayKetThuc] > [NgayBatDau]");
                    table.ForeignKey(
                        name: "FK_Contracts_Rooms_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Rooms",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Tenants_MaNguoiThue",
                        column: x => x.MaNguoiThue,
                        principalTable: "Tenants",
                        principalColumn: "MaNguoiThue",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
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
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractHistories",
                columns: table => new
                {
                    MaLichSu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHopDong = table.Column<int>(type: "int", nullable: false),
                    HanhDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractHistories", x => x.MaLichSu);
                    table.ForeignKey(
                        name: "FK_ContractHistories_Contracts_MaHopDong",
                        column: x => x.MaHopDong,
                        principalTable: "Contracts",
                        principalColumn: "MaHopDong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDonKyHieu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    MaHopDong = table.Column<int>(type: "int", nullable: true),
                    KyHoaDon = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TienPhong = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienDien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienNuoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienDichVu = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    HanThanhToan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThaiThanhToan = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_Invoices_Contracts_MaHopDong",
                        column: x => x.MaHopDong,
                        principalTable: "Contracts",
                        principalColumn: "MaHopDong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Rooms_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Rooms",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoveOuts",
                columns: table => new
                {
                    MaTraPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHopDong = table.Column<int>(type: "int", nullable: false),
                    NgayTraPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CongNoTruocTraPhong = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienPhatSinhCuoiKy = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    KhoanPhaiThuCuoiCung = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienCocBanDau = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienKhauTruCoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienHoanCoc = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TienPhaiTraThem = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DaQuyetToan = table.Column<bool>(type: "bit", nullable: false),
                    TrangThaiPhongSauTra = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveOuts", x => x.MaTraPhong);
                    table.ForeignKey(
                        name: "FK_MoveOuts_Contracts_MaHopDong",
                        column: x => x.MaHopDong,
                        principalTable: "Contracts",
                        principalColumn: "MaHopDong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    MaThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhuongThuc = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.MaThanhToan);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "Invoices",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractHistories_MaHopDong",
                table: "ContractHistories",
                column: "MaHopDong");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_MaHopDongKyHieu",
                table: "Contracts",
                column: "MaHopDongKyHieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_MaNguoiThue",
                table: "Contracts",
                column: "MaNguoiThue");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_MaPhong",
                table: "Contracts",
                column: "MaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MaHoaDonKyHieu",
                table: "Invoices",
                column: "MaHoaDonKyHieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MaHopDong",
                table: "Invoices",
                column: "MaHopDong");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MaPhong_KyHoaDon",
                table: "Invoices",
                columns: new[] { "MaPhong", "KyHoaDon" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveOuts_MaHopDong",
                table: "MoveOuts",
                column: "MaHopDong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MaHoaDon",
                table: "Payments",
                column: "MaHoaDon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MaPhongKyHieu",
                table: "Rooms",
                column: "MaPhongKyHieu",
                unique: true,
                filter: "[DaXoa] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Services_TenDichVu",
                table: "Services",
                column: "TenDichVu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUsages_MaDichVu",
                table: "ServiceUsages",
                column: "MaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUsages_MaPhong_MaDichVu_KySuDung",
                table: "ServiceUsages",
                columns: new[] { "MaPhong", "MaDichVu", "KySuDung" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_CCCD",
                table: "Tenants",
                column: "CCCD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UtilityReadings_MaPhong_KyGhi",
                table: "UtilityReadings",
                columns: new[] { "MaPhong", "KyGhi" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractHistories");

            migrationBuilder.DropTable(
                name: "MoveOuts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "ServiceUsages");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UtilityReadings");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
