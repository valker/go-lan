using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    public partial class PlayingForm : Form
    {
        public PlayingForm(int n)
        {
            InitializeComponent();
            goban1.N = n;
        }
    }
}
