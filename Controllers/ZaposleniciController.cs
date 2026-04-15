using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Data;
using SistemZaZaposlenike.Models;
using System.Threading.Tasks;
using SistemZaZaposlenike.Services;
using SistemZaZaposlenike.ViewModels;

namespace SistemZaZaposlenike.Controllers;

public class ZaposleniciController : Controller
{
    private readonly AppDbContext _context;
    private readonly IValutaService _valutaService;

    public ZaposleniciController(AppDbContext context, IValutaService valutaService)
    {
        _context = context;
        _valutaService = valutaService;
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

    public async Task<IActionResult> Details(int? id, string? valuta = null)
    {
        if (id == null)
            return NotFound();

        var zaposlenik = await _context.Zaposlenici
            .Include(z => z.Odjel)
            .FirstOrDefaultAsync(z => z.ZaposlenikID == id);

        if (zaposlenik == null)
            return NotFound();

        ViewBag.OdabranaValuta = valuta;
        ViewBag.KonvertovaniIznos = null;

        if (!string.IsNullOrWhiteSpace(valuta))
        {
            var konvertovaniIznos = await _valutaService.KonvertujIzBamAsync(zaposlenik.PlataMonthly, valuta);
            ViewBag.KonvertovaniIznos = konvertovaniIznos;

            if (konvertovaniIznos == null)
            {
                ViewBag.ValutaError = "Konverzija trenutno nije dostupna. Pokušajte ponovo kasnije.";
            }
        }

        return View(zaposlenik);
    }

    //GET
    public async Task<IActionResult> Create()
    {
        await UcitajOdjele();
        return View(new ZaposlenikViewModel
        {
            DatumZaposlenja = DateTime.Today
        });
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ZaposlenikViewModel model)
    {
        if (await _context.Zaposlenici.AnyAsync(z => z.Email == model.Email))
        {
            ModelState.AddModelError("Email", "Ovaj email već postoji u sistemu.");
        }

        if (ModelState.IsValid)
        {
            var zaposlenik = new Zaposlenik
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                PlataMonthly = model.PlataMonthly,
                DatumZaposlenja = model.DatumZaposlenja,
                OdjelID = model.OdjelID
            };

            _context.Zaposlenici.Add(zaposlenik);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Zaposlenik je uspješno dodan.";
            return RedirectToAction(nameof(Index));
        }

        await UcitajOdjele(model.OdjelID);
        return View(model);
    }

    //GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var zaposlenik = await _context.Zaposlenici.FindAsync(id);

        if (zaposlenik == null)
            return NotFound();

        var model = new ZaposlenikViewModel
        {
            ZaposlenikID = zaposlenik.ZaposlenikID,
            Ime = zaposlenik.Ime,
            Prezime = zaposlenik.Prezime,
            Email = zaposlenik.Email,
            PlataMonthly = zaposlenik.PlataMonthly,
            DatumZaposlenja = zaposlenik.DatumZaposlenja,
            OdjelID = zaposlenik.OdjelID
        };

        await UcitajOdjele(model.OdjelID);
        return View(model);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ZaposlenikViewModel model)
    {
        if (id != model.ZaposlenikID)
            return NotFound();

        if (await _context.Zaposlenici.AnyAsync(z => z.Email == model.Email && z.ZaposlenikID != model.ZaposlenikID))
        {
            ModelState.AddModelError("Email", "Ovaj email već postoji u sistemu.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                var zaposlenik = await _context.Zaposlenici.FindAsync(id);

                if (zaposlenik == null)
                    return NotFound();

                zaposlenik.Ime = model.Ime;
                zaposlenik.Prezime = model.Prezime;
                zaposlenik.Email = model.Email;
                zaposlenik.PlataMonthly = model.PlataMonthly;
                zaposlenik.DatumZaposlenja = model.DatumZaposlenja;
                zaposlenik.OdjelID = model.OdjelID;

                _context.Update(zaposlenik);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Zaposlenik je uspješno ažuriran.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaposlenikPostoji(model.ZaposlenikID))
                    return NotFound();

                throw;
            }
        }

        await UcitajOdjele(model.OdjelID);
        return View(model);
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