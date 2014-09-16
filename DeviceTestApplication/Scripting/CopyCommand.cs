using System;
using System.IO;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class CopyCommand : Command
    {
        public CopyCommand(XmlNode n) : base(n)
        {
        }

        /// <summary>
        /// Copy from source to target
        /// </summary>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            var scp = new ScpClient(client.ConnectionInfo) {BufferSize = 8*1024};

            // Need this or we get Dropbear exceptions...
            scp.Connect();
            
            bool status = false;
            try
            {
                scp.Upload(new FileInfo(Source), Target);
                status = true;
            } catch(Exception e)
            {
                Logger.Warn("Exception in SCP transfer: " + e.Message);
            }

            return status;
        }
    }
}
