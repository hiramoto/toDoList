using DDay.iCal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todoList
{
    public partial class Form1 : Form
    {
        private String balloonTxt = String.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            timer1.Interval = Properties.Settings.Default.Interval;
            showNotify();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            showNotify();
        }

        private void showNotify()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                String urlVal = Properties.Settings.Default.icsUrl;
                if (String.IsNullOrEmpty(urlVal)) return;

                Uri uri = new Uri(urlVal);
                IICalendarCollection iCal = iCalendar.LoadFromUri(uri);

                foreach (ICalendarObject obj in iCal)
                {
                    foreach (DDay.iCal.Todo mEvent in obj.Children)
                    {
                        if ((mEvent.Due) == null) continue;
                        if ((mEvent.Due.Date) >= DateTime.Today.AddDays(1)) continue;
                        sb.Append(mEvent.Summary + "\r\n");
                    }
                }
            }
            catch (WebException e)
            {
                sb.Append("unable to connect: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                sb.Append("unable to retrieve: " + e.Message);
            }
            balloonTxt = sb.ToString();
            notifyIcon1.BalloonTipText = balloonTxt;
            notifyIcon1.ShowBalloonTip(3);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(balloonTxt)) return;
            notifyIcon1.BalloonTipText = balloonTxt;
            notifyIcon1.ShowBalloonTip(3);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Dispose();
            Application.Exit();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form setting = new Settings())
            {
                setting.Enabled = true;
                if(setting.ShowDialog() == DialogResult.OK)
                {
                    this.Enabled = false;
                    this.ShowInTaskbar = false;
                    return;
                }
            }
        }

    }
}
