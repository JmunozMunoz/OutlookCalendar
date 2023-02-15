using MediatR;
using OutlookCalendar.Domain.Core.Responses;
using System.Collections.Generic;

namespace OutlookCalendar.OutlookCalendar.Querries
{
    public class GetCalendarsQuerries : IRequest<ResponseBindingModel<List<OutlookCalendarResponse>>>
    {
    }
}
