using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemZaZaposlenike.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odjeli",
                columns: table => new
                {
                    OdjelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odjeli", x => x.OdjelID);
                });

            migrationBuilder.CreateTable(
                name: "Projekti",
                columns: table => new
                {
                    ProjekatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BudzetUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DatumPocetak = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumKraj = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekti", x => x.ProjekatID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenici",
                columns: table => new
                {
                    ZaposlenikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PlataMonthly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdjelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenici", x => x.ZaposlenikID);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_Odjeli_OdjelID",
                        column: x => x.OdjelID,
                        principalTable: "Odjeli",
                        principalColumn: "OdjelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZaposlenikProjekti",
                columns: table => new
                {
                    ZaposlenikID = table.Column<int>(type: "int", nullable: false),
                    ProjekatID = table.Column<int>(type: "int", nullable: false),
                    Uloga = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SatiNaTjednu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaposlenikProjekti", x => new { x.ZaposlenikID, x.ProjekatID });
                    table.ForeignKey(
                        name: "FK_ZaposlenikProjekti_Projekti_ProjekatID",
                        column: x => x.ProjekatID,
                        principalTable: "Projekti",
                        principalColumn: "ProjekatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZaposlenikProjekti_Zaposlenici_ZaposlenikID",
                        column: x => x.ZaposlenikID,
                        principalTable: "Zaposlenici",
                        principalColumn: "ZaposlenikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_Email",
                table: "Zaposlenici",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_OdjelID",
                table: "Zaposlenici",
                column: "OdjelID");

            migrationBuilder.CreateIndex(
                name: "IX_ZaposlenikProjekti_ProjekatID",
                table: "ZaposlenikProjekti",
                column: "ProjekatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZaposlenikProjekti");

            migrationBuilder.DropTable(
                name: "Projekti");

            migrationBuilder.DropTable(
                name: "Zaposlenici");

            migrationBuilder.DropTable(
                name: "Odjeli");
        }
    }
}
