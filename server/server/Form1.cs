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
public class Request //Class for keeping requests
{
    public string From { get; set; }
    public string To { get; set; }
}
namespace server
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> Onlines = new List<string>();
        Dictionary<string, List<string>> friendships = new Dictionary<string, List<string>>();

        string[] lines = System.IO.File.ReadAllLines(@"user_db.txt");
        bool terminating = false;
        bool listening = false;

        List<Request> pending = new List<Request>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
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


        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();

                    // Name is recieved here
                    Byte[] nameBuffer = new Byte[64];
                    newClient.Receive(nameBuffer);
                    string incomingName = Encoding.Default.GetString(nameBuffer);
                    incomingName = incomingName.Substring(0, incomingName.IndexOf("\0"));

                    // Name is checked here
                    if (!lines.Contains(incomingName) || Onlines.Contains(incomingName))
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
                        friendships.Add(incomingName, new List<string>());

                        foreach (Request req in pending)
                        {
                            if (incomingName == req.To)
                            {
                                try
                                {
                                    string request = "friendrequestfrom" + req.From;
                                    buffer = Encoding.Default.GetBytes(request);
                                    newClient.Send(buffer);
                                    pending.Remove(req);
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
                catch
                {
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

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (incomingMessage.StartsWith("sentrequestby("))//this is a friend request
                    {
                        string from = "", to = "";
                        int i = 0;
                        incomingMessage = incomingMessage.Replace("sentrequestby(", "");
                        while (incomingMessage.ElementAt(i) != ')')
                        {
                            from += incomingMessage.ElementAt(i);
                            i++;
                        }
                        i++;//to get rid of ')'
                        while (i < incomingMessage.Length)
                        {
                            to += incomingMessage.ElementAt(i);
                            i++;
                        }

                        if (lines.Contains(to) && !friendships[from].Contains(to))
                        {
                            string message = "/valid/";
                            buffer = Encoding.Default.GetBytes(message);
                            thisClient.Send(buffer);

                            if (!Onlines.Contains(to))
                                pending.Add(new Request { From = from, To = to });
                            else
                            {
                                string request = "friendrequestfrom" + from;
                                buffer = Encoding.Default.GetBytes(request);
                                int index = Onlines.FindIndex(a => a == to);
                                Socket invitee = clientSockets[index];
                                invitee.Send(buffer);
                            }
                        }
                        else
                        {
                            try
                            {
                                string message = "/invalid/";//request is invalid, invitee doesn't exist.
                                buffer = Encoding.Default.GetBytes(message);
                                thisClient.Send(buffer);
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
                    else if (incomingMessage.StartsWith("freply"))
                    {
                        incomingMessage = incomingMessage.Replace("freply", "");
                        if (incomingMessage.StartsWith("Reject"))
                        {
                            incomingMessage = incomingMessage.Replace("Reject", "");
                            string rejector = incomingMessage.Substring(0, incomingMessage.IndexOf("-"));
                            incomingMessage = incomingMessage.Replace(rejector + "-", "");
                            string rejected = incomingMessage;
                            if (!friendships[rejected].Contains(rejector) && !friendships[rejector].Contains(rejected))
                            {
                                string message = rejector + " rejected your friend invite.\n";
                                buffer = Encoding.Default.GetBytes(message);
                                clientSockets[Onlines.IndexOf(rejected)].Send(buffer);
                            }
                            else
                            {
                                string message = rejected + " is already in friends list.\n";
                                buffer = Encoding.Default.GetBytes(message);
                                thisClient.Send(buffer);
                                logs.AppendText(rejector + " tried to reject " + rejected + "'s already accepted friend request.\n");
                            }
                        }
                        else if (incomingMessage.StartsWith("Accept"))
                        {
                            incomingMessage = incomingMessage.Replace("Accept", "");
                            string accepter = incomingMessage.Substring(0, incomingMessage.IndexOf("-"));
                            incomingMessage = incomingMessage.Replace(accepter + "-", "");
                            string accepted = incomingMessage;
                            if(!friendships[accepted].Contains(accepter) && !friendships[accepter].Contains(accepted))
                            {
                                string message = accepter + " accepted your friend invite.\n";
                                buffer = Encoding.Default.GetBytes(message);
                                clientSockets[Onlines.IndexOf(accepted)].Send(buffer);
                                logs.AppendText(accepter + " accepted " + accepted + "'s friend request.\n");
                                friendships[accepted].Add(accepter);
                                friendships[accepter].Add(accepted);
                            }
                            else
                            {
                                string message = accepted + " is already in friends list.\n";
                                buffer = Encoding.Default.GetBytes(message);
                                thisClient.Send(buffer);
                                logs.AppendText(accepter + " tried to re-accepted " + accepted + "'s friend request.\n");
                            }
                        }
                    }
                    else if (incomingMessage.StartsWith("flist"))
                    {
                        incomingMessage = incomingMessage.Replace("flist", "");
                        logs.AppendText("Sending friends list to " + incomingMessage);
                        string flist = "Friends List: \n";
                        foreach (string s in friendships[incomingMessage])
                        {
                            flist += s + "\n";
                        }
                        buffer = Encoding.Default.GetBytes(flist);
                        thisClient.Send(buffer);

                    }
                    else
                        try
                        {
                            // find name of thisClient user
                            int index = clientSockets.FindIndex(socket => socket == thisClient);
                            string thisName = Onlines[index];
                            if (clientSockets.Count() > 0)
                            {
                                // clientsockets can be changed to the list of online clients
                                foreach (Socket client in (clientSockets))
                                {
                                    try
                                    {
                                        if (client != thisClient)
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
                                logs.AppendText("This message is broadcasted: " + incomingMessage + "\n");
                            }
                            else
                                logs.AppendText("Message could not be broadcasted only one client connected\n");

                        }
                        catch
                        {
                            if (!terminating)
                            {
                                logs.AppendText("A client has disconnected\n");
                            }
                            int index = clientSockets.FindIndex(socket => socket == thisClient);
                            string thisName = Onlines[index];
                            Onlines.Remove(thisName);
                            clientSockets.Remove(thisClient);
                            thisClient.Close();
                            connected = false;
                        }
                }
                catch
                {
                    logs.AppendText("A client disconnected.\n");
                    terminating = true;
                    textBox_message.Enabled = false;
                    button_send.Enabled = false;
                    textBox_port.Enabled = true;
                    button_listen.Enabled = true;
                    serverSocket.Close();
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
            if (message != "" && message.Length <= 64)
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
