using Doctorly.EventManager.Api.Application.Dtos;
using Doctorly.EventManager.Api.Application.UseCases;
using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Doctorly.EventManager.Api.Infastructure.Services;
using Doctorly.EventManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Doctorly.EventManager.Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly GetEventsHandler _getEvents;
        private readonly GetEventByIdHandler _getEventById;
        private readonly CreateEventHandler _createEvent;
        private readonly UpdateEventHandler _updateEvent;
        private readonly DeleteEventHandler _deleteEvent;

        public EventController(
            GetEventsHandler getEvents,
            GetEventByIdHandler getEventById,
            CreateEventHandler createEvent,
            UpdateEventHandler updateEvent,
            DeleteEventHandler deleteEvent)
        {
            _getEvents = getEvents;
            _getEventById = getEventById;
            _createEvent = createEvent;
            _updateEvent = updateEvent;
            _deleteEvent = deleteEvent;
        }

        [HttpGet]
        public IActionResult GetEvents([FromQuery] SearchEventDto dto)
        {
            var (items, totalCount) = _getEvents.Handle(dto);
            return Ok(new { Items = items, TotalCount = totalCount });
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var dto = _getEventById.Handle(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] CreateEventDto dto)
        {
            var result = _createEvent.Handle(dto);
            return CreatedAtAction(nameof(GetEventById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] UpdateEventDto dto)
        {
            var success = _updateEvent.Handle(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var success = _deleteEvent.Handle(id);
            return success ? NoContent() : NotFound();
        }
    }

}