using System;

namespace AspNetCoreCqrsSample.Domain
{
    public class BookingConfirmed
    {
        public long BookingId { get; set; }

        public DateTime ConfirmedAt { get; set; }

        public string ConfirmationCode { get; set; }
    }
}
