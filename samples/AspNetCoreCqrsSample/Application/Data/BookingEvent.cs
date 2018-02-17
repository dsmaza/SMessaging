using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreCqrsSample.Application.Data
{
    public class BookingEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public long BookingId { get; set; }

        public string Type { get; set; }

        public string Body { get; set; }

        public Booking Booking { get; set; }

        public static BookingEvent CreateFrom(object @event)
        {
            return new BookingEvent
            {
                Created = DateTime.Now,
                BookingId = (long)@event.GetType().GetProperty("BookingId").GetValue(@event),
                Type = @event.GetType().AssemblyQualifiedName,
                Body = Newtonsoft.Json.JsonConvert.SerializeObject(@event)
            };
        }
    }
}
