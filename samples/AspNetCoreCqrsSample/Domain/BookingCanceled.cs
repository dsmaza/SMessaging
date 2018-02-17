using System;

namespace AspNetCoreCqrsSample.Domain
{
    public class BookingCanceled
    {
        public long BookingId { get; set; }

        public DateTime CanceledAt { get; set; }
    }
}
