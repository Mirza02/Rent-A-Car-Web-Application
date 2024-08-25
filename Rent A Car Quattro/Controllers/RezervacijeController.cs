using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent_A_Car_Quattro.Data;
using Rent_A_Car_Quattro.Models;

namespace Rent_A_Car_Quattro.Controllers
{
    [Authorize(Roles = "Korisnik")]
    public class RezervacijeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijeController(ApplicationDbContext context)
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




        // GET: Rezervacije/Create
        public IActionResult Create(int? voziloId)
        {
            if (voziloId == null)
            {
                return NotFound();
            }

            // Get the logged-in user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the vehicle from the database
            var vozilo = _context.Vozilo.FirstOrDefault(v => v.ID == voziloId);
            if (vozilo == null)
            {
                return NotFound();
            }

            // Set default value for vrijeme_rezervacije (e.g., 1 hour)
            int defaultVrijemeRezervacije = 1; // 1 hour

            // Create a new reservation object and prefill the fields
            var rezervacija = new Rezervacija
            {
                vozilo_fk = vozilo.ID,
                korisnik_fk = userId,
                lokacija_fk = vozilo.lokacija_fk,
                vrijeme_rezervacije = defaultVrijemeRezervacije,
                cijena_rezervacija = defaultVrijemeRezervacije * vozilo.cijena_po_satu * 0.01,
                kilometraza_ili_vrijeme = true,
                pocetak_rezervacije = DateTime.Now, 
                kraj_rezervacije = DateTime.Now.AddHours(1)
            };

            return View(rezervacija);
        }

        // POST: Rezervacije/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,pocetak_rezervacije,kraj_rezervacije,vrijeme_rezervacije,kilometraza_rezervacije,kilometraza_ili_vrijeme,cijena_rezervacija,lokacija_fk,korisnik_fk,placanje_fk,vozilo_fk")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the vehicle to get its pricing details
                var vozilo = await _context.Vozilo.FirstOrDefaultAsync(v => v.ID == rezervacija.vozilo_fk);
                if (vozilo == null)
                {
                    return NotFound();
                }

                // Calculate the number of hours between the start and end date
                var totalHours = (rezervacija.kraj_rezervacije - rezervacija.pocetak_rezervacije).TotalHours;

                // Calculate the price based on the total hours
                rezervacija.cijena_rezervacija = vozilo.cijena_po_satu * totalHours;

                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }



        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.ID == id);
        }
    }
}
