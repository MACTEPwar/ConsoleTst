using Client.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port;
            int.TryParse(textBox1.Text, out port);
            string address = textBox2.Text;
            init(port, "127.0.0.1");
        }

        MySocket s;

        void init(int port, string address)
        {
            s = new MySocket(address, port);
            s.Connect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string comand = textBox3.Text;
            if (s != null)
            {
                MessageBox.Show(s.Send(comand));
            }
        }
    }
}
