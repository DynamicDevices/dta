using System;
using System.Collections.Generic;
using System.Xml;
using log4net;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class Test
    {
        #region Fields

        protected static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<Command> _commands = new List<Command>();

        public string Name { get; set; }

        public List<Command> Commands { get { return _commands; } set { _commands = value; } }

        public Script TestScript { get; set; }

        public event UpdateUIHandler OnUpdateUI;

        #endregion

        #region Factory Methods

        public static Test CreateTest(XmlNode n, Script script)
        {
            var a = new Test {TestScript = script};

            if (n.Attributes != null)
            {
                if (n.Attributes["name"] == null)
                {
                    Logger.Warn("Test has no name - ignoring");
                    throw new Exception("Test has no name");
                }

                // Make sure there's no capitalisation issue
                a.Name = n.Attributes["name"].Value.ToLower();
            }

            // TBD Setup commands within action
            XmlNodeList cnl = n.SelectNodes("command");
            if (cnl != null)
                foreach (XmlNode cn in cnl)
                {
                    if (cn.Attributes != null)
                        switch (cn.Attributes["type"].Value)
                        {
                            case "copy":
                                a.Commands.Add(new CopyCommand(cn));
                                break;

                            case "extract":
                                a.Commands.Add(new ExtractCommand(cn));
                                break;

                            case "delete":
                                a.Commands.Add(new DeleteCommand(cn));
                                break;

                            case "control":
                                a.Commands.Add(new ControlCommand(cn));
                                break;

                            case "modemcontrol":
                                a.Commands.Add(new ModemControlCommand(cn));
                                break;

                            case "stream":
                                a.Commands.Add(new StreamCommand(cn));
                                break;

                            case "gpsstream":
                                a.Commands.Add(new GPSStreamCommand(cn));
                                break;

                            case "ui":
                                a.Commands.Add(new UICommand(cn));
                                break;

                            case "upload":
                                a.Commands.Add(new UploadCommand(cn));
                                break;

                            case "readserial" :
                                a.Commands.Add(new ReadSerialCommand(cn));
                                break;

                            case "report":
                                a.Commands.Add(new ReportCommand(cn));
                                break;

                            default:
                                Logger.Warn("Unknown command type: " + cn.Attributes["type"].Value);
                                break;
                        }
                }
            return a;
        }

        #endregion

        #region Properties

        public Command LastCommand { get; set; }

        public SshClient SshClient { get; set; }

        #endregion

        #region Commands

        // TODO: Rethink the API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool Execute(string remoteHost, string login, string password, out string output)
        {
            output = "";

            try
            {
                int commandNum = 0;
                foreach (var c in Commands)
                {
                    bool stat;

                    commandNum++;

                    LastCommand = c;

                    c.OnUpdateUI -= c_OnUpdateUI;
                    c.OnUpdateUI += c_OnUpdateUI;

                    if(OnUpdateUI != null)
                        OnUpdateUI(this, "- executing command: (" + commandNum + "): " + c.Log);

                    if (c is UploadCommand)
                        stat = ((UploadCommand)c).Execute(remoteHost, login, password);
                    else if(c is ReadSerialCommand)
                    {
                        stat = c.Execute(SshClient);
                        // Make sure we store the output
                        c.ForceStoreOutput = true;
                    }
                    else
                    {
                        stat = c.Execute(SshClient);
                    }

                    if(c.ForceStoreOutput)
                    {
                        output = c.Output;
                    }

                    if (!stat)
                    {
                        output = c.Output;

                        switch (c.FailureMode)
                        {
                            case EnumFailureMode.Ignore:
                                Logger.Warn("Command failed, (ignoring) output " + c.Output);
                                break;
                            case EnumFailureMode.Fail:
                                Logger.Warn("Command failed, (failing) output " + c.Output);
                                return false;
                            case EnumFailureMode.Warn:
                                // TBD: WHAT TO DO HERE ?
                                Logger.Warn("Command failed, (warning/ignoring) output " + c.Output);
                                break;
                            default:
                                Logger.Warn("Command failed, unknown failure mode, failing, output: " + c.Output);
                                return false;
                        }
                    }
                }
            } catch(Exception e)
            {
                Logger.Warn("Exception executing commands: " + e.Message);
                return false;
            }
            return true;
        }

        void c_OnUpdateUI(object sender, string info)
        {
            if(OnUpdateUI != null)
            {
                OnUpdateUI(sender, info);
            }
        }

        #endregion

    }
}
