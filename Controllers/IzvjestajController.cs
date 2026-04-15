using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Data;

namespace SistemZaZaposlenike.Controllers;

public class IzvjestajController : Controller
{
    private readonly AppDbContext _context;

    public IzvjestajController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Download()
    {
        var odjeli = await _context.Odjeli
            .Include(o => o.Zaposlenici)
            .OrderBy(o => o.Naziv)
            .ToListAsync();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Izvjestaj");

        int row = 1;

        worksheet.Cell(row, 1).Value = "Izvještaj o zaposlenicima po odjelu";
        worksheet.Cell(row, 1).Style.Font.Bold = true;
        worksheet.Cell(row, 1).Style.Font.FontSize = 16;
        row += 2;

        foreach (var odjel in odjeli)
        {
            worksheet.Cell(row, 1).Value = $"Odjel: {odjel.Naziv}";
            worksheet.Cell(row, 1).Style.Font.Bold = true;
            worksheet.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            row++;

            worksheet.Cell(row, 1).Value = "Ime";
            worksheet.Cell(row, 2).Value = "Prezime";
            worksheet.Cell(row, 3).Value = "Email";
            worksheet.Cell(row, 4).Value = "Plata";
            worksheet.Cell(row, 5).Value = "Datum zaposlenja";

            for (int col = 1; col <= 5; col++)
            {
                worksheet.Cell(row, col).Style.Font.Bold = true;
                worksheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightBlue;
            }

            row++;

            foreach (var zaposlenik in odjel.Zaposlenici.OrderBy(z => z.Prezime).ThenBy(z => z.Ime))
            {
                worksheet.Cell(row, 1).Value = zaposlenik.Ime;
                worksheet.Cell(row, 2).Value = zaposlenik.Prezime;
                worksheet.Cell(row, 3).Value = zaposlenik.Email;
                worksheet.Cell(row, 4).Value = zaposlenik.PlataMonthly;
                worksheet.Cell(row, 5).Value = zaposlenik.DatumZaposlenja.ToString("dd.MM.yyyy");
                row++;
            }

            worksheet.Cell(row, 1).Value = "Ukupan broj zaposlenika:";
            worksheet.Cell(row, 2).Value = odjel.Zaposlenici.Count;
            row++;

            worksheet.Cell(row, 1).Value = "Prosječna plata odjela:";
            worksheet.Cell(row, 2).Value = odjel.Zaposlenici.Any()
                ? odjel.Zaposlenici.Average(z => z.PlataMonthly)
                : 0;
            row += 2;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(
            stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "izvjestaj_zaposlenici.xlsx");
    }
}