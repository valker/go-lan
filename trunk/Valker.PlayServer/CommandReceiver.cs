using System;

namespace Valker.PlayServer
{
    public class CommandReceiver : ICommandReceiver
    {
        private static readonly char[] LineEnds = new char[] {'\n', '\r'};

        #region ICommandReceiver Members

        public event EventHandler MessageReceived;

        public void Start(IClient client)
        {
            client.ReadCompleted += ClientReadCompleted;
            client.ReadAsync();
        }

        #endregion

        private void ClientReadCompleted(object sender, ReadCompletedEventArgs e)
        {
            var client = (IClient) sender;
            int bytesRead = e.BytesRead;
            client.AppendMessageFromBuffer(bytesRead);
            string message = client.GetMessage();
            if (message.IndexOfAny(LineEnds) > -1)
            {
                message = message.Trim();
                client.ReadCompleted -= ClientReadCompleted;
                var args = new MessageReceivedEventArgs {Message = message, Client = client};
                InvokeMessageReceived(args);
            }
            else
            {
                client.ReadAsync();
            }
        }

        private void InvokeMessageReceived(EventArgs e)
        {
            EventHandler messageReceived;
            lock (this)
            {
                messageReceived = MessageReceived;
            }
            if (messageReceived != null)
            {
                messageReceived(this, e);
            }
        }
    }
}