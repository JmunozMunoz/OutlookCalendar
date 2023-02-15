﻿using MediatR;
using OutlookCalendar.Domain.Core.Responses;
using System;

namespace OutlookCalendar.Application.OutlookCalendar.Querries
{
    public class CreateEventQuerries : IRequest<ResponseBindingModel<string>>
    {
        public string CalendarID { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
    }
}
