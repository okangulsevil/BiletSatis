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

        // public async Task<IActionResult> Index(int? flightId)
        // {
        //     // Dropdown için Flight verilerini hazırlayın
        //     ViewBag.Flights = new SelectList(
        //         await _context.Flights.Select(f => new { f.Id, f.FlightNumber }).ToListAsync(),
        //         "Id",
        //         "FlightNumber",
        //         flightId); // Seçili değeri hatırlamak için flightId eklenir

        //     // Tüm biletleri yükle
        //     var tickets = _context.Tickets
        //         .Include(t => t.Customer)
        //         .Include(t => t.Flight)
        //         .AsQueryable();

        //     // Eğer bir uçuş seçildiyse, filtre uygula
        //     if (flightId.HasValue && flightId > 0)
        //     {
        //         tickets = tickets.Where(t => t.FlightId == flightId.Value);
        //     }

        //     return View(await tickets.ToListAsync());
        // }

        [HttpGet]
        public async Task<IActionResult> Index(int? flightId)
        {
            if (flightId == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.Flight)
                .Where(t => t.FlightId == flightId && t.CustomerId == null) // Boş biletler
                .ToListAsync();

            ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName");
            ViewBag.FlightId = flightId; // Uçuş bilgisi için gönderiliyor

            return View(tickets);
        }

        // [HttpGet]
        // public async Task<IActionResult> Index(int? flightId)
        // {
        //     var tickets = await _context.Tickets
        //          .Include(t => t.Customer) // Müşteri bilgilerini dahil et
        //          .Include(t => t.Flight)  // Uçuş bilgilerini dahil et
        //          .Where(t => t.CustomerId != null) // Müşteriye atanmış biletler
        //          .ToListAsync();

        //     return View(tickets);
        // }

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
        public async Task<IActionResult> Edit(int? id)
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
            // BookingDate kontrolü
            Console.WriteLine(ticket.BookingDate); // Bu satır ile veriyi kontrol edin.
            // Dropdown için ViewBag değerlerini doldur ve seçili değeri belirt
            ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName", ticket.CustomerId);
            ViewBag.Flights = new SelectList(await _context.Flights.ToListAsync(), "Id", "FlightNumber", ticket.FlightId);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tickets.Any(o => o.Id == ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName", ticket.CustomerId);
            ViewBag.Flights = new SelectList(await _context.Flights.ToListAsync(), "Id", "FlightNumber", ticket.FlightId);

            return View(ticket);
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

            // Bileti boşa çıkar
            ticket.CustomerId = null;
            ticket.BookingDate = null;

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("ReservedTickets");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(int flightId, Dictionary<int, int> CustomerIds)
        {
            foreach (var item in CustomerIds)
            {
                var ticketId = item.Key;
                var customerId = item.Value;

                if (customerId > 0)
                {
                    var ticket = await _context.Tickets.FindAsync(ticketId);
                    if (ticket != null && ticket.CustomerId == null)
                    {
                        ticket.CustomerId = customerId;
                        ticket.BookingDate = DateTime.Now; // Rezervasyon tarihi olarak bugünün tarihi atanıyor

                        _context.Update(ticket);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendirme
        }

        [HttpGet]
        public async Task<IActionResult> ReservedTickets()
        {
            // Rezervasyon yapılmış biletleri getir
            var reservedTickets = await _context.Tickets
                .Include(t => t.Customer) // Müşteri bilgilerini dahil et
                .Include(t => t.Flight)  // Uçuş bilgilerini dahil et
                .Where(t => t.CustomerId != null) // Sadece müşteri atanmış biletler
                .ToListAsync();

            return View(reservedTickets);
        }

    }
}