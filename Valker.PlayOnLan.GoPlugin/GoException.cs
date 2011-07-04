using System;

namespace Valker.PlayOnLan.GoPlugin
{
    internal class GoException : Exception
    {
        public ExceptionReason Reason { get; set; }

        public GoException(ExceptionReason reason)
        {
            Reason = reason;
        }
    }
}