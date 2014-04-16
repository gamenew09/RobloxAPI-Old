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
            int i = -1;
            int.TryParse(textBox1.Text, out i);
            Console.WriteLine(i.ToString());
            if (i == 0)
            {
                pictureBox1.Image = RobloxApi.GetUserByUsername(textBox1.Text).Thumbnail;
            }
            else
            {
                pictureBox1.Image = RobloxApi.GetUserThumbnail(i, 110, 110);
            }
        }
    }
}
