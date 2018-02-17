using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreCqrsSample.Application.Data
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // in real life this could be mapped to database user
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        // in real life this could be reference to any service in database
        public int ServiceId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime BookingAt { get; set; }

        public DateTime BookingCreatedAt { get; set; }

        public DateTime? BookingConfirmedAt { get; set; }

        public DateTime? BookingCanceledAt { get; set; }

        public string Comment { get; set; }

        public int Status { get; set; }

        public virtual ICollection<BookingEvent> BookingEvents { get; set; }
    }
}
