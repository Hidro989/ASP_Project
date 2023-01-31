using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThiTracNghiem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "MaThi",
                columns: table => new
                {
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SLSD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaThi", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "MonThi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongDe = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonThi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeThi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDeThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongCauHoi = table.Column<int>(type: "int", nullable: true),
                    ThoiGian = table.Column<int>(type: "int", nullable: true),
                    MonThiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeThi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeThi_MonThi_MonThiID",
                        column: x => x.MonThiID,
                        principalTable: "MonThi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHoi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    A = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    B = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    C = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DapAnDung = table.Column<int>(type: "int", nullable: false),
                    DeThiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CauHoi_DeThi_DeThiID",
                        column: x => x.DeThiID,
                        principalTable: "DeThi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CauHoi_DeThiID",
                table: "CauHoi",
                column: "DeThiID");

            migrationBuilder.CreateIndex(
                name: "IX_DeThi_MonThiID",
                table: "DeThi",
                column: "MonThiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CauHoi");

            migrationBuilder.DropTable(
                name: "MaThi");

            migrationBuilder.DropTable(
                name: "DeThi");

            migrationBuilder.DropTable(
                name: "MonThi");
        }
    }
}
