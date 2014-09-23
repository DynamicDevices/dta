using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class ControlCommand : Command
    {
        #region Fields

        #endregion

        public ControlCommand(XmlNode n)
            : base(n)
        {
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

            try
            {
                Logger.Debug("Command Tx: " + Send);

                var command1 = client.RunCommand(Send);

                // If we have no success response defined but we do have an error response then assume success to begin
                if (!string.IsNullOrEmpty(ErrorResponse) && string.IsNullOrEmpty(SuccessResponse))
                    success = true;

                if (string.IsNullOrEmpty(ErrorResponse) && string.IsNullOrEmpty(SuccessResponse))
                {
                    // Just check exit status
                    if (command1.ExitStatus != 0)
                    {
                        Logger.Warn("Error exit status: " + command1.ExitStatus);
                        if (FailureMode != EnumFailureMode.Warn)
                        {
                            Output = "Error exit status: " + command1.ExitStatus + ": " + command1.Result;
                        }
                        return false;
                    }
                    success = true;
                }

                var response = command1.Result;
                
                // For some reason we seem to get command output back in error? (Maybe? It's the error stream - aha...)
                if (string.IsNullOrEmpty(response))
                    response = command1.Error;

                if (!string.IsNullOrEmpty(ErrorResponse))
                {
                    if (response.Contains(ErrorResponse))
                    {
                        Logger.Warn("- Error response: " + response);
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(SuccessResponse))
                {
                    if (response.Contains(SuccessResponse))
                    {
                        Logger.Warn("- Success response: " + response);
                        success = true;
                    }
                }

                // Perform regexp replacement...
                Output = !string.IsNullOrEmpty(OutputRegExp) ? Regex.Match(response, OutputRegExp).Value : response;

                Logger.Debug("Output: " + (!string.IsNullOrEmpty(Output) ? Output : "<none>"));
            }
            catch (Exception e)
            {
                Logger.Warn("Exception in worker thread: " + e.Message);
                success = false;
            }

            return success;
        }

    }
}
