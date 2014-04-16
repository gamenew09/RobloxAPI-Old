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
    public partial class GetPlaces : Form
    {
        public GetPlaces()
        {
            InitializeComponent();
        }

        RobloxAPI.SearchQuery q = new RobloxAPI.SearchQuery();

        private void GetPlaces_Load(object sender, EventArgs e)
        {
            q.Arguments.Add("sortFilter","1");
            q.Arguments.Add("MaxRows", "5");
            propertyGrid1.SelectedObject = q;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(q.ToString());
                listBox1.Items.Clear();
                listBox1.Items.AddRange(RobloxApi.SearchForPlaces(q));
            }
            catch { }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox1.SelectedItem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = q;
        }
    }
}
