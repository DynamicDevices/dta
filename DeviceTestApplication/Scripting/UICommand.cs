using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Renci.SshNet;

namespace DeviceTestApplication.Scripting
{
    public class UICommand : Command
    {
        public UICommand(XmlNode n) : base(n)
        {
            if (n.Attributes != null)
                foreach (XmlAttribute xa in n.Attributes)
                {
                    switch (xa.Name)
                    {
                        case "subType":
                            switch(xa.Value)
                            {
                                case "YesNo" :
                                    SubType = EnumUICommandSubType.Yesno;
                                    break;
                                case "Info":
                                    SubType = EnumUICommandSubType.Info;
                                    break;
                                default:
                                    Logger.Warn("Unknown UI command subtype: " + xa.Value);
                                    break;
                            }
                            break;
                        case "display":
                            Display = xa.Value;
                            break;
                        case "image" :
                            Image = xa.Value;
                            break;
                        default:
//                        _logger.Warn("Unknown control attribute: " + xa.Name);
                            break;
                    }
                }
        }

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
                if (string.IsNullOrEmpty(Image))
                {
                    switch (SubType)
                    {
                        case EnumUICommandSubType.Yesno:
                            DialogResult dr = MessageBoxEx.Show(Display, "Test Question", MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                                success = true;
                            break;
                        case EnumUICommandSubType.Info:
                            MessageBoxEx.Show(Display, "Test Information", MessageBoxButtons.OK,
                                              MessageBoxIcon.Information);
                            success = true;
                            break;
                        default:
                            Logger.Warn("Unknown UI command subtype " + SubType);
                            break;
                    }
                }
                else
                {
                    var f = new UIForm();
                    f.ImageFileName = Globals.CacheFolder + Path.DirectorySeparatorChar + Image;
                    switch (SubType)
                    {
                        case EnumUICommandSubType.Yesno:
                            f.Text = "Test Question";
                            f.Display = Display;
                            f.Buttons = MessageBoxButtons.YesNo;
                            DialogResult dr = f.ShowDialog();
                            if (dr == DialogResult.Yes)
                                success = true;
                            break;
                        case EnumUICommandSubType.Info:
                            f.Text = "Test Information";
                            f.Display = Display;
                            f.Buttons = MessageBoxButtons.OK;
                            f.ShowDialog();
                            success = true;
                            break;
                        default:
                            Logger.Warn("Unknown UI command subtype " + SubType);
                            break;
                    }                                         
                }
            }
            catch (Exception e)
            {
                Logger.Warn("Exception in UI display: " + e.Message);
            }
            return success;
        }

        public EnumUICommandSubType SubType { get; set; }

        public string Display { get; set; }

        public string Image { get; set; }
    }
}
