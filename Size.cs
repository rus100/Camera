using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Camera
{
    public partial class Size : Form
    {   
        public Size()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.height = (int)numericUpDown2.Value;
            Settings.width = (int)numericUpDown1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.width = (int)numericUpDown1.Value; 
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           Settings.height = (int)numericUpDown2.Value;
        }

    }
}
