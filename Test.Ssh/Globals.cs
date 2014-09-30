using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace DeviceTestApplication
{
    public class Globals
    {
        /// <summary>
        /// Folder to cache remote files (archives/scripts)
        /// </summary>
        public static string CacheFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Path.DirectorySeparatorChar + "Dynamic Devices" + Path.DirectorySeparatorChar + "DTA" + Path.DirectorySeparatorChar + "Cache"; }
        }

        /// <summary>
        /// Folder to store results
        /// </summary>
        public static string ResultFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Path.DirectorySeparatorChar + "Dynamic Devices" + Path.DirectorySeparatorChar + "DTA" + Path.DirectorySeparatorChar + "Results"; }
        }

        /// <summary>
        /// Local script file name
        /// </summary>
        public static string ScriptFileName
        {
            get
            {
                return CacheFolder + Path.DirectorySeparatorChar + "tix6testscript-1.0.xml";
            }
        }

        /// <summary>
        /// Local software archive file name
        /// </summary>
        public static string SoftwareArchiveFileName
        {
            get
            {
                return CacheFolder + Path.DirectorySeparatorChar + "tix6testarchive-1.0.tgz";
            }
        }

        /// <summary>
        /// Local resources archive file name
        /// </summary>
        public static string ResourcesArchiveFileName
        {
            get
            {
                return CacheFolder + Path.DirectorySeparatorChar + "tix6testresources-1.0.tgz";
            }
        }

        /// <summary>
        /// Address of device we're currently testing
        /// </summary>
        public static IPAddress DeviceAddress { get; set; }

        /// <summary>
        /// Whether test device is pingable
        /// </summary>
        public static bool DevicePingable { get; set; }

        /// <summary>
        /// Current producer SN
        /// </summary>
        public static string ProducerSerialNumber { get; set; }

        /// <summary>
        /// Current customer SN
        /// </summary>
        public static string CustomerSerialNumber { get; set; }

        public static bool CacheIsEnabled
        {
            get
            {
                bool stat = false;

                try
                {
                    if (ConfigurationManager.AppSettings["system_disable_cache"] != null)
                        stat = !bool.Parse(ConfigurationManager.AppSettings["system_disable_cache"]);
                }
                catch (Exception)
                {

                }
                return stat;
            }
        }

        public static string ProductionDBURI
        {
            get
            {
                string db = null;

                try
                {
                    if (ConfigurationManager.AppSettings["db_production_uri"] != null)
                        db = ConfigurationManager.AppSettings["db_production_uri"];
                }
                catch (Exception)
                {

                }
                return db;
            }
        }

        public static string DevelopmentDBURI
        {
            get
            {
                string db = null;

                try
                {
                    if (ConfigurationManager.AppSettings["db_development_uri"] != null)
                        db = ConfigurationManager.AppSettings["db_development_uri"];
                }
                catch (Exception)
                {

                }
                return db;
            }
        }

        private static Dictionary<string, string> _collScriptVariables = new Dictionary<string, string>();
 
        public static Dictionary<string, string> ScriptVariables { get { return _collScriptVariables; } }
    }
}
