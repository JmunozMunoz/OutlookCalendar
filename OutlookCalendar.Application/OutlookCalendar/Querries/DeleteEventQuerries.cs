using MediatR;
using OutlookCalendar.Domain.Core.Responses;

namespace OutlookCalendar.Application.OutlookCalendar.Querries
{
    public class DeleteEventQuerries : IRequest<ResponseBindingModel<string>>
    {
        public string Id { get; set; }
    }
}
