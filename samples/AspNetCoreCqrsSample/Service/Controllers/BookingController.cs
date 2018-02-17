using System;
using System.Threading.Tasks;
using AspNetCoreCqrsSample.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SMessaging.Abstractions;

namespace AspNetCoreCqrsSample.Service.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger logger;
        private readonly IMessaging messaging;

        public BookingController(ILogger<BookingController> logger, IMessaging messaging)
        {
            this.logger = logger;
            this.messaging = messaging;
        }

        [HttpGet("api/booking/{bookingId}")]
        public async Task<IActionResult> GetBookingDetails([FromRoute]GetBookingDetails query)
        {
            try
            {
                var result = await messaging.Send(query);
                return StatusCode(result.Code, result.Value);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(GetBookingDetails)} error");
                return StatusCode(500);
            }
        }

        [HttpPost("api/booking")]
        public async Task<IActionResult> CreateBooking([FromBody]CreateBooking cmd)
        {
            try
            {
                var result = await messaging.Send(cmd);
                return StatusCode(result.Code, result.Value);
            }
            catch (Domain.BookingException e)
            {
                return StatusCode(e.Status, new { e.Message });
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(CreateBooking)} error");
                return StatusCode(500);
            }
        }

        [HttpPut("api/booking/{bookingId}")]
        public async Task<IActionResult> ConfirmBooking(long bookingId, [FromBody]ConfirmBooking cmd)
        {
            try
            {
                cmd.BookingId = bookingId;
                await messaging.Send(cmd);
                return StatusCode(200);
            }
            catch (Domain.BookingException e)
            {
                return StatusCode(e.Status, new { e.Message });
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(ConfirmBooking)} error");
                return StatusCode(500);
            }
        }

        [HttpDelete("api/booking/{bookingId}")]
        public async Task<IActionResult> CancelBooking([FromRoute]CancelBooking cmd)
        {
            try
            {
                await messaging.Send(cmd);
                return StatusCode(204);
            }
            catch (Domain.BookingException e)
            {
                return StatusCode(e.Status, new { e.Message });
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(CancelBooking)} error");
                return StatusCode(500);
            }
        }
    }
}
