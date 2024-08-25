using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent_A_Car_Quattro.Data;
using Rent_A_Car_Quattro.Models;
using Microsoft.AspNetCore.Authorization;

namespace Rent_A_Car_Quattro.Views
{
    public class VozilaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VozilaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vozila
        public async Task<IActionResult> Index(string markaFilter, string modelFilter, int? kmFilter, int? vrataFilter, int? snagaMotora, string gradFilter)
        {
            IQueryable<Vozilo> vozila = _context.Vozilo;

            if (!string.IsNullOrEmpty(markaFilter))
            {
                vozila = vozila.Where(v => v.marka.Contains(markaFilter));
            }
            if (!string.IsNullOrEmpty(modelFilter))
            {
                vozila = vozila.Where(v => v.model.Contains(modelFilter));
            }
            if (kmFilter.HasValue)
            {
                vozila = vozila.Where(v => v.kilometraza <= kmFilter);
            }
            if (vrataFilter.HasValue)
            {
                vozila = vozila.Where(v => v.broj_vrata == vrataFilter);
            }
            if (snagaMotora.HasValue)
            {
                vozila = vozila.Where(v => v.engine_size <= snagaMotora);
            }
            if (!string.IsNullOrEmpty(gradFilter))
            {
                vozila = vozila.Where(v => v.grad.Contains(gradFilter));
            }

            // Store the current filter in ViewBag to persist in the view
            ViewBag.CurrentFilter = markaFilter;

            return View(await vozila.ToListAsync());
        }

        // GET: Vozila/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // GET: Vozila/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozila/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,marka,model,kilometraza,datum_zadnjeg_servisa,cijena_po_satu,cijena_po_kilometru,Transmisija,Gorivo,vozilo_rezervisano,broj_sasije,broj_vrata,engine_size,grad")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                var poslovnica = await _context.Poslovnica.FirstOrDefaultAsync(p => p.grad == vozilo.grad);
                if (poslovnica != null)
                {
                    vozilo.lokacija_fk = poslovnica.ID;
                }
                else
                {
                    ModelState.AddModelError("grad", "No Poslovnica found for the specified city.");
                    return View(vozilo);
                }

                vozilo.datum_sljedeceg_servisa = izracunajIduciServis(vozilo.Transmisija, vozilo.Gorivo, vozilo.datum_zadnjeg_servisa);

                _context.Add(vozilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vozilo);
        }

        private DateTime izracunajIduciServis(VrstaTransmisije transmisija, VrstaGoriva gorivo, DateTime datumZadnjegServisa)
        {
            DateTime iduciServisDate;
            if (transmisija == VrstaTransmisije.Manuelna && gorivo == VrstaGoriva.Benzin)
            {
                iduciServisDate = datumZadnjegServisa.AddMonths(14);
            }
            else if (transmisija == VrstaTransmisije.Automatik && gorivo == VrstaGoriva.Benzin)
            {
                iduciServisDate = datumZadnjegServisa.AddMonths(18);
            }
            else if (gorivo == VrstaGoriva.Elektricni)
            {
                iduciServisDate = datumZadnjegServisa.AddMonths(6);
            }
            else
            {
                iduciServisDate = datumZadnjegServisa.AddMonths(12);
            }
            return iduciServisDate;
        }

        // GET: Vozila/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo.FindAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }
            return View(vozilo);
        }

        // POST: Vozila/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,marka,model,kilometraza,datum_zadnjeg_servisa,cijena_po_satu,cijena_po_kilometru,Transmisija,Gorivo,vozilo_rezervisano,broj_sasije")] Vozilo vozilo)
        {
            if (id != vozilo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoziloExists(vozilo.ID))
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
            return View(vozilo);
        }

        // GET: Vozila/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // POST: Vozila/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vozilo = await _context.Vozilo.FindAsync(id);
            if (vozilo != null)
            {
                _context.Vozilo.Remove(vozilo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoziloExists(int id)
        {
            return _context.Vozilo.Any(e => e.ID == id);
        }
    }
}
