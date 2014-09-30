using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class ReportCommand : Command
    {
        public ReportCommand(XmlNode n) : base(n)
        {
            Command = "notepad.exe";

            if (n.Attributes != null)
                foreach (XmlAttribute xa in n.Attributes)
                {
                    switch (xa.Name)
                    {
                        case "sourceFile":
                            SourceFile = xa.Value;
                            break;
                        case "command":
                            Command = xa.Value;
                            break;
                        default:
//                        _logger.Warn("Unknown control attribute: " + xa.Name);
                            break;
                    }
                }
        }

        /// <summary>
        /// Perform some kind of control action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            bool success = false;
            try
            {
                var sourcePath = Path.Combine(Globals.CacheFolder, SourceFile);
                if(!File.Exists(sourcePath))
                {
                    Logger.Warn("SourceFile '" + sourcePath + "' does not exist");
                    return false;
                }

                var sourceText = File.ReadAllText(sourcePath);

                var displayText = ScriptHelpers.ReplaceVars(sourceText);

                var displayPath = Path.Combine(Globals.CacheFolder,
                                               Globals.CustomerSerialNumber + DateTime.Now.Ticks + ".tmp");
                
                File.WriteAllText(displayPath, displayText);

                Process.Start(Command, displayPath);

                success = true;
            }
            catch (Exception e)
            {
                Logger.Warn("Exception in report command: " + e.Message);
            }
            return success;
        }

        public string Command { get; set; }

        public string SourceFile { get; set; }
    }
}
