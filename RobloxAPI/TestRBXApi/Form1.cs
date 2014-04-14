using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RobloxAPI;
using System.Reflection;
using System.Diagnostics;

namespace TestRBXApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        User user;

        private void Form1_Load(object sender, EventArgs e)
        {
            user = RobloxApi.GetUserById(100);
            ChangePage();
        }

        public void ChangePage()
        {
            pictureBox1.Image = user.Thumbnail;
            label1.Text = user.Username;
            textBox1.Text = user.Blurb;
            listBox1.Items.Clear();
            listBox1.Items.AddRange(user.Friends);
            listBox2.Items.Clear();
            try
            {
                listBox2.Items.AddRange(RobloxApi.GetPlacesFrom(user.Id).Showcase.ToArray());
            } 
            catch{ }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int o = 0;
                int.TryParse(textBox2.Text, out o);
                user = RobloxApi.GetUserById(o);
                ChangePage();
            }
            catch { }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                user = (User)listBox1.SelectedItem;
                ChangePage();
            }
            catch { }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch { }
        }

    }
}
