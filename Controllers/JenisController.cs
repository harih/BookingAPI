using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


[Route("api/[controller]")]
[ApiController]
public class JenisController : ControllerBase
{
    private readonly BookingContext _context;

    public JenisController()
    {
        _context = new BookingContext();
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
        _context.SeedData();
    }

    [HttpGet("getJenis")]
    public IActionResult GetJenis()
    {
        return Ok(_context.Jenis.ToList());
    }
}