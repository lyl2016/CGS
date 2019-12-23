using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdGroup
{
    public partial class MyForm : Form
    {
        private float xscale, yscale;
        public float Xscale
        {
            get { return this.xscale; }
        }
        public float Yscale
        {
            get { return this.yscale; }
        }
        public MyForm()
        {
            xscale = (float)1.0; yscale = (float)1.0;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xscale = (float)numericUpDown1.Value;
            yscale = (float)numericUpDown2.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
