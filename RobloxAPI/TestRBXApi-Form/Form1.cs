using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestRBXApi_Form.Tests;
using RobloxAPI;
using System.Threading;

namespace TestRBXApi_Form
{
    public partial class Form1 : Form
    {
        Thread t;

        public Form1()
        {
            InitializeComponent();
            t = new Thread(testTh);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsersThumbnail form = new UsersThumbnail();
            form.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BlurbForm form = new BlurbForm();
            form.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointsForm form = new PointsForm();
            form.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetPlaces places = new GetPlaces();
            places.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MembershipGetter form = new MembershipGetter();
            form.Show(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProductImage form = new ProductImage();
            form.Show(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Leaderboards leader = new Leaderboards();
            leader.Show(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isEnabled = checkBox1.Checked;
        }

        private delegate void RBXTest();

        private void CheckRBXStatus()
        {
            label3.Text = (RobloxStats.IsRobloxDBUp()) ? "Fine" : "Down";
            label3.ForeColor = (RobloxStats.IsRobloxDBUp()) ? Color.Green : Color.Red;
            label4.Text = "RBX Player Count: " + RobloxStats.GetPlayerCount();
        }

        bool isEnabled = false;

        void testTh()
        {
            while (!IsDisposed)
            {
                try
                {
                    if (isEnabled)
                        Invoke(new RBXTest(CheckRBXStatus));
                }
                catch { }
                Thread.Sleep(1000);
            }
        }

       
    }
}
