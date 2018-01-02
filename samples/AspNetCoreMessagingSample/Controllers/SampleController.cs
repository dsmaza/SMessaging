using System;
using System.Threading.Tasks;
using AspNetCoreMessagingSample.Models;
using Microsoft.AspNetCore.Mvc;
using SMessaging.Abstractions;

namespace AspNetCoreMessagingSample.Controllers
{
    public class SampleController : Controller
    {
        private readonly IMessaging messaging;

        public SampleController(IMessaging messaging)
        {
            this.messaging = messaging;
        }

        [HttpPost("api/sample")]
        public async Task<IActionResult> Post([FromBody]SampleMessage message)
        {
            try
            {
                var result = await messaging.Send(message);
                return StatusCode(result.Code, result.Value);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
