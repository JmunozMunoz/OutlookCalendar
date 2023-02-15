using System;

namespace OutlookCalendar.Domain.Core.Exceptions
{
    public class UnauthorizedBusinessException : Exception
    {
        public UnauthorizedBusinessException()
        {
        }

        public UnauthorizedBusinessException(string message) : base(message)
        {
        }
    }
}
