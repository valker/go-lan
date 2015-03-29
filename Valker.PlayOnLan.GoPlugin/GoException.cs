using System;

namespace Valker.PlayOnLan.GoPlugin
{
    public class GoException : Exception
    {
        public ExceptionReason Reason { get; set; }

        public GoException(ExceptionReason reason)
        {
            Reason = reason;
        }
    }
}