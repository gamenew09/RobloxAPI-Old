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
    public partial class ProductImage : Form
    {
        public ProductImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(textBox1.Text);
                pictureBox1.Image = RobloxApi.GetThumbnailImage(RobloxApi.GetProductInfo(i), 110, 110);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while getting the image: "+ex.Message);
            }
        }
    }
}
