using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkLog
{
    public partial class ChangeFolder : Form
    {
        public ChangeFolder()
        {
            InitializeComponent();

            

            this.folder.Text = Properties.Settings.Default.folder;

            this.folderBrowserDialog1.SelectedPath = this.folder.Text;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Owner.Focus();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.folder.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void sv_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.folder = this.folder.Text;
            this.Hide();
            this.Owner.Focus();
            this.Dispose();
        }
    }
}
