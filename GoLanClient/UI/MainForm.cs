using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoLanClient.Engine;
using MyGoban;

namespace GoLanClient.UI
{
    public partial class MainForm : Form
    {
        public MainForm(IEngine engine)
        {
            Engine = engine;
            InitializeComponent();
        }

        protected IEngine Engine { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartMonitoring();

            comboBox1.DataSource = Engine.Rules;
        }

        private void StartMonitoring()
        {
            Engine.PropertyChanged += EngineOnPropertyChanged;
            Engine.NeiboursChanged += EngineOnNeiboursChanged;
            Engine.Start();
        }

        private void EngineOnNeiboursChanged(object sender, NeiboursChangedEventArgs args)
        {
            Invoke(new EventHandler<NeiboursChangedEventArgs>(delegate(object o, NeiboursChangedEventArgs eventArgs)
                                                                  {
                                                                      if (eventArgs.Added)
                                                                      {
                                                                          var item = listView1.Items.Add(eventArgs.Neibour.Name);
                                                                          item.Name = eventArgs.Neibour.Name;
                                                                      } 
                                                                      else
                                                                      {
                                                                          var index = listView1.Items.IndexOfKey(eventArgs.Neibour.Name);
                                                                          if (index != -1)
                                                                          {
                                                                              listView1.Items.RemoveAt(index);
                                                                          }
                                                                      }
                                                                  }),new object[]{sender,args});
        }

        private void EngineOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Invoke(new EventHandler(delegate(object o, EventArgs eventArgs) { 
            

                listView1.SuspendLayout();

                listView1.Items.Clear();

                foreach (INeibour neibour in Engine.Neibours)
                {
                    listView1.Items.Add(neibour.Name);
                }

                listView1.ResumeLayout(true);
                listView1.Invalidate();
            }), null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rules = (IRules) comboBox1.SelectedItem;
            var control = rules.GetControl();
            control.Name = "playfield";
            groupBox2.Controls.Add(control);
            comboBox1.Hide();
            button1.Hide();
            control.Dock=DockStyle.Fill;
        }
    }
}