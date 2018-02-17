using System;
using System.Threading.Tasks;
using AspNetCoreCqrsSample.Application.Contracts;
using AspNetCoreCqrsSample.Application.Data;
using AspNetCoreCqrsSample.Application.Interfaces;
using SMessaging.Abstractions;

namespace AspNetCoreCqrsSample.Application.Commands
{
    public class CreateBookingHandler : IHandleMessage<CreateBooking>
    {
        private static readonly System.Threading.SemaphoreSlim semaphoreSlim = new System.Threading.SemaphoreSlim(1, 1);
        private readonly IUnitOfWork unitOfWork;

        public CreateBookingHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<MessageResult> Handle(CreateBooking message)
        {
            await semaphoreSlim.WaitAsync();
            try 
            {
                // TODO validations etc.
                var startAt = DateTime.Parse(message.StartAt);
                var serviceId = int.Parse(message.ServiceId);

                if (await unitOfWork.Exists<Booking>(b => b.StartAt == startAt && b.ServiceId == serviceId))
                {
                    throw new Domain.BookingAlreadyExists();
                }

                var createdUtc = DateTime.UtcNow;
                var booking = new Booking
                {
                    Email = message.Email,
                    Name = message.Name,
                    Surname = message.Surname,
                    Phone = message.Phone,
                    BookingAt = DateTime.Parse(message.BookingAt),
                    StartAt = startAt,
                    ServiceId = serviceId,
                    Quantity = int.Parse(message.Quantity),
                    Price = decimal.Parse(message.Price),
                    Comment = message.Comment,
                    BookingCreatedAt = createdUtc,
                    Status = (int)Domain.BookingStatus.Created
                };
                await unitOfWork.Add(booking);
                await unitOfWork.Save();

                var bookingCreated = new Domain.BookingCreated
                {
                    CreatedAt = createdUtc,
                    BookingId = booking.Id,
                };

                await unitOfWork.Add(BookingEvent.CreateFrom(bookingCreated));
                await unitOfWork.Save();
                return new MessageResult(201, new { booking.Id });
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
