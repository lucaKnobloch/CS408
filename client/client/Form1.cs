using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {
        Semaphore requestConfirm = new Semaphore(0, 1);
        bool friendRequestValid = false;
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            string name = nameBox.Text;
            int portNum;
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {

                    if (IP != "")
                    {
                        if (name != "" && name.Length <= 64)
                        {
                            clientSocket.Connect(IP, portNum);
                            terminating = false;
                            // Send name
                            Byte[] buffer = new Byte[64];
                            buffer = Encoding.Default.GetBytes(name);
                            clientSocket.Send(buffer);

                            // Client waits here to recieve confirmation
                            Byte[] confirmBuffer = new Byte[64];
                            clientSocket.Receive(confirmBuffer);
                            string confirmName = Encoding.Default.GetString(confirmBuffer);
                            confirmName = confirmName.Substring(0, confirmName.IndexOf("\0"));

                            // Name confirmation is checked here
                            if (confirmName == "denied")
                            {
                                // Invalid name
                                logs.AppendText("Invalid Name\n");
                            }
                            else if (confirmName == "confirmed")
                            {
                                // Valid name
                                button_connect.Enabled = false;
                                textBox_message.Enabled = true;
                                button_send.Enabled = true;
                                disconnectButton.Enabled = true;
                                connected = true;
                                logs.AppendText("Connected to the server!\n");
                                Thread receiveThread = new Thread(Receive);
                                receiveThread.Start();
                            }
                        }
                        else
                        {
                            logs.AppendText("Add your name!\n");
                        }
                    }
                    else
                    {
                        logs.AppendText("Add a IP address\n");
                    }

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }

        }

        private void Receive()
        {
            this.Controls.Add(requestBox);
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage.StartsWith("friendrequestfrom"))
                    {
                        incomingMessage = incomingMessage.Replace("friendrequestfrom", "");
                        requestBox.BeginUpdate();
                        requestBox.Items.Add(incomingMessage);
                        requestBox.EndUpdate();
                    }
                    else if (incomingMessage.StartsWith("/"))
                    {
                        incomingMessage = incomingMessage.Replace("/", "");
                        friendRequestValid = false;
                        if (incomingMessage == "valid")
                            friendRequestValid = true;
                        requestConfirm.Release(1);
                    }
                    else
                    {
                        logs.AppendText(incomingMessage + "\n");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        disconnectButton.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;

            if (message != "")
            {
                if (message.Length <= 64)
                {
                    message = nameBox.Text + ": " + message;
                    Byte[] buffer = new Byte[64];
                    buffer = Encoding.Default.GetBytes(message);
                    clientSocket.Send(buffer);
                    logs.AppendText(message + "\n");
                }
                else
                {
                    logs.AppendText("The message is too long!" + "\n");
                }

            }

            else
            {
                logs.AppendText("Add a message to sent" + "\n");
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            logs.AppendText("Disconnected from server\n");
            button_connect.Enabled = true;
            textBox_message.Enabled = false;
            button_send.Enabled = false;
            disconnectButton.Enabled = false;
            clientSocket.Close();
            connected = false;
            terminating = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {
           
            string friendRequest;
            if (friend_box.Text == "" || friend_box.Text.Length > 64)
            {
                logs.AppendText("Invalid friend name\n");
            }
            else
            {
                friendRequest = "sentrequestby(" + nameBox.Text + ")" + friend_box.Text;
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(friendRequest);
                clientSocket.Send(buffer);
                requestConfirm.WaitOne();
                if (friendRequestValid)
                {
                    logs.AppendText("Friend invite sent to " + friend_box.Text + ".\n");
                }
                else
                {
                    logs.AppendText("Invalid friend name\n");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)//STEP 3 ? -Ozgur
        {
            string friendRemove;
            // TODO: Replace xyz with identifier for friend remove
            if (friend_box.Text == "" || friend_box.Text.Length > 64)
            {
                logs.AppendText("Invalid friend name\n");
            }
            else
            {
                friendRemove = "xyz" + nameBox.Text + "+" + friend_box.Text;
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(friendRemove);
                clientSocket.Send(buffer);
                requestConfirm.WaitOne();
                if (friendRequestValid)
                {
                    logs.AppendText(friend_box.Text + " removed from friends list" + ".\n");
                }
                else
                {
                    logs.AppendText("Invalid friend name\n");
                }
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (requestBox.SelectedItem.ToString() == "")
            {
                logs.AppendText("No friend request selected.");
            }
            else
            {
                string friendRequest = "freplyAccept" + nameBox.Text + "-" + requestBox.SelectedItem.ToString();
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(friendRequest);
                clientSocket.Send(buffer);
                logs.AppendText(requestBox.SelectedItem.ToString() + " added to friends list" + ".\n");
                requestBox.Items.Remove(requestBox.SelectedItem.ToString());
                requestBox.Update();
            }
        }

        private void rejectButton_Click(object sender, EventArgs e)
        {
            if (requestBox.SelectedItem.ToString() == "")
            {
                logs.AppendText("No friend request selected.");
            }
            else
            {
                string friendRequest = "freplyReject" + nameBox.Text + "-" + requestBox.SelectedItem.ToString();
                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(friendRequest);
                clientSocket.Send(buffer);
                logs.AppendText(friend_box.Text + " removed from friends requests" + ".\n");
                requestBox.Items.Remove(requestBox.SelectedItem.ToString());
                requestBox.Update();
            }
        }

        private void requestFriendList_Click(object sender, EventArgs e)
        {
            string flistreq = "flist" + nameBox.Text;
            Byte[] buffer = new Byte[64];
            buffer = Encoding.Default.GetBytes(flistreq);
            clientSocket.Send(buffer);
        }
    }
}
