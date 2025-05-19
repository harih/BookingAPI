using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;



public class BookingContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Jenis> Jenis { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=:memory:");

    public void SeedData()
    {
        // Seed Bookings
        var bookingData = File.ReadAllText("Data/bookings.json");
        var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingData);
        Bookings.AddRange(bookings);

        // Seed Rooms
        var roomData = File.ReadAllText("Data/rooms.json");
        var rooms = JsonConvert.DeserializeObject<List<Room>>(roomData);
        Rooms.AddRange(rooms);

        // Seed Jenis
        var jenisData = File.ReadAllText("Data/jenis.json");
        var jenis = JsonConvert.DeserializeObject<List<Jenis>>(jenisData);
        Jenis.AddRange(jenis);

        SaveChanges();
    }
}
