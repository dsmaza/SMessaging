namespace AspNetCoreCqrsSample.Application.Contracts
{
    public class CreateBooking
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }
        
        public string ServiceId { get; set; }

        public string Price { get; set; }

        public string Quantity { get; set; }

        public string StartAt { get; set; }

        public string BookingAt { get; set; }

        public string Comment { get; set; }
    }
}
