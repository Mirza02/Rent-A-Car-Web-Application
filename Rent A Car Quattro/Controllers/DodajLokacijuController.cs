using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent_A_Car_Quattro.Data;
using Rent_A_Car_Quattro.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Rent_A_Car_Quattro.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class DodajLokacijuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DodajLokacijuController> _logger;
        public DodajLokacijuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacije/Create
        public IActionResult Insert()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Poslovnica> vozila = _context.Poslovnica;
            return View(await vozila.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([Bind("latitude, longitude, grad, opis")] Poslovnica poslovnica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poslovnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poslovnica);
        }


        // GET: Rezervacije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacija = await _context.Poslovnica
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return View(lokacija);
        }

        // POST: Rezervacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lokacija = await _context.Poslovnica.FindAsync(id);
            if (lokacija != null)
            {
                _context.Poslovnica.Remove(lokacija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
