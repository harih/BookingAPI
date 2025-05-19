using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly BookingContext _context;

    public BookingController()
    {
        _context = new BookingContext();
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
        _context.SeedData();
    }

    [HttpGet("getBookingList")]
    public IActionResult GetBookingList() => Ok(_context.Bookings.ToList());


    // [HttpGet("getBookingFilter/{year}/{month}/{dept?}")]
    // public IActionResult GetBookingFilter(int year, int month, string? dept = null)
    // {
    //     var filteredBookings = _context.Bookings
    //         .Where(b => b.Date.Year == year && b.Date.Month == month);

    //     if (!string.IsNullOrEmpty(dept))
    //     {
    //         filteredBookings = filteredBookings.Where(b => b.Department == dept);
    //     }

    //     return Ok(filteredBookings.ToList());
    // }

    [HttpGet("getBookingFilter/{year}/{month?}/{dept?}")]
    public IActionResult GetBookingFilter(int year, int? month = null, string? dept = null)
    {
        var filteredBookings = _context.Bookings.Where(b => b.Date.Year == year);

        if (month.HasValue)
        {
            filteredBookings = filteredBookings.Where(b => b.Date.Month == month);
        }

        if (!string.IsNullOrEmpty(dept))
        {
            filteredBookings = filteredBookings.Where(b => b.Department == dept);
        }

        return Ok(filteredBookings.ToList());
    }

    [HttpGet("getBookingSummary/{month}")]
    public IActionResult GetBookingSummary(int month)
    {
        var summary = _context.Bookings
            .Where(b => b.Date.Month == month)
            .GroupBy(b => new { b.Date.Month, b.Department })
            .Select(g => new { Month = g.Key.Month, Department = g.Key.Department, Count = g.Count() })
            .ToList();

        return Ok(summary);
    }

    [HttpGet("getBookingByDate")]
    public IActionResult GetBookingByDate()
    {
        var summary = _context.Bookings
            .GroupBy(b => new { DateOnly = DateOnly.FromDateTime(b.Date), b.Room, b.Department }) // Converts DateTime to DateOnly
            .Select(g => new { Date = g.Key.DateOnly, Room = g.Key.Room, Department = g.Key.Department, Count = g.Count() })
            .OrderBy(g => g.Date)
            .ToList();

        return Ok(summary);
    }

}