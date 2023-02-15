using MediatR;
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
    public class UpdateEventHnadlers : IRequestHandler<UpdateEventQuerries, ResponseBindingModel<string>>
    {
        IOutlookCalendarRequestRepository _outlookCalenadarRequestRepository;

        public UpdateEventHnadlers(IOutlookCalendarRequestRepository outlookCalenadarRequestRepository)
        {
            _outlookCalenadarRequestRepository = outlookCalenadarRequestRepository;
        }

        public async Task<ResponseBindingModel<string>> Handle(UpdateEventQuerries request, CancellationToken cancellationToken)
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

                string result = await _outlookCalenadarRequestRepository.UpdateEvent(outlookEvent);
                response.Result = result;
                response.Succeeded = true;

            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.ErrorResult = new ErrorMessageBindingModel
                {
                    Code = "400",
                    Message = "Update Event Error = " + ex.Message
                };
            }
            return response;
        }
    }
}
