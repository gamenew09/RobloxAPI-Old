using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RobloxAPI;

namespace TestRBXApi_Form.Tests
{
    public partial class BlurbForm : Form
    {
        public BlurbForm()
        {
            InitializeComponent();
        }

        public void GetAndShowBlurb(int id)
        {
            richTextBox1.Text = RobloxApi.GetUserBlurb(id);
            toolStripStatusLabel1.Text = RobloxApi.GetUserById(id).Username;
        }

        private void BlurbForm_Load(object sender, EventArgs e)
        {
            richTextBox1.DetectUrls = true;
            GetAndShowBlurb(5762824);
        }
    }
}
