namespace GapService
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class GapException : Exception
    {
        public GapException()
        {
        }

        public GapException(string message) : base(message)
        {
        }

        protected GapException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public GapException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

