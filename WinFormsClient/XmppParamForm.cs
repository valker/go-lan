using System.Windows.Forms;

namespace WinFormsClient
{
    public partial class XmppParamForm : Form
    {
        public XmppParamForm()
        {
            InitializeComponent();
            const string host = true ? "acer" : "MOSDB9VF4J";
            txtUserAccount.Text = "client@" + host;
            txtServerAccount.Text = "server@" + host;
        }

        public string PlayerName { get { return txtPlayerName.Text; } }

        public string UserAccount
        {
            get { return txtUserAccount.Text; }
        }

        public string ServerAccount
        {
            get { return txtServerAccount.Text; }
        }
    }
}
