using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ClientApp
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ConnectAsClient));
            t.Start();
        }

        private void ConnectAsClient()
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 5000);
            updateUI("เชื่อมต่อสำเร็จ");
            NetworkStream stream = client.GetStream();
            string str = "Hello World";
            byte[] message = Encoding.ASCII.GetBytes(str);
            stream.Write(message, 0, message.Length);
            
        }

        private void updateUI(string v)
        {
            Func<int> del = delegate ()
            {
                textBox1.AppendText(v + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);

        }
    }
}
