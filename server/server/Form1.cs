using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if(Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                
                // lenght of all the valid users -> cant be more than that
                serverSocket.Listen(300);

                listening = true;
                button_listen.Enabled = false;
                textBox_message.Enabled = true;
                button_send.Enabled = true;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

 List<string> Onlines = new List<string>();
        private void Accept()
        {
            while(listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();

                    // Name is recieved here
                    Byte[] nameBuffer = new Byte[64];
                    newClient.Receive(nameBuffer);
                    string incomingName = Encoding.Default.GetString(nameBuffer);
                    incomingName = incomingName.Substring(0, incomingName.IndexOf("\0"));
                    
                    string[] lines = System.IO.File.ReadAllLines(@"user_db.txt");
                       

                    // Name is checked here
                    if(!lines.Contains(incomingName) || Onlines.Contains(incomingName))
                    {
                        // Invalid name
                        string wakeClient = "denied";
                        Byte[] buffer = Encoding.Default.GetBytes(wakeClient);
                        newClient.Send(buffer);
                        newClient.Shutdown(SocketShutdown.Both);
                        logs.AppendText("A client tried to connect with invalid name.\n");
                        newClient.Close();

                    }
                    else
                    {
                        // Valid name
                        clientSockets.Add(newClient);
                        logs.AppendText("Client : " + incomingName + " is connected. \n");
                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();

                        // sends message 
                        string wakeClient = "confirmed";
                        Byte[] buffer = Encoding.Default.GetBytes(wakeClient);
                        newClient.Send(buffer);

                        // adds name 
                        Onlines.Add(incomingName);

                    }
                }
                catch (Exception e)
                {
                    logs.AppendText(e.ToString());
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive()
        {
            Socket thisClient = clientSockets[clientSockets.Count() - 1];
            bool connected = true;

            while(connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    // find name of thisClient user
                    int index = clientSockets.FindIndex(socket => socket == thisClient);
                    string thisName = Onlines[index];
                    // thisClient will be removed from the list to not get the own message
                    Onlines.Remove(thisName);
                    clientSockets.Remove(thisClient);
                    if (clientSockets.Count() > 0)
                    {
                        // clientsockets can be changed to the list of online clients 
                        foreach (Socket client in (clientSockets))
                        {
                            try
                            {
                                logs.AppendText("This message is broadcasted: " + incomingMessage + "\n");
                                client.Send(buffer);
                            }
                            catch
                            {
                                logs.AppendText("There is a problem! Check the connection...\n");
                                terminating = true;
                                textBox_message.Enabled = false;
                                button_send.Enabled = false;
                                textBox_port.Enabled = true;
                                button_listen.Enabled = true;
                                serverSocket.Close();
                            }

                        }

                    }
                    else
                        logs.AppendText("Message could not be broadcasted only one client connected");
                    // socket will be added again to not miss the further messages 
                    clientSockets.Add(thisClient);
                    Onlines.Add(thisName);
                }
                catch
                {
                    if(!terminating)
                    {
                        logs.AppendText("A client has disconnected\n");
                    }
                    Onlines.RemoveAt(clientSockets.FindIndex(socket => socket == thisClient));
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;
            if(message != "" && message.Length <= 64)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                
                foreach (Socket client in clientSockets)
                {
                    try
                    {
                        client.Send(buffer);
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }

                }
            }
        }
    }
}
