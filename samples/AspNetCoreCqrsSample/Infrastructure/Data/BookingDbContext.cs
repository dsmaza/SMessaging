using AspNetCoreCqrsSample.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCqrsSample.Infrastructure.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<BookingEvent> BookingEvents { get; set; }
    }
}
