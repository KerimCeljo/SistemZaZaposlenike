using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemZaZaposlenike.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Odjeli",
                columns: new[] { "OdjelID", "Lokacija", "Naziv" },
                values: new object[,]
                {
                    { 1, "Sarajevo", "IT" },
                    { 2, "Sarajevo", "HR" },
                    { 3, "Sarajevo", "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Projekti",
                columns: new[] { "ProjekatID", "BudzetUSD", "DatumKraj", "DatumPocetak", "Naziv" },
                values: new object[,]
                {
                    { 1, 15000m, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Web App" },
                    { 2, 22000m, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mobile App" },
                    { 3, 18000m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "API System" },
                    { 4, 12000m, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Internal Tool" }
                });

            migrationBuilder.InsertData(
                table: "Zaposlenici",
                columns: new[] { "ZaposlenikID", "DatumZaposlenja", "Email", "Ime", "OdjelID", "PlataMonthly", "Prezime" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "kerim@test.com", "Kerim", 1, 2000m, "Celjo" },
                    { 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "amir@test.com", "Amir", 1, 1800m, "Hodzic" },
                    { 3, new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "lejla@test.com", "Lejla", 2, 1700m, "Kovacevic" },
                    { 4, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara@test.com", "Sara", 2, 1600m, "Basic" },
                    { 5, new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "adnan@test.com", "Adnan", 3, 2200m, "Suljic" },
                    { 6, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "haris@test.com", "Haris", 3, 2100m, "Delic" },
                    { 7, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "maja@test.com", "Maja", 1, 1900m, "Ilic" },
                    { 8, new DateTime(2023, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivan@test.com", "Ivan", 2, 1750m, "Peric" },
                    { 9, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "amina@test.com", "Amina", 3, 1650m, "Smajic" },
                    { 10, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "nermin@test.com", "Nermin", 1, 2000m, "Zukic" }
                });

            migrationBuilder.InsertData(
                table: "ZaposlenikProjekti",
                columns: new[] { "ProjekatID", "ZaposlenikID", "SatiNaTjednu", "Uloga" },
                values: new object[,]
                {
                    { 1, 1, 40, "Developer" },
                    { 1, 2, 35, "Backend" },
                    { 2, 3, 20, "HR Support" },
                    { 3, 4, 25, "QA" },
                    { 4, 5, 30, "Finance Lead" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ZaposlenikProjekti",
                keyColumns: new[] { "ProjekatID", "ZaposlenikID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ZaposlenikProjekti",
                keyColumns: new[] { "ProjekatID", "ZaposlenikID" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ZaposlenikProjekti",
                keyColumns: new[] { "ProjekatID", "ZaposlenikID" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ZaposlenikProjekti",
                keyColumns: new[] { "ProjekatID", "ZaposlenikID" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "ZaposlenikProjekti",
                keyColumns: new[] { "ProjekatID", "ZaposlenikID" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "Projekti",
                keyColumn: "ProjekatID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projekti",
                keyColumn: "ProjekatID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projekti",
                keyColumn: "ProjekatID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projekti",
                keyColumn: "ProjekatID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zaposlenici",
                keyColumn: "ZaposlenikID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Odjeli",
                keyColumn: "OdjelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Odjeli",
                keyColumn: "OdjelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Odjeli",
                keyColumn: "OdjelID",
                keyValue: 3);
        }
    }
}
