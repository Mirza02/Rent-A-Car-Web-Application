using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent_A_Car_Quattro.Data;
using Rent_A_Car_Quattro.Models;

namespace Rent_A_Car_Quattro.Controllers
{
    [Authorize(Roles = "Korisnik, Zaposlenik")]
    public class RezervacijaCheckController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijaCheckController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RezervacijaCheck
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rezervacija.ToListAsync());
        }

        // GET: RezervacijaCheck/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: RezervacijaCheck/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RezervacijaCheck/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,pocetak_rezervacije,kraj_rezervacije,vrijeme_rezervacije,kilometraza_rezervacije,kilometraza_ili_vrijeme,cijena_rezervacija,lokacija_fk,korisnik_fk,placanje_fk,vozilo_fk")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }

        // GET: RezervacijaCheck/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            return View(rezervacija);
        }

        // POST: RezervacijaCheck/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,pocetak_rezervacije,kraj_rezervacije,vrijeme_rezervacije,kilometraza_rezervacije,kilometraza_ili_vrijeme,cijena_rezervacija,lokacija_fk,korisnik_fk,placanje_fk,vozilo_fk")] Rezervacija rezervacija)
        {
            if (id != rezervacija.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }

        // GET: RezervacijaCheck/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: RezervacijaCheck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija != null)
            {
                _context.Rezervacija.Remove(rezervacija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.ID == id);
        }
    }
}
