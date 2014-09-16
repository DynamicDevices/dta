using System;
using System.Configuration;
using System.IO;
using System.Net;
using DynamicDevices.Testing.DAO;

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
                var scrURI = new Uri(DeviceClass.ScriptURL);
                return  CacheFolder + Path.DirectorySeparatorChar + scrURI.Segments[scrURI.Segments.Length - 1];
            }
        }

        /// <summary>
        /// Local software archive file name
        /// </summary>
        public static string SoftwareArchiveFileName
        {
            get
            {
                var swURI = new Uri(DeviceClass.SoftwareURL);
                return CacheFolder + Path.DirectorySeparatorChar + swURI.Segments[swURI.Segments.Length - 1];
            }
        }

        /// <summary>
        /// Local resources archive file name
        /// </summary>
        public static string ResourcesArchiveFileName
        {
            get
            {
                var resURI = new Uri(DeviceClass.ResourcesURL);
                return CacheFolder + Path.DirectorySeparatorChar + resURI.Segments[resURI.Segments.Length - 1];
            }
        }

        /// <summary>
        /// Currently logged in test user
        /// </summary>
        public static Employee LoggedInUser { get; set; }

        /// <summary>
        /// Class of device we're currently testing
        /// </summary>
        public static DeviceClass DeviceClass { get; set; }

        /// <summary>
        /// Address of device we're currently testing
        /// </summary>
        public static IPAddress DeviceAddress { get; set; }

        /// <summary>
        /// Whether test device is pingable
        /// </summary>
        public static bool DevicePingable { get; set; }

        /// <summary>
        /// TestList to run
        /// </summary>
        public static TestList TestList { get; set; }

        /// <summary>
        /// Device object newly created for device under test
        /// </summary>
        public static Device Device { get; set; }

        /// <summary>
        /// Test location
        /// </summary>
        public static TestLocation TestLocation { get; set; }

        /// <summary>
        /// Current producer SN
        /// </summary>
        public static string ProducerSerialNumber { get; set; }

        /// <summary>
        /// Current customer SN
        /// </summary>
        public static string CustomerSerialNumber { get; set; }

        public static string LocalResultFile
        {
            get
            {
                return ResultFolder + Path.DirectorySeparatorChar 
                    + Device.DeviceClass.Name 
                    + "_" 
                    + Device.ProducerSerialNumber 
                    + ".xml";
            }
        }

        public static string RemoteResultFile
        {
            get
            {
                return Device.DeviceClass.Name + "_" + Device.ProducerSerialNumber + ".xml";
            }
        }

        public static bool CacheIsEnabled
        {
            get
            {
                bool stat = false;

                try
                {
                    if (ConfigurationManager.AppSettings["system_disable_cache"] != null)
                        stat = !bool.Parse(ConfigurationManager.AppSettings["system_disable_cache"]);
                } catch(Exception)
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

    }
}
