using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.Api;
using Valker.PlayOnLan.UI;

namespace Valker.PlayOnLan.Transport
{
    public class LocalTransport : ITransport
    {
        private MainForm _mainForm;
        private LocalForm _localForm;

        List<LocalClientForm> _clients = new List<LocalClientForm>();

        public LocalTransport(MainForm form)
        {
            _mainForm = form;
        }

        public void Start()
        {
            _localForm = new LocalForm(this);
            _localForm.Show(_mainForm);
        }

        public void AddClient(string clientName)
        {
            AddClient(CreateClient(clientName));
        }

        private void AddClient(LocalClientForm client)
        {
            _clients.Add(client);
            InvokeClientAdded(new ClientEventArgs(){Client = client});
        }

        private LocalClientForm CreateClient(string clientName)
        {
            var client = new LocalClientForm(clientName);
            client.Closed += ClientOnClosed;
            return client;
        }

        private void ClientOnClosed(object sender, EventArgs args)
        {
            RemoveClient((LocalClientForm)sender);
        }

        public void RemoveClient(LocalClientForm client)
        {
            _clients.Remove(client);
            InvokeClientRemoved(new ClientEventArgs() { Client = client });
        }

        public event EventHandler<ClientEventArgs> ClientAdded;

        private void InvokeClientAdded(ClientEventArgs e)
        {
            EventHandler<ClientEventArgs> handler = ClientAdded;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ClientEventArgs> ClientRemoved;
        public string Name
        {
            get { return "Local"; }
        }

        private void InvokeClientRemoved(ClientEventArgs e)
        {
            EventHandler<ClientEventArgs> handler = ClientRemoved;
            if (handler != null) handler(this, e);
        }

        public LocalClientForm FindClient(string clientName)
        {
            return _clients.Find(client => client.Name == clientName);
        }
    }
}
