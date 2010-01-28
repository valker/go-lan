using System;

namespace go_engine.Data
{
    public class GoException : Exception
    {
        public GoException(ExceptionReason reason)
        {
            Reason = reason;
        }

        public ExceptionReason Reason { get; set; }
    }
}