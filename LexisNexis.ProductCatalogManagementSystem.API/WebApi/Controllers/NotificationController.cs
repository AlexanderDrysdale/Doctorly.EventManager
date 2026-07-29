using Doctorly.EventManager.Api.Application.UseCases;
using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Doctorly.EventManager.Api.Infastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Doctorly.EventManager.Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly SendIcalHandler _sendIcalHandler;

        public NotificationController(SendIcalHandler sendIcalHandler)
        {
            _sendIcalHandler = sendIcalHandler;
        }

        [HttpPost("events/{id}/ical")]
        public IActionResult SendEventIcal(int id)
        {
            _sendIcalHandler.Handle(id);
            return Ok(new { Message = "iCal invitations sent." });
        }
    }
}
