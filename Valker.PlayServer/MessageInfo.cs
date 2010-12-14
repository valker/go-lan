namespace Valker.PlayServer
{
    public class MessageInfo : IMessageInfo
    {
        public MessageInfo(byte[] value, bool disposing)
        {
            Value = value;
            Disposing = disposing;
        }

        #region IMessageInfo Members

        public bool Disposing { get; set; }

        public byte[] Value { get; set; }

        #endregion
    }
}