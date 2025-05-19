using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly BookingContext _context;

    public RoomController()
    {
        _context = new BookingContext();
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
        _context.SeedData();
    }

    [HttpGet("getRooms")]
    public IActionResult GetRooms()
    {
        return Ok(_context.Rooms.ToList());
    }
}