using System;
using System.Xml;
using log4net;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public delegate void UpdateUIHandler(object sender, string info);

    public class Command
    {
        #region Fields

        protected static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event UpdateUIHandler OnUpdateUI;

        #endregion

        #region Constructor

        public Command()
        {

        }

        public Command(XmlNode n)
        {
            OutputRegExp = "";

            if (n.Attributes != null)
                foreach(XmlAttribute xa in n.Attributes)
                {
                    switch(xa.Name)
                    {
                        case "mode" :
                            switch(xa.Value)
                            {
                                case "force" :
                                    Mode = EnumMode.Force;
                                    break;
                                default :
                                    Logger.Warn("Unknown attribute value: " + xa.Name + "="  +xa.Value);
                                    break;
                            }
                            break;
                        case "source" :
                            Source = ScriptHelpers.ReplaceVars(xa.Value);
                            break;
                        case "target" :
                            Target = ScriptHelpers.ReplaceVars(xa.Value);
                            break;
                        case "log" :
                            Log = ScriptHelpers.ReplaceVars(xa.Value);
                            break;
                        case "failureMode":
                            switch(xa.Value)
                            {
                                case "fail":
                                    FailureMode = EnumFailureMode.Fail;
                                    break;
                                case "ignore":
                                    FailureMode = EnumFailureMode.Ignore;
                                    break;
                                case "warn":
                                    FailureMode = EnumFailureMode.Warn;
                                    break;

                            }
                            break;
                        case "forceStoreOutput" :
                            try
                            {
                                ForceStoreOutput = bool.Parse(xa.Value);
                            } catch(Exception e)
                            {
                                Logger.Warn("Problem parsing XML: " + e.Message);
                            }
                            break;
                        case "outputRegExp" :
                            OutputRegExp = xa.Value;
                            break;
                        case "initWait" :
                            try
                            {
                                InitWaitSecs = int.Parse(xa.Value);
                            } catch(Exception e)
                            {
                                Logger.Warn("Problem parsing XML: " + e.Message);
                            }
                            break;
                    }
                }
        }

        #endregion

        public virtual bool Execute(SshClient client)
        {
            return false;
        }

        protected void UpdateUI(string info)
        {
            if(OnUpdateUI != null)
            {
                try
                {
                    OnUpdateUI(this, info);   
                } catch(Exception e)
                {
                    Logger.Warn("Problem updating UI: " + e.Message);
                }
            }
        }
        /// <summary>
        /// Operation mode
        /// </summary>
        public EnumMode Mode { get; set; }

        /// <summary>
        /// Source file/directory
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Target file/directory
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Log entry
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// What to do on failure
        /// </summary>
        public EnumFailureMode FailureMode { get; set; }

        /// <summary>
        /// Output from command
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Always store the output (even if successful)
        /// </summary>
        public bool ForceStoreOutput { get; set; }

        /// <summary>
        /// Parse output through this regexp
        /// </summary>
        public string OutputRegExp { get; set; }

        /// <summary>
        /// Wait for seconds at start
        /// </summary>
        public int InitWaitSecs { get; set; }
    }
}
