using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BiletSatis.Models;
using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return View(new List<SearchResult>());
        }

        // Customer sonuçlarını getir
        var customers = await _context.Customers
            .Where(c => EF.Functions.Like(c.FirstName, $"%{query}%") ||
                        EF.Functions.Like(c.LastName, $"%{query}%") ||
                        EF.Functions.Like(c.Email, $"%{query}%") ||
                        EF.Functions.Like(c.Phone, $"%{query}%"))
            .Select(c => new SearchResult
            {
                Type = "Customer",
                Title = c.FirstName,
                Description = $"Email: {c.Email}, Phone: {c.Phone}",
                Link = $"/Customer/Edit/{c.Id}"
            }).ToListAsync();

        // Flight sonuçlarını getir
        var flights = await _context.Flights
            .Where(f => EF.Functions.Like(f.FlightNumber, $"%{query}%") ||
                        EF.Functions.Like(f.Origin, $"%{query}%") ||
                        EF.Functions.Like(f.Destination, $"%{query}%"))
            .Select(f => new SearchResult
            {
                Type = "Flight",
                Title = f.FlightNumber,
                Description = $"From: {f.Origin} to {f.Destination}",
                Link = $"/Flight/Edit/{f.Id}"
            }).ToListAsync();

        // Ticket sonuçlarını getir
        var tickets = await _context.Tickets
            .Include(t => t.Customer)
            .Include(t => t.Flight)
            .Where(t => EF.Functions.Like(t.SeatNumber, $"%{query}%") ||
                        EF.Functions.Like(t.Customer.FirstName, $"%{query}%") ||
                        EF.Functions.Like(t.Customer.LastName, $"%{query}%") ||
                        EF.Functions.Like(t.Flight.FlightNumber, $"%{query}%"))
            .Select(t => new SearchResult
            {
                Type = "Ticket",
                Title = $"Ticket for {t.Customer.FirstName}",
                Description = $"Flight: {t.Flight.FlightNumber}, Seat: {t.SeatNumber}",
                Link = $"/Ticket/Edit/{t.Id}"
            }).ToListAsync();

        // Tüm sonuçları birleştir ve sıralayın
        var results = customers
            .Concat(flights)
            .Concat(tickets)
            .OrderBy(r => r.Type)
            .ToList();

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