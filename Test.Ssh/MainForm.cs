using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeviceTestApplication;
using DeviceTestApplication.Scripting;
using Renci.SshNet;

namespace Test.Ssh
{
    public partial class MainForm : Form
    {
        protected SshClient _client;

        public MainForm()
        {
            InitializeComponent();
        }

        private void DoLog(string text)
        {
            toolStripStatusLabel1.Text = text;
        }

        private void ButtonConnectClick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            Application.DoEvents();

            _client = new SshClient(textBoxIPAddr.Text, textBoxLogin.Text, textBoxPassword.Text);

            _client.Connect();
            toolStripStatusLabel1.Text = "Connection: " + _client.IsConnected;
            Application.DoEvents();

            bool stat;
            string output;

            var script = new Script(Globals.ScriptFileName);

                script.Initialisation.SshClient = _client;
//                script.Initialisation.OnUpdateUI -= testScript_OnUpdateUI;
  //              script.Initialisation.OnUpdateUI += testScript_OnUpdateUI;

                stat = script.Initialisation.Execute(textBoxIPAddr.Text, textBoxLogin.Text,
                                                     textBoxPassword.Text, out output);

            toolStripStatusLabel1.Text = "Copy status: " + stat;
        }

        private void ButtonDisconnectClick(object sender, EventArgs e)
        {
            if(_client != null)
            {
                _client.Disconnect();
                _client.Dispose();
                _client = null;
                toolStripStatusLabel1.Text = "Disconnected";
            }
        }
    }
}
