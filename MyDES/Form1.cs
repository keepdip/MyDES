using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox3.Text = Des.DESEncryption(textBox1.Text, textBox2.Text);
            textBox4.Text = Des.DESDecryption(textBox3.Text, textBox2.Text);
        }
    }
}
