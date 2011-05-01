using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WorkLog
{
    public partial class WorkLog : Form
    {
        string WORKLOG_FOLDER;

        public WorkLog()
        {
            InitializeComponent();

            if (Properties.Settings.Default.folder == "none")
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dir = Path.Combine(dir, "Worklogs");

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                Properties.Settings.Default.folder = dir;
                WORKLOG_FOLDER = dir;
            }
            else
            {
                folderCheck();
            }

            openDateFile(DateTime.Now.ToString("yyyy-MM-dd"));
        }

        private void folderCheck()
        {
            WORKLOG_FOLDER = Properties.Settings.Default.folder;

            if (!Directory.Exists(WORKLOG_FOLDER))
                Directory.CreateDirectory(WORKLOG_FOLDER);
        }

        public string Worklog_Folder
        {
            get { return WORKLOG_FOLDER; }
            set { WORKLOG_FOLDER = value; }
        }

        private void save_Click(object sender, EventArgs e)
        {
            folderCheck();

            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("hh:mm tt");

            string file = Path.Combine(WORKLOG_FOLDER, day+".txt");

            StreamWriter w = File.AppendText(file);
            w.WriteLine(time + "\t\t\t" + this.entry.Text);
            w.Close();

            this.entry.Clear();

            viewport.Text = File.ReadAllText(file);

            viewport.SelectionStart = viewport.Text.Length;
            viewport.ScrollToCaret();


        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string date = this.cal.SelectionStart.ToString("yyyy-MM-dd");

            openDateFile(date);
        }

        private void openDateFile(string date)
        {
            string file = Path.Combine(WORKLOG_FOLDER, date + ".txt");

            if (File.Exists(file))
                viewport.Text = File.ReadAllText(file);
            else
                viewport.Clear();

            viewport.Focus();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            entry.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog(this);
        }

        private void changeWorklogsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form c = new ChangeFolder();
            c.ShowDialog(this);

            folderCheck();
            openDateFile(cal.SelectionStart.ToString("yyyy-MM-dd"));

        }
    }
}
