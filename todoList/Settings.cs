using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todoList
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.icsUrl = textBox1.Text;
            Properties.Settings.Default.Interval = int.Parse(listBox1.SelectedItem.ToString())*1000;
            this.Visible = false;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.icsUrl;
            listBox1.SelectedItem = (Properties.Settings.Default.Interval/1000).ToString();
        }
    }
}
