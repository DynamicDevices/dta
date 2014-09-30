using System;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class ModemControlCommand : ControlCommand
    {
        #region Fields

        #endregion

        private static frmQuestionForm qForm;

        public ModemControlCommand(XmlNode n)
            : base(n)
        {
            if (n.Attributes != null)
                foreach (XmlAttribute xa in n.Attributes)
                {
                    switch (xa.Name)
                    {
                            // Command string to send
                        case "minCSQ":
                            try
                            {
                                MinCSQ = int.Parse(ScriptHelpers.ReplaceVars(xa.Value));
                            } catch(Exception e)
                            {
                                Logger.Warn("Exception parsing minCSQ: " + e.Message);
                            }
                            break;
                        case "hangUpOnFail":
                            try
                            {
                                HangUpOnFail = xa.Value;
                            } catch(Exception e)
                            {
                                Logger.Warn("Exception parsing hangUpOnFail" + e.Message);
                            }
                            break;
                    }
                }
        }

        protected int? MinCSQ { get; set; }

        protected string HangUpOnFail { get; set; }

        /// <summary>
        /// Perform some kind of control action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            if (Send.Contains("ATD"))
            {
                if (!Send.EndsWith(";"))
                    Send += ";";

                if (Send.Contains("$TESTPHONENUMBER"))
                {
                    if(qForm == null)
                        qForm = new frmQuestionForm {Question = "Please enter the number to dial"};
                    qForm.ShowDialog();

                    Send = Send.Replace("$TESTPHONENUMBER", qForm.Answer);
                }
            }

            bool success = base.Execute(client);
            if (!success)
            {
                if (!string.IsNullOrEmpty(HangUpOnFail))
                {
                    Send = HangUpOnFail;
                    base.Execute(client);
                }
                return false;
            }

            if (Send.Contains("AT+CSQ"))
            {
                if (MinCSQ.HasValue)
                {
                    try
                    {
                        string[] arr =
                            Output.Split(',');
                        int csq = int.Parse(arr[0]);
                        if (csq < MinCSQ)
                        {
                            Logger.Warn("CSQ is too low");
                            Output += " - CSQ too low (<" + MinCSQ + ")";
                            success = false;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn("Exception analysing CSQ response: " + e.Message);
                        success = false;

                    }
                }
            }
            return success;
        }
    }
}
