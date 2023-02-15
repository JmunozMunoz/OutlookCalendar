using OutlookCalendar.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace OutlookCalendar.Domain.Core.Repositories
{
    public interface IOutlookCalendarRequestRepository
    {
        Task<string> CreateEvent(OutlookEvent @event, string calendarId);

        Task<string> UpdateEvent(OutlookEvent @event);

        Task DeleteEvent(string eventId);

        Task<ReceivedCalendarsData> GetOutlookCalendars();
    }
}
