using System;

namespace OutlookCalendar.Domain.Core.Exceptions
{
    public class GeneralBusinessException : Exception
    {
        public GeneralBusinessException()
        {
        }

        public GeneralBusinessException(string message) : base(message)
        {
        }
    }
}
