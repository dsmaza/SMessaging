namespace AspNetCoreCqrsSample.Application.Contracts
{
    public class ConfirmBooking
    {
        public long BookingId { get; set; }

        // in real life this could be payment ID
        public string ConfirmationCode { get; set; }
    }
}
