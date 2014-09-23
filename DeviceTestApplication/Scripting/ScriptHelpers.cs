using System;
using System.IO;
using System.Text;

namespace DeviceTestApplication.Scripting
{
    public class ScriptHelpers
    {
        public static string ReplaceVars(string s)
        {
            var sb = new StringBuilder(s);

            var scrURI = new Uri(Globals.DeviceClass.SoftwareURL);
            var strFileName = Globals.CacheFolder + Path.DirectorySeparatorChar + scrURI.Segments[scrURI.Segments.Length - 1];

            sb = sb.Replace("$BREAK",
                            "\f");

            sb = sb.Replace("$ARCHIVE", strFileName);

            sb = sb.Replace("$LOCALRESULTFILE",
                            Globals.LocalResultFile);

            sb = sb.Replace("$REMOTERESULTFILE",
                            Globals.RemoteResultFile);

            sb = sb.Replace("$PRODUCERSERIALNUMBER",
                Globals.ProducerSerialNumber);

            sb = sb.Replace("$CUSTOMERSERIALNUMBER",
                            Globals.CustomerSerialNumber);

            // TODO: Revisit the time strings
            sb = sb.Replace("$yyyy-MM-dd",
                            DateTime.Now.ToString("yyyy-MM-dd"));

            sb = sb.Replace("$HH:mm:ss",
                            DateTime.Now.ToString("HH:mm:ss"));

            sb = sb.Replace("$HH:mm",
                            DateTime.Now.ToString("HH:mm"));


            // Generate custom serial)

            if(string.IsNullOrEmpty(Globals.ProducerSerialNumber))
                Globals.ProducerSerialNumber = "";

            if (s.Contains("$IMXFORMATCUSTOMERSERIALNUMBER"))
            {
                string vsn = Globals.CustomerSerialNumber.PadLeft(32, '0');

                string vsn2 = "0x";
                for (int i = 0; i < 32; i++)
                {
                    if ((i > 0) && (i%8) == 0)
                        vsn2 += ",0x";

                    vsn2 += vsn[i];
                }
                sb = sb.Replace("$IMXFORMATCUSTOMERSERIALNUMBER", vsn2);
            }

            if (s.Contains("$IMXFORMATPRODUCERSERIALNUMBER"))
            {
                string vsn = Globals.ProducerSerialNumber.PadLeft(32, '0');

                string vsn2 = "";
                for (int i = 0; i < 32; i++)
                {
                    if ((i > 0) && (i % 8) == 0)
                        vsn2 += ",";

                    vsn2 += vsn[i];
                }
                sb = sb.Replace("$IMXFORMATPRODUCERSERIALNUMBER", vsn2);
            }

            return sb.ToString();
        }
    }
}
