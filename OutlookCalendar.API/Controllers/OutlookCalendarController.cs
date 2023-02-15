
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using OutlookCalendar.Domain.Core.Responses;
using OutlookCalendar.OutlookCalendar.Querries;
using OutlookCalendar.Application.OutlookCalendar.Querries;
using OutlookCalendar.Domain.Core.Models;
using System;

namespace Dale.Services.EntFileForATM.API.Controllers
{
    /// <summary>
    /// Version 1 OutlookCalendar Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OutlookCalendarController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OutlookCalendarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the list calendars
        /// </summary>
        /// <returns>The events calendar list</returns>
        /// <response code="200">Returns the item</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseBindingModel<List<OutlookCalendarResponse>>> GetCalendars()
        {
            return await _mediator.Send(new GetCalendarsQuerries());
        }

        /// <summary>
        /// Create Events In Calendar
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateEvent
        ///     {
        ///         "Id : "1",
        //          "Name" = "New Event",
        //          "Description" = "Description New Event",
        //          "Start_time" = "2021-05-15T12:00:00.0000000",
        //          "End_time" = "2021-05-15T14:00:00.0000000",
        ///     }
        /// </remarks>
        /// <param name="outlookEvent">Objeto de la solicitud.</param>
        /// <returns>Id Event created</returns>
        /// <response code="200">Returns the item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseBindingModel<string>> CreateEvent(OutlookEvent outlookEvent)
        {
            return await _mediator.Send(new CreateEventQuerries()
            {
                Id = outlookEvent.Id,
                Name = outlookEvent.Name,
                Description = outlookEvent.Description,
                Start_time = outlookEvent.Start_time,
                End_time = outlookEvent.End_time,
                CalendarID = "1"
            });
        }

        /// <summary>
        /// Update Events In Calendar
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateEvent
        ///     {
        ///         "Id : "1",
        //          "Name" = "New Event",
        //          "Description" = "Description New Event",
        //          "Start_time" = "2021-05-15T12:00:00.0000000",
        //          "End_time" = "2021-05-15T14:00:00.0000000",
        ///     }
        /// </remarks>
        /// <param name="outlookEvent">Objeto de la solicitud.</param>
        /// <returns>Id Event updated</returns>
        /// <response code="200">Returns the item</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseBindingModel<string>> UpdateEvent(OutlookEvent outlookEvent)
        {
            return await _mediator.Send(new UpdateEventQuerries()
            {
                Id = outlookEvent.Id,
                Name = outlookEvent.Name,
                Description = outlookEvent.Description,
                Start_time = outlookEvent.Start_time,
                End_time = outlookEvent.End_time
            });
        }

        /// <summary>
        /// Delete Events In Calendar
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /DeleteEvent
        ///     {
        ///         "Id : "1"
        ///     }
        /// </remarks>
        /// <param name="Id">Id event delete</param>
        /// <returns>Object result</returns>
        /// <response code="200">Returns the item</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseBindingModel<string>> DeleteEvent(string Id)
        {
            return await _mediator.Send(new DeleteEventQuerries()
            {
                Id = Id
            });
        }
    }
}
