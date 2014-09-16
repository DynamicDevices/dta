using System;
using System.Text;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class GPSStreamCommand : StreamCommand
    {
        #region Fields

        #endregion

        public GPSStreamCommand(XmlNode n)
            : base(n)
        {
        }

        /// <summary>
        /// Perform some kind of control action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            bool success = base.Execute(client);

            if(!success)
                return false;

            // Was this a check for a lock ?
            if (SuccessResponse.Contains("$GPGSA"))
            {
                // If so check we have one!
                //
                // $--GGA,hhmmss.ss,llll.ll,a,yyyyy.yy,a,x,xx,x.x,x.x,M,x.x,M,x.x,xxxx 
                //
                // hhmmss.ss = UTC of position
                // llll.ll = latitude of position
                // a = N or S
                // yyyyy.yy = Longitude of position
                // a = E or W
                // x = GPS Quality indicator (0=no fix, 1=GPS fix, 2=Dif. GPS fix)
                // xx = number of satellites in use
                // x.x = horizontal dilution of precision
                // x.x = Antenna altitude above mean-sea-level
                // M = units of antenna altitude, meters
                // x.x = Geoidal separation
                // M = units of geoidal separation, meters
                // x.x = Age of Differential GPS data (seconds)
                // xxxx = Differential reference station ID 
                var parts = Output.Split(new[] {','});

                if(parts.Length >= 7)
                {
                    try
                    {
                        var eFix = (EnumGPSFix) int.Parse(parts[6]);

                        if(eFix == EnumGPSFix.None)
                        {
                            Output = "No GPS Fix: " + Output;
                            Logger.Warn("GPS has no fix: " + Output);
                            success = false;
                        }
                    }
                    catch(Exception e)
                    {
                        Logger.Warn("Exception parsing GPS sentence: " + e.Message);
                    }
               }
            }
            return success;
        }

    }
}
