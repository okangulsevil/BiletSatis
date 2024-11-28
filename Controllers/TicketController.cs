using BiletSatis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Controllers
{
    public class TicketController(DataContext context) : Controller
    {
        private readonly DataContext _context = context;

        // public async Task<IActionResult> Index()
        // {
        //     var tickets = await _context.Tickets
        //         .Include(t => t.Customer)
        //         .Include(t => t.Flight)
        //         .ToListAsync();
        //     return View(tickets);
        // }

        public async Task<IActionResult> Index(int? flightId)
        {
            // Dropdown için Flight verilerini hazırlayın
            ViewBag.Flights = new SelectList(
                await _context.Flights.Select(f => new { f.Id, f.FlightNumber }).ToListAsync(),
                "Id",
                "FlightNumber",
                flightId); // Seçili değeri hatırlamak için flightId eklenir

            // Tüm biletleri yükle
            var tickets = _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Flight)
                .AsQueryable();

            // Eğer bir uçuş seçildiyse, filtre uygula
            if (flightId.HasValue && flightId > 0)
            {
                tickets = tickets.Where(t => t.FlightId == flightId.Value);
            }

            return View(await tickets.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "Id", "FullName");
            ViewBag.Flights = new SelectList(_context.Flights, "Id", "FlightNumber");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}