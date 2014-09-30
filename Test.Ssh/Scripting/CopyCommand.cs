using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
            Debug.WriteLine("CopyCommand");

            var scp = new ScpClient(client.ConnectionInfo) {BufferSize = 8*1024};

            Debug.WriteLine("Connect");

            // Need this or we get Dropbear exceptions...
            scp.Connect();

            Debug.WriteLine("Upload");

            bool status = false;
            try
            {
                scp.Upload(new FileInfo(Source), Target);
                status = true;
            } catch(Exception e)
            {
                Logger.Warn("Exception in SCP transfer: " + e.Message);
            }

            Debug.WriteLine("Done");

            return status;
        }
    }
}
