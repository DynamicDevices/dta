using System;
using System.Xml;

namespace DeviceTestApplication.Scripting
{
    public class UploadCommand : Command
    {        
        public UploadCommand(XmlNode n) : base(n)
        {
            
        }

        /// <summary>
        /// Perform some kind of control action
        /// </summary>
        /// <returns></returns>
        public bool Execute(string remoteHost, string login, string password)
        {
            bool success = false;
            try
            {
                // ONLY HANDLE FTP FOR NOW
                //
                // AJL - Append DateTime so we don't overwrite existing info                
//                string remote = Globals.DeviceClass.ResultsURL + "/" + Target + "." + DateTime.Now.Ticks;

  //              success = RemoteFileManager.UploadFile(new Uri(remote), Source);
    //            if(!success)
      //              Logger.Warn("Failed uploading results '" + Source + "' to '" + remote + "'");
            }
            catch (Exception e)
            {
                Logger.Warn("Exception uploading file: " + e.Message);
            }
            return success;
        }
    }
}
