using System;
using System.Threading.Tasks;
using AspNetCoreCqrsSample.Application.Contracts;
using AspNetCoreCqrsSample.Application.Data;
using AspNetCoreCqrsSample.Application.Interfaces;
using SMessaging.Abstractions;

namespace AspNetCoreCqrsSample.Application.Queries
{
    public class GetOrderDetailsHandler : IHandleMessage<GetBookingDetails>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetOrderDetailsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<MessageResult> Handle(GetBookingDetails message)
        {
            var booking = await unitOfWork.Find<Booking>(message.BookingId);
            return new MessageResult(200, new
            {
                booking.Id,
                booking.Name,
                booking.Surname,
                booking.Email,
                booking.Phone,
                ServiceName = "Hello World Service", // get it from database
                booking.StartAt,
                booking.BookingAt,
                Status = Enum.GetName(typeof(Domain.BookingStatus), booking.Status)
            });
        }
    }
}
