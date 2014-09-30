using System;
using System.Collections.Generic;
using System.Xml;
using log4net;

namespace DeviceTestApplication.Scripting
{
    public class Script
    {
        protected static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Dictionary<string,Test> _collTests = new Dictionary<string,Test>();

        public Script(string fileName)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            // Populate actions
            if (xmlDoc.SelectSingleNode("/testscript/@name") != null)
            {
                var n = xmlDoc.SelectSingleNode("/testscript/@name");
                if (n != null) Name = n.Value;
            }
            if (xmlDoc.SelectSingleNode("/testscript/@version") != null)
            {
                var n = xmlDoc.SelectSingleNode("/testscript/@version");
                if (n != null) Version = new Version(n.Value);
            }
            if (xmlDoc.SelectSingleNode("/testscript/@prompt") != null)
            {
                var n = xmlDoc.SelectSingleNode("/testscript/@prompt");
                if (n != null) Prompt = n.Value;
            }

            XmlNodeList nodes = xmlDoc.SelectNodes("/testscript/test");
            if (nodes != null)
            {
                foreach (XmlNode n in nodes)
                {
                    try
                    {
                        var a = Test.CreateTest(n, this);
                        _collTests.Add(a.Name, a);
                    } catch(Exception e)
                    {
                        Logger.Warn("Exception creating test: " + e.Message);
                    }
                }
            }
        }

        #region Properties

        public string Name { get; set; }

        public Version Version { get; set; }

        /// <summary>
        /// Remote prompt response to expect
        /// </summary>
        public string Prompt { get; set; }

        public Dictionary<string,Test> Tests
        {
            get { return new Dictionary<string,Test>(_collTests); }
        }

        public Test Initialisation
        {
            get { 
                if(_collTests.ContainsKey("init")) 
                    return _collTests["init"];                 
                return null;
            }
        }

        public Test ReadSerial
        {
            get
            {
                if (_collTests.ContainsKey("readserialnumber"))
                    return _collTests["readserialnumber"];
                return null;
            }
        }

        public Test End
        {
            get
            {
                if (_collTests.ContainsKey("end"))
                    return _collTests["end"];
                return null;
            }
        }

        #endregion

    }
}
