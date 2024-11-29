using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BiletSatis.Models;
using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Controllers;

public class HomeController(ILogger<HomeController> logger, DataContext context) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly DataContext _context = context;

    // public async Task<IActionResult> Search(string query)
    // {
    //     if (string.IsNullOrEmpty(query))
    //     {
    //         return View(new List<SearchResult>());
    //     }

    //     // Customer sonuçlarını getir
    //     var customers = await _context.Customers
    //         .Where(c => EF.Functions.Like(c.FirstName, $"%{query}%") ||
    //                     EF.Functions.Like(c.LastName, $"%{query}%") ||
    //                     EF.Functions.Like(c.Email, $"%{query}%") ||
    //                     EF.Functions.Like(c.Phone, $"%{query}%"))
    //         .Select(c => new SearchResult
    //         {
    //             Type = "Customer",
    //             Title = c.FirstName,
    //             Description = $"Email: {c.Email}, Phone: {c.Phone}",
    //             Link = $"/Customer/Edit/{c.Id}"
    //         }).ToListAsync();

    //     // Flight sonuçlarını getir
    //     var flights = await _context.Flights
    //         .Where(f => EF.Functions.Like(f.FlightNumber, $"%{query}%") ||
    //                     EF.Functions.Like(f.Origin, $"%{query}%") ||
    //                     EF.Functions.Like(f.Destination, $"%{query}%"))
    //         .Select(f => new SearchResult
    //         {
    //             Type = "Flight",
    //             Title = f.FlightNumber,
    //             Description = $"From: {f.Origin} to {f.Destination}",
    //             Link = $"/Flight/Edit/{f.Id}"
    //         }).ToListAsync();

    //     // Ticket sonuçlarını getir
    //     var tickets = await _context.Tickets
    //         .Include(t => t.Customer)
    //         .Include(t => t.Flight)
    //         .Where(t => EF.Functions.Like(t.SeatNumber, $"%{query}%") ||
    //                     EF.Functions.Like(t.Customer.FirstName, $"%{query}%") ||
    //                     EF.Functions.Like(t.Customer.LastName, $"%{query}%") ||
    //                     EF.Functions.Like(t.Flight.FlightNumber, $"%{query}%"))
    //         .Select(t => new SearchResult
    //         {
    //             Type = "Ticket",
    //             Title = $"Ticket for {t.Customer.FirstName}",
    //             Description = $"Flight: {t.Flight.FlightNumber}, Seat: {t.SeatNumber}",
    //             Link = $"/Ticket/Edit/{t.Id}"
    //         }).ToListAsync();

    //     // Tüm sonuçları birleştir ve sıralayın
    //     var results = customers
    //         .Concat(flights)
    //         .Concat(tickets)
    //         .OrderBy(r => r.Type)
    //         .ToList();

    //     return View(results);
    // }

    [HttpGet]
    public async Task<IActionResult> Search(string origin, string destination, DateTime? date)
    {
        // Arama işlemi yapılıyor
        var flights = _context.Flights.Include(f => f.Tickets).AsQueryable();

        if (!string.IsNullOrEmpty(origin))
        {
            flights = flights.Where(f => f.Origin.Contains(origin));
        }

        if (!string.IsNullOrEmpty(destination))
        {
            flights = flights.Where(f => f.Destination.Contains(destination));
        }

        if (date.HasValue)
        {
            flights = flights.Where(f => f.DepartureTime.Date == date.Value.Date);
        }

        var results = await flights.Select(f => new SearchResult
        {
            Id = f.Id,
            Title = f.FlightNumber,
            Origin = f.Origin,
            Destination = f.Destination,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            ExtraInfo = $"{f.Tickets.Count(t => t.CustomerId == null)} boş koltuk var.",
            Link = Url.Action("Index", "Ticket", new { flightId = f.Id }),
            Airline = f.Airline, // Firma adı
            AirlineLogoPath = f.AirlineLogoPath // Firma logosu yolu
        }).ToListAsync();

        return View(results);
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}