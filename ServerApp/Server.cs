﻿using System;
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

namespace TPCIP_Form
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread tcpserver = new Thread(new ThreadStart(TcpServerRUN));
            tcpserver.Start();
        }

        private void TcpServerRUN()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any,5000);
            tcpListener.Start();
            updateUI("รอการเชื่อมต่อ...");
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                updateUI("เชื่อมต่อแล้ว");
                Thread tcpHandlerThread = new Thread(new ParameterizedThreadStart(TcpHandler));
                tcpHandlerThread.Start(client);
            }
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

        private void TcpHandler(object client)
        {
            TcpClient mClient = (TcpClient)client;
            NetworkStream stream = mClient.GetStream();
            byte[] message = new byte[1024];
            stream.Read(message, 0, message.Length);
            updateUI("Message: " + Encoding.ASCII.GetString(message));
            stream.Close();
            mClient.Close();
        }
    }
}
