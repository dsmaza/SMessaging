using System;

namespace AspNetCoreCqrsSample.Domain
{
    public class BookingCreated
    {
        public long BookingId { get; set; }

        public DateTime CreatedAt { get; set; } 
    }
}
