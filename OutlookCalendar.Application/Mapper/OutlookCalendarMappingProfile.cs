using AutoMapper;
using OutlookCalendar.Domain.Core.Models;
using OutlookCalendar.Domain.Core.Responses;

namespace OutlookCalendar.Application.Mapper
{
    public class OutlookCalendarMappingProfile : Profile
    {
        public OutlookCalendarMappingProfile()
        {
            CreateMap<OutlookEvent, OutlookEventResponse>().ReverseMap();
            CreateMap<Value, OutlookCalendarResponse>().ReverseMap();
        }
    }
}
