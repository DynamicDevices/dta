using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace DeviceTestApplication.Scripting
{
    public class StreamCommand : Command
    {
        #region Fields

        #endregion

        public StreamCommand(XmlNode n)
            : base(n)
        {
            TimeoutSecs = 20;

            if (n.Attributes != null)
                foreach (XmlAttribute xa in n.Attributes)
                {
                    switch (xa.Name)
                    {
                            // Command string to send
                        case "send":
                            Send = ScriptHelpers.ReplaceVars(xa.Value);
                            break;
                            // Response should contain this string
                        case "successResponse":
                            SuccessResponse = ScriptHelpers.ReplaceVars(xa.Value);
                            break;
                        case "errorResponse":
                            ErrorResponse = ScriptHelpers.ReplaceVars(xa.Value);
                            break;
                            // Wait for up to x secs
                        case "waitForSecs":
                            try
                            {
                                TimeoutSecs = int.Parse(xa.Value);
                            }
                            catch(Exception e)
                            {
                                Logger.Warn("Problem parsing XML: " + e.Message);
                            }
                            break;
                        case "prompt":
                            Prompt = xa.Value;
                            break;
                    }
                }
        }

        /// <summary>
        ///  What to send to target
        /// </summary>
        public string Send { get; set; }

        /// <summary>
        ///  What to expect response to contain
        /// </summary>
        public string SuccessResponse { get; set; }

        /// <summary>
        /// If this string is in response then it is an error
        /// </summary>
        public string ErrorResponse { get; set; }

        /// <summary>
        /// How long to wait
        /// </summary>
        public int TimeoutSecs { get; set; }

        public string Prompt { get; set; }

        /// <summary>
        /// Perform some kind of control action
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override bool Execute(SshClient client)
        {
            bool success = false;
            var rxData = new StringBuilder();

            SshCommand cmd= null;
            try
            {
                if (string.IsNullOrEmpty(ErrorResponse) && string.IsNullOrEmpty(SuccessResponse))
                    throw new Exception("Need Error/Success response defined for StreamCommand");
                
                Logger.Debug("Command Tx: " + Send);

                cmd = client.CreateCommand(Send);
                cmd.CommandTimeout = new TimeSpan(0, 0, TimeoutSecs);
                cmd.BeginExecute(new AsyncCallback(ExecutionCallback), cmd);                

                var reader = new StreamReader(cmd.OutputStream);
    
                DateTime dtStart = DateTime.Now;
                long intervalS;
                bool timedOut = false;
                do
                {
                        if( reader.BaseStream.Length > 0)
                          rxData.Append(reader.ReadToEnd());
                        
                        if (rxData.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(ErrorResponse))
                            {
                                if (rxData.ToString().Contains(ErrorResponse))
                                {
                                    Logger.Warn("- Error response: " + rxData);
                                    return false;
                                }
                            }

                            if (!string.IsNullOrEmpty(SuccessResponse))
                            {
                                if (rxData.ToString().Contains(SuccessResponse))
                                {
                                    Logger.Warn("- Success response: " + rxData);
                                    success = true;
                                }
                            }
                        }

                    intervalS = (long)DateTime.Now.Subtract(dtStart).TotalSeconds;
                    if(TimeoutSecs > 0)
                        timedOut = intervalS > TimeoutSecs;
                } while (!timedOut && !success);

                if(timedOut)
                {
                    UpdateUI("*** Timed out");
                    return false;
                }

                // Perform regexp replacement...
                Output = !string.IsNullOrEmpty(OutputRegExp) ? Regex.Match(rxData.ToString(), OutputRegExp).Value : rxData.ToString();

                Logger.Debug("Output: " + (!string.IsNullOrEmpty(Output) ? Output : "<none>"));
            }
            catch (Exception ex)
            {
                Logger.Warn("Exception in worker thread: " + ex.Message);
                success = false;
            }
            finally
            {
                try
                {
                    // TODO: We seem to timeout and disconnect if we try to cancel. Perhaps better for now to kill the process in the next command instead
                    //cmd.CancelAsync();
                    cmd.Dispose();
                    cmd = null;
                }
                catch (Exception e)
                {
                    Logger.Warn("Exception stopping shell: " + e.Message);
                }
            }
            return success;
        }

        private void ExecutionCallback(IAsyncResult ar)
        {
            try
            {
                var cmd = (SshCommand) ar.AsyncState;
                cmd.EndExecute(ar);
            } catch
            {
            }

        }
    }
}
