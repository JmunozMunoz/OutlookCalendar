using System;

namespace OutlookCalendar.Domain.Core.Responses
{
    public class OutlookEventResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
    }
}
