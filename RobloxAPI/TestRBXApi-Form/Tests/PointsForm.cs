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
    public partial class PointsForm : Form
    {
        public PointsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                User usr = RobloxApi.GetUserById(int.Parse(textBox1.Text));
                Console.WriteLine(usr.Id + "  " + usr.Username);
                label2.Text = usr.Points.ToString();
            }
            catch { MessageBox.Show("An error occured when getting User's Points."); }
        }
    }
}
