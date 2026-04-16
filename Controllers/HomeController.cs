using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Data;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var brojZaposlenika = await _context.Zaposlenici.CountAsync();
        var brojOdjela = await _context.Odjeli.CountAsync();
        var brojProjekata = await _context.Projekti.CountAsync();

        var prosjecnaPlata = await _context.Zaposlenici.AnyAsync()
            ? await _context.Zaposlenici.AverageAsync(z => z.PlataMonthly)
            : 0;

        ViewBag.BrojZaposlenika = brojZaposlenika;
        ViewBag.BrojOdjela = brojOdjela;
        ViewBag.BrojProjekata = brojProjekata;
        ViewBag.ProsjecnaPlata = prosjecnaPlata;

        return View();
    }
}