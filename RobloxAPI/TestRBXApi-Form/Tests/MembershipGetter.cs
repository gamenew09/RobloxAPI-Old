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
    public partial class MembershipGetter : Form
    {
        public MembershipGetter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User usr = RobloxApi.GetUserById(1);
            MessageBox.Show(usr.Membership.ToString());
        }
    }
}
