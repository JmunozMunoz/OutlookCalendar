using MediatR;
using OutlookCalendar.Application.Mapper;
using OutlookCalendar.Application.OutlookCalendar.Querries;
using OutlookCalendar.Domain.Core.Repositories;
using OutlookCalendar.Domain.Core.Responses;
using OutlookCalendar.OutlookCalendar.Querries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OutlookCalendar.Application.OutlookCalendar.Handlers
{
    public class DeleteEventHandlers : IRequestHandler<DeleteEventQuerries, ResponseBindingModel<string>>
    {
        IOutlookCalendarRequestRepository _outlookCalenadarRequestRepository;

        public DeleteEventHandlers(IOutlookCalendarRequestRepository outlookCalenadarRequestRepository)
        {
            _outlookCalenadarRequestRepository = outlookCalenadarRequestRepository;
        }

        public async Task<ResponseBindingModel<string>> Handle(DeleteEventQuerries request, CancellationToken cancellationToken)
        {
            var response = new ResponseBindingModel<string>();

            try
            {
                await _outlookCalenadarRequestRepository.DeleteEvent(request.Id);

                response.Result = "OK";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.ErrorResult = new ErrorMessageBindingModel
                {
                    Code = "400",
                    Message = "Delete Event Error = " + ex.Message
                };
            }
            return response;
        }
    }
}
