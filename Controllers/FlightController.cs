using BiletSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Controllers
{
    public class FlightController(DataContext context) : Controller
    {
        private readonly DataContext _context = context;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Flights.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {
                // Logo kaydetme işlemi
                if (Logo != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Logo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Logo.CopyToAsync(fileStream);
                    }

                    flight.AirlineLogoPath = "/images/" + uniqueFileName;
                }

                _context.Flights.Add(flight);
                await _context.SaveChangesAsync();

                // Kapasite kadar bilet oluştur
                for (int i = 1; i <= flight.Capacity; i++)
                {
                    _context.Tickets.Add(new Ticket
                    {
                        FlightId = flight.Id,
                        SeatNumber = $"Seat-{i}" // Koltuk numarası
                    });
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Flight = await _context.Flights.FindAsync(id);

            if (Flight == null)
            {
                return NotFound();
            }
            return View(Flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Flight flight)
        {

            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Flights.Any(o => o.Id == flight.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Flight = await _context.Flights.FindAsync(id);
            if (Flight == null)
            {
                return NotFound();
            }
            return View(Flight);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var Flight = await _context.Flights.FindAsync(id);
            if (Flight == null)
            {
                return NotFound();
            }
            _context.Flights.Remove(Flight);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

