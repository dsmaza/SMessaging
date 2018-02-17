using System.Threading.Tasks;
using AspNetCoreCqrsSample.Application.Contracts;
using AspNetCoreCqrsSample.Domain;
using SMessaging.Abstractions;

namespace AspNetCoreCqrsSample.Application.Commands
{
    public class BookingHandlers : IHandleMessage<ConfirmBooking>, IHandleMessage<CancelBooking>
    {
        private readonly IBookingRepository repository;

        public BookingHandlers(IBookingRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MessageResult> Handle(ConfirmBooking message)
        {
            var booking = await repository.Get(message.BookingId);
            booking.Confirm(message.ConfirmationCode);
            await repository.Save(booking);
            return MessageResult.Null;
        }

        public async Task<MessageResult> Handle(CancelBooking message)
        {
            var booking = await repository.Get(message.BookingId);
            booking.Cancel();
            await repository.Save(booking);
            return MessageResult.Null;
        }
    }
}
