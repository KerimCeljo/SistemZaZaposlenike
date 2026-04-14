using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Data;

namespace SistemZaZaposlenike.Controllers;

public class OdjeliController : Controller
{
    private readonly AppDbContext _context;

    public OdjeliController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var odjeli = await _context.Odjeli
            .Include(o => o.Zaposlenici)
            .OrderBy(o => o.Naziv)
            .ToListAsync();

        return View(odjeli);
    }
}