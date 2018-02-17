using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCqrsSample.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNetCoreCqrsSample.Infrastructure.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext dbContext;

        public BookingRepository(BookingDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Booking> Get(long id)
        {
            var events = await dbContext
                .BookingEvents
                .Where(e => e.BookingId == id)
                .OrderBy(e => e.Created)
                .Select(e => new
                {
                    e.Type,
                    e.Body
                })
                .ToListAsync();
            var loadedEvents = events
                .Select(e => JsonConvert.DeserializeObject(e.Body, Type.GetType(e.Type)))
                .ToArray();
            var booking = new Booking();
            booking.LoadEvents(loadedEvents);
            return booking;
        }

        public async Task Save(Booking booking)
        {
            var bookingEntity = await dbContext.Bookings.FindAsync(booking.Id);
            bookingEntity.Status = (int)booking.Status;
            bookingEntity.BookingConfirmedAt = booking.ConfirmedAt;
            bookingEntity.BookingCanceledAt = booking.CanceledAt;

            foreach (var @event in booking.Events)
            {
                dbContext.BookingEvents.Add(Application.Data.BookingEvent.CreateFrom(@event));
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
