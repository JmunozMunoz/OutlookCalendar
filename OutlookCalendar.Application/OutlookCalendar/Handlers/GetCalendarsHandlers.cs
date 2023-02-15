using MediatR;
using OutlookCalendar.Application.Mapper;
using OutlookCalendar.Domain.Core.Repositories;
using OutlookCalendar.Domain.Core.Responses;
using OutlookCalendar.OutlookCalendar.Querries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OutlookCalendar.Application.OutlookCalendar.Handlers
{
    public class GetCalendarsHandlers : IRequestHandler<GetCalendarsQuerries, ResponseBindingModel<List<OutlookCalendarResponse>>>
    {
        IOutlookCalendarRequestRepository _outlookCalenadarRequestRepository;

        public GetCalendarsHandlers(IOutlookCalendarRequestRepository outlookCalenadarRequestRepository)
        {
            _outlookCalenadarRequestRepository = outlookCalenadarRequestRepository;
        }

        public async Task<ResponseBindingModel<List<OutlookCalendarResponse>>> Handle(GetCalendarsQuerries request, CancellationToken cancellationToken)
        {
            var response = new ResponseBindingModel<List<OutlookCalendarResponse>>();

            try
            {
                var entity = await _outlookCalenadarRequestRepository.GetOutlookCalendars();

                response.Succeeded = !(entity is null);
                response.Result = OutlookCalendarMapper.Mapper.Map<List<OutlookCalendarResponse>>(entity);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.ErrorResult = new ErrorMessageBindingModel
                {
                    Code = "400",
                    Message = "GetCalendarsQuerries Event Error = " + ex.Message

                };
            }

            return response;
        }
    }
}
