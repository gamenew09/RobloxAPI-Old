using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestRBXApi_Form.Tests;

namespace TestRBXApi_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
