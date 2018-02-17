using System;

namespace AspNetCoreCqrsSample.Domain
{
    public class Booking : Aggregate
    {
        public long Id { get; private set; }

        public BookingStatus Status { get; private set; }

        public DateTime? ConfirmedAt { get; private set; }

        public DateTime? CanceledAt { get; private set; }

        private void BookingCreated(BookingCreated bookingCreated)
        {
            Id = bookingCreated.BookingId;
            Status = BookingStatus.Created;
        }

        private void BookingConfirmed(BookingConfirmed bookingConfirmed)
        {
            Status = BookingStatus.Confirmed;
            ConfirmedAt = bookingConfirmed.ConfirmedAt;
        }

        private void BookingCanceled(BookingCanceled bookingCanceled)
        {
            Status = BookingStatus.Canceled;
            CanceledAt = bookingCanceled.CanceledAt;
        }

        public void Confirm(string confirmationCode)
        {
            if (string.IsNullOrWhiteSpace(confirmationCode))
            {
                throw new InvalidConfirmationCodeException();
            }
            if (Status != BookingStatus.Created)
            {
                throw new BookingNotCreatedException();
            }
            Apply(new BookingConfirmed
            {
                BookingId = Id,
                ConfirmedAt = DateTime.UtcNow,
                ConfirmationCode = confirmationCode
            });
        }

        public void Cancel()
        {
            if (Status != BookingStatus.Confirmed)
            {
                throw new BookingNotConfirmedException();
            }
            Apply(new BookingCanceled
            {
                BookingId = Id,
                CanceledAt = DateTime.UtcNow
            });
        }
    }
}