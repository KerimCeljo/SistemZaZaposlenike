using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Data;
using SistemZaZaposlenike.Models;
using System.Threading.Tasks;

namespace SistemZaZaposlenike.Controllers;

public class ZaposleniciController : Controller
{
    private readonly AppDbContext _context;

    public ZaposleniciController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? odjelId)
    {
        var odjeli = await _context.Odjeli
            .OrderBy(o => o.Naziv)
            .ToListAsync();

        ViewBag.Odjeli = new SelectList(odjeli, "OdjelID", "Naziv", odjelId);
        ViewBag.OdabraniOdjelId = odjelId;

        var query = _context.Zaposlenici
            .Include(z => z.Odjel)
            .AsQueryable();

        if (odjelId.HasValue)
        {
            query = query.Where(z => z.OdjelID == odjelId.Value);
        }

        var zaposlenici = await query
            .OrderBy(z => z.Prezime)
            .ThenBy(z => z.Ime)
            .ToListAsync();

        return View(zaposlenici);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var zaposlenik = await _context.Zaposlenici
            .Include(z => z.Odjel)
            .FirstOrDefaultAsync(z => z.ZaposlenikID == id);

        if (zaposlenik == null)
            return NotFound();

        return View(zaposlenik);
    }

    public async Task<IActionResult> Create()
    {
        await UcitajOdjele();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Zaposlenik zaposlenik)
    {
        if (await _context.Zaposlenici.AnyAsync(z => z.Email == zaposlenik.Email))
        {
            ModelState.AddModelError("Email", "Ovaj email već postoji u sistemu.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(zaposlenik);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Zaposlenik je uspješno dodan.";
            return RedirectToAction(nameof(Index));
        }

        await UcitajOdjele(zaposlenik.OdjelID);
        return View(zaposlenik);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var zaposlenik = await _context.Zaposlenici.FindAsync(id);

        if (zaposlenik == null)
            return NotFound();

        await UcitajOdjele(zaposlenik.OdjelID);
        return View(zaposlenik);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Zaposlenik zaposlenik)
    {
        if (id != zaposlenik.ZaposlenikID)
            return NotFound();

        if (await _context.Zaposlenici.AnyAsync(z => z.Email == zaposlenik.Email && z.ZaposlenikID != zaposlenik.ZaposlenikID))
        {
            ModelState.AddModelError("Email", "Ovaj email već postoji u sistemu.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(zaposlenik);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Zaposlenik je uspješno ažuriran.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaposlenikPostoji(zaposlenik.ZaposlenikID))
                    return NotFound();

                throw;
            }
        }

        await UcitajOdjele(zaposlenik.OdjelID);
        return View(zaposlenik);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var zaposlenik = await _context.Zaposlenici
            .Include(z => z.Odjel)
            .FirstOrDefaultAsync(z => z.ZaposlenikID == id);

        if (zaposlenik == null)
            return NotFound();

        return View(zaposlenik);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var zaposlenik = await _context.Zaposlenici.FindAsync(id);

        if (zaposlenik != null)
        {
            _context.Zaposlenici.Remove(zaposlenik);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Zaposlenik je uspješno obrisan.";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ZaposlenikPostoji(int id)
    {
        return _context.Zaposlenici.Any(e => e.ZaposlenikID == id);
    }

    private async Task UcitajOdjele(int? selectedOdjelId = null)
    {
        var odjeli = await _context.Odjeli
            .OrderBy(o => o.Naziv)
            .ToListAsync();

        ViewBag.OdjelID = new SelectList(odjeli, "OdjelID", "Naziv", selectedOdjelId);
    }
}