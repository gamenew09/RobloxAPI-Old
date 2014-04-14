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
    public partial class UsersThumbnail : Form
    {
        public UsersThumbnail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                int.TryParse(textBox1.Text, out i);
                pictureBox1.Image = RobloxApi.GetUserThumbnail(i, 110, 110);
            }
            catch { MessageBox.Show("Failed to show thumbnail, you might've typed in a username instead of a integer(number)."); }
        }
    }
}
