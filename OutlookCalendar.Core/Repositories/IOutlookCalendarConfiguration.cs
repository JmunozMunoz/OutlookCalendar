using OutlookCalendar.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookCalendar.Domain.Core.Repositories
{
    public interface IOutlookCalendarConfiguration
    {
        public OAuth2Model OAuth2Model { get; }
    }
}
