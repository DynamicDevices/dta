using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceTestApplication.Scripting
{
    public partial class UIForm : Form
    {
        protected MessageBoxButtons _buttons = MessageBoxButtons.YesNoCancel;
        protected string _imageFilename;

        public UIForm()
        {
            InitializeComponent();
        }

        public string Display
        {
            get { return textBoxDisplay.Text; }
            set { textBoxDisplay.Text = value; 
//                SizeF size = labelDisplay.CreateGraphics().MeasureString(labelDisplay.Text, labelDisplay.Font);
  //              labelDisplay.Left = (int)((Width - size.Width)/2);
            }
        }

        public MessageBoxButtons Buttons
        {
            get { return _buttons; }
            set
            {
                if(_buttons != value)
                {
                    if (value == MessageBoxButtons.OK)
                    {
                        buttonOK.Visible = true;
                        buttonYes.Visible = false;
                        buttonNo.Visible = false;
                    }
                    else if (value == MessageBoxButtons.YesNo)
                    {
                        buttonOK.Visible = false;
                        buttonYes.Visible = true;
                        buttonNo.Visible = true;
                    }
                    else
                    {
                        buttonOK.Visible = false;
                        buttonYes.Visible = false;
                        buttonNo.Visible = false;
                    }
                }
                _buttons = value;
            }
        }

        public string ImageFileName
        {
            get { return _imageFilename; }
            set { _imageFilename = value;
                pictureBox1.Image = new Bitmap(_imageFilename);
            }
        }
    }
}
