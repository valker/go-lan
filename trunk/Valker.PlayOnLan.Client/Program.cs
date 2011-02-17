﻿using System.Windows.Forms;
using Valker.PlayOnLan.Client.Communication;

namespace Valker.PlayOnLan.Client
{
    class Program
    {
        static void Main()
        {
            var client = new ClientImpl();
            Form form = new MainForm(client);
            Application.Run(form);
        }
    }
}