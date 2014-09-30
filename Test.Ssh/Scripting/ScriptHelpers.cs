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

            var strFileName = Globals.CacheFolder + Path.DirectorySeparatorChar + "tix6testarchive-1.0.tgz";

            sb = sb.Replace("$BREAK",
                            "\f");

            sb = sb.Replace("$ARCHIVE", strFileName);

            return sb.ToString();
        }
    }
}
