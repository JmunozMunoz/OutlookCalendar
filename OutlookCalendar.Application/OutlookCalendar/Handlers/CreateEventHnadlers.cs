using MediatR;
using Newtonsoft.Json;
using OutlookCalendar.Application.OutlookCalendar.Querries;
using OutlookCalendar.Domain.Core.Models;
using OutlookCalendar.Domain.Core.Repositories;
using OutlookCalendar.Domain.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OutlookCalendar.Application.OutlookCalendar.Handlers
{
    public class CreateEventHnadlers : IRequestHandler<CreateEventQuerries, ResponseBindingModel<string>>
    {
        IOutlookCalendarRequestRepository _outlookCalenadarRequestRepository;

        public CreateEventHnadlers(IOutlookCalendarRequestRepository outlookCalenadarRequestRepository)
        {
            _outlookCalenadarRequestRepository = outlookCalenadarRequestRepository;
        }

        public async Task<ResponseBindingModel<string>> Handle(CreateEventQuerries request, CancellationToken cancellationToken)
        {
            var response = new ResponseBindingModel<string>();

            try
            {
                OutlookEvent outlookEvent = new OutlookEvent()
                {
                    Id = request.Id,
                    Description = request.Description,
                    Start_time = request.Start_time,
                    End_time = request.End_time,
                    Name = request.Name
                };

                string result = await _outlookCalenadarRequestRepository.CreateEvent(outlookEvent, request.CalendarID);
                response.Result = result;
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.ErrorResult = new ErrorMessageBindingModel
                {
                    Code = "400",
                    Message = "Create Event Error = " + ex.Message
                };
            }

            return response;
        }
    }
}
