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
using System.Threading;

namespace OnlineChess{
    public partial class MainMenuForm : Form{

        private NetworkStream stream;
        private TcpClient client;
        private const string CONNECTING_ERROR = "Could not connect to host", 
                             HOSTING_ERROR = "Could not to host", 
                             WAIT = "Waiting for connection...",
                             CONNECTING = "Connecting to host...", 
                             CONNECTED = "Connected", 
                             END = "End", 
                             BAD_IP_PORT = "Bad IP/PORT";


        public MainMenuForm()
        {
            InitializeComponent();
        }

        private String ReadMessage()
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            Int32 i = stream.Read(bytes, 0, bytes.Length);
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            return data;
        }

        private void WriteMessage(String data)
        {
            Byte[] bytes = new Byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }

        private void SendDisconnectMessage()
        {
            if(stream.CanRead)
            {
                byte[] bytes = new byte[] { Convert.ToByte(signalType.disconnect)};
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        }

        private async void HostButton_Click(object sender, EventArgs e){

            TcpListener server = null;
            HostButton.Enabled = false;
            JoinButton.Enabled = false;
            try
            {                
                Int32 port = Convert.ToInt32(PortTextBox.Text);
                IPAddress localAddr = IPAddress.Parse(IPTextBox.Text);

                server = new TcpListener(localAddr, port);
                server.Start();

                InfoLabel.Text = WAIT;

                TcpClient client = await server.AcceptTcpClientAsync();
                server.Stop();
                InfoLabel.Text = CONNECTED;

                // Get a stream object for reading and writing
                stream = client.GetStream();

                WriteMessage(CONNECTED);
                String response = ReadMessage();
                InfoLabel.Text = response.ToString();

                ChessUI chess = new ChessUI(stream, figureColor.White);
                chess.ShowDialog();
                InfoLabel.Text = END;
                SendDisconnectMessage();
                client.Close();
            }
            catch(SocketException)
            {
                InfoLabel.Text = HOSTING_ERROR;
            }
            catch (FormatException)
            {
                InfoLabel.Text = BAD_IP_PORT;
            }
            finally
            {
                // Stop listening for new clients
                if (server != null)
                    server.Stop();
                HostButton.Enabled = true;
                JoinButton.Enabled = true;
            }
        }

        public bool Join()
        {
            /*HostButton.Enabled = false;
            JoinButton.Enabled = false;*/
            try
            {
                //InfoLabel.Text = "Connecting to host...";
                // Create a TcpClient
                string ip = IPTextBox.Text;
                Int32 port = Convert.ToInt32(PortTextBox.Text);
                client = new TcpClient(ip, port);
                stream = client.GetStream();
                return true;

                // Get a client stream for reading and writing.
               

                /*String data = ReadMessage();
                InfoLabel.Text = data.ToString();
                WriteMessage("Connected");

                ChessUI chess = new ChessUI(stream, figureColor.Black);
                chess.ShowDialog();
                InfoLabel.Text = "end";
                SendDisconnectMessage();

                // Close everything
                client.Close();
                stream.Close();*/
            }
            catch (SocketException)
            {
                //MessageBox.Show("ds", "dsa", MessageBoxButtons.OK);
                /*InfoLabel.Text = "Could not connect to host";*/
                /*host.Enabled = true;
                join.Enabled = true;*/
                return false;
            }
            catch(FormatException)
            {
                return false;
            }
        }

        private async void JoinButton_Click(object sender, EventArgs e)
        {
            bool res = false;
            HostButton.Enabled = false;//
            JoinButton.Enabled = false;//
            InfoLabel.Text = CONNECTING;//
            await Task.Run(() => res = Join());
            if (!res)
                InfoLabel.Text = CONNECTING_ERROR;
            else
            {
                String data = ReadMessage();
                InfoLabel.Text = data.ToString();
                WriteMessage(CONNECTED);

                ChessUI chess = new ChessUI(stream, figureColor.Black);
                chess.ShowDialog();
                InfoLabel.Text = END;
                SendDisconnectMessage();

                // Close everything
                client.Close();
                stream.Close();
            }
            HostButton.Enabled = true;
            JoinButton.Enabled = true;
            /*try
            {
                
                // Create a TcpClient
                string ip = IPTextBox.Text;
                Int32 port = Convert.ToInt32(PortTextBox.Text);
                TcpClient client = new TcpClient(ip, port);


                // Get a client stream for reading and writing.
                this.stream = client.GetStream();


                String data = ReadMessage();
                InfoLabel.Text = data.ToString();
                WriteMessage("Connected");

                ChessUI chess = new ChessUI(stream, figureColor.Black);
                chess.ShowDialog();
                InfoLabel.Text = "end";
                SendDisconnectMessage();

                // Close everything
                client.Close();
                stream.Close();
                HostButton.Enabled = true;
                JoinButton.Enabled = true;
            }
            catch (SocketException)
            {
                InfoLabel.Text = "Could not connect to host";
                HostButton.Enabled = true;
                JoinButton.Enabled = true;
            }*/
        }
    }
}
