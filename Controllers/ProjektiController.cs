using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Data;

namespace SistemZaZaposlenike.Controllers;

public class ProjektiController : Controller
{
    private readonly AppDbContext _context;

    public ProjektiController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var projekti = await _context.Projekti
            .Include(p => p.ZaposlenikProjekti)
            .OrderBy(p => p.Naziv)
            .ToListAsync();

        return View(projekti);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var projekat = await _context.Projekti
            .Include(p => p.ZaposlenikProjekti)
                .ThenInclude(zp => zp.Zaposlenik)
            .FirstOrDefaultAsync(p => p.ProjekatID == id);

        if (projekat == null)
            return NotFound();

        return View(projekat);
    }
}