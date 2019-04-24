using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMMV_PackMan
{
    public partial class frmLoading : Form
    {
        const int SECONDS_PER_MIN = 60;
        const int MIN_DOUBLE_DIGIT_NUM = 10;
        public bool PreciseIsDisposing { get; set; }
        long TimerCounter = 0;
        bool DelayedCloseCall = false;
        public frmLoading()
        {
            InitializeComponent();
        }
        public frmLoading(string status)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(status))
                lblStatus.Text = status;
          
        }

        public void ChangeStatus(string status)
        {
            lblStatus.Text = status;
        }

        public void SafeClose()
        {
            if (PreciseIsDisposing)
                return;
            if (!Visible)
            {
                DelayedCloseCall = true;
                return;
            }
            PreciseIsDisposing = true;
            this.Invoke((MethodInvoker)delegate
            {
                Close();
            });
        }

        private void timerElapsed_Tick(object sender, EventArgs e)
        {
            ++TimerCounter;
            lblElapsed.Text = "Elapsed Time: " + GetFormattedLongSeconds(TimerCounter);
            if (DelayedCloseCall)
                Close();
        }

        string GetFormattedLongSeconds(long sec)
        {
            if (sec < SECONDS_PER_MIN)
                return "00:" + GetDoubleDigit(sec);
            else
            {
                int minuteCount = (int)Math.Floor(sec / (double)SECONDS_PER_MIN);
                int remainder = (int)(sec % SECONDS_PER_MIN);
                return GetDoubleDigit(minuteCount) + ":" + GetDoubleDigit(remainder);
            }
        }

        string GetDoubleDigit(decimal num)
        {
            if (num < MIN_DOUBLE_DIGIT_NUM)
                return "0" + num.ToString();
            else
                return num.ToString();
        }

        private void frmLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            PreciseIsDisposing = true;
            timerElapsed.Stop();
        }

        private void frmLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
                e.Handled = true;
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            if (GlobalClass.MainForm == null || GlobalClass.MainForm.Disposing || GlobalClass.MainForm.IsDisposed || !GlobalClass.MainForm.Visible)
                CenterToScreen();
        }
    }
}
