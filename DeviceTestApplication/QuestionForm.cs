using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceTestApplication
{
    public partial class frmQuestionForm : Form
    {
        public frmQuestionForm()
        {
            InitializeComponent();
        }

        public string Question
        {
            get { return labelTitle.Text;}
            set { labelTitle.Text = value; }
        }

        public string Answer
        {
            get { return textBoxAnswer.Text; }
            set { textBoxAnswer.Text = value; }
        }

        private void frmQuestionForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r' && Answer.Length > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

    }
}
