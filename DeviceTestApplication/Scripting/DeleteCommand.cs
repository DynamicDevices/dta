using System;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class DeleteCommand : Command
    {
        public DeleteCommand(XmlNode n) : base(n)
        {
        }

        /// <summary>
        /// Copy from source to target
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            try
            {
                var command1 = client.RunCommand("rm -Rf " + Target);
                return command1.ExitStatus == 0;

            } catch(Exception e)
            {
                Logger.Warn("Exception deleting files: " +e.Message);
            }
            return false;
        }
    }
}
