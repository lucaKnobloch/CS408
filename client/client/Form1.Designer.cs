namespace client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.requestBox = new System.Windows.Forms.ListBox();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.friend_box = new System.Windows.Forms.TextBox();
            this.add_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.remove_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rejectButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.requestFriendList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_message_friends = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(89, 62);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(116, 22);
            this.textBox_ip.TabIndex = 2;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(89, 97);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(116, 22);
            this.textBox_port.TabIndex = 3;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(89, 172);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(116, 28);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(211, 62);
            this.logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(255, 325);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Enabled = false;
            this.textBox_message.Location = new System.Drawing.Point(211, 390);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(255, 22);
            this.textBox_message.TabIndex = 6;
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(211, 416);
            this.button_send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(256, 32);
            this.button_send.TabIndex = 8;
            this.button_send.Text = "Send to all";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(89, 130);
            this.nameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(116, 22);
            this.nameBox.TabIndex = 9;
            this.nameBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Name:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(89, 206);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(116, 28);
            this.disconnectButton.TabIndex = 10;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // requestBox
            // 
            this.requestBox.FormattingEnabled = true;
            this.requestBox.ItemHeight = 16;
            this.requestBox.Location = new System.Drawing.Point(469, 62);
            this.requestBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.requestBox.Name = "requestBox";
            this.requestBox.Size = new System.Drawing.Size(280, 244);
            this.requestBox.TabIndex = 11;
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(client.Form1);
            // 
            // friend_box
            // 
            this.friend_box.Location = new System.Drawing.Point(24, 268);
            this.friend_box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.friend_box.Name = "friend_box";
            this.friend_box.Size = new System.Drawing.Size(167, 22);
            this.friend_box.TabIndex = 12;
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(24, 295);
            this.add_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(83, 32);
            this.add_button.TabIndex = 13;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Friend Requests: ";
            // 
            // remove_button
            // 
            this.remove_button.Location = new System.Drawing.Point(108, 295);
            this.remove_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(83, 32);
            this.remove_button.TabIndex = 13;
            this.remove_button.Text = "Remove";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Friend Name:";
            // 
            // rejectButton
            // 
            this.rejectButton.Location = new System.Drawing.Point(619, 321);
            this.rejectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rejectButton.Name = "rejectButton";
            this.rejectButton.Size = new System.Drawing.Size(140, 32);
            this.rejectButton.TabIndex = 16;
            this.rejectButton.Text = "Reject";
            this.rejectButton.UseVisualStyleBackColor = true;
            this.rejectButton.Click += new System.EventHandler(this.rejectButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(473, 321);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(140, 32);
            this.acceptButton.TabIndex = 16;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // requestFriendList
            // 
            this.requestFriendList.Location = new System.Drawing.Point(24, 334);
            this.requestFriendList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.requestFriendList.Name = "requestFriendList";
            this.requestFriendList.Size = new System.Drawing.Size(167, 36);
            this.requestFriendList.TabIndex = 17;
            this.requestFriendList.Text = "Request Friends List";
            this.requestFriendList.UseVisualStyleBackColor = true;
            this.requestFriendList.Click += new System.EventHandler(this.requestFriendList_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(531, 417);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 31);
            this.button1.TabIndex = 19;
            this.button1.Text = "Send to friends";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_send_friends_Click);
            // 
            // textBox_message_friends
            // 
            this.textBox_message_friends.Location = new System.Drawing.Point(469, 390);
            this.textBox_message_friends.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_message_friends.Name = "textBox_message_friends";
            this.textBox_message_friends.Size = new System.Drawing.Size(280, 22);
            this.textBox_message_friends.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(475, 367);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Message to send to friends";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 375);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 37);
            this.button2.TabIndex = 22;
            this.button2.Text = "Request Invitations\r\n";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 462);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_message_friends);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.requestFriendList);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.rejectButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.remove_button);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.friend_box);
            this.Controls.Add(this.requestBox);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.ListBox requestBox;
        private System.Windows.Forms.TextBox friend_box;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button remove_button;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button rejectButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button requestFriendList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_message_friends;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}

