using System;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class ExtractCommand : Command
    {
        public ExtractCommand(XmlNode n) : base(n)
        {
        }

        /// <summary>
        /// Copy from source to target
        /// </summary>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            bool success = false;
            try
            {
                var command1 = client.RunCommand("tar xzvf " + Target);
                var s1 = command1.Result;
                Output = s1;

                var command2 = client.RunCommand("echo $?");
                var s2 = command2.Result;
                
                var arrRsp = s2.Split(new[] {"\n"}, StringSplitOptions.None);

                if(arrRsp.Length > 0)
                    if (arrRsp[0] == "0")
                        success = true;

            } catch(Exception e)
            {
                Logger.Warn("Exception extracting archive: " +e.Message);
            }
            return success;
        }
    }
}
