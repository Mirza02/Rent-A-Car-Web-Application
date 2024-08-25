using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent_A_Car_Quattro.Data;
using Rent_A_Car_Quattro.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rent_A_Car_Quattro.Controllers
{
    public class LokacijaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LokacijaController> _logger;

        public LokacijaController(ApplicationDbContext context, ILogger<LokacijaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Prikaz()
        {
            var locations = await _context.Poslovnica.ToListAsync();
            return View(locations);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
