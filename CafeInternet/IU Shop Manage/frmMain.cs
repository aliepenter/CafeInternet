using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeInternet
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmAll fsa = new frmAll();
            fsa.TopLevel = false;
            Size s = pnlShow.Size;
            pnlShow.Controls.Clear();
            fsa.Size = s;
            fsa.Dock = DockStyle.Fill;
            fsa.Show();
            pnlShow.Controls.Add(fsa);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbHour.Text = DateTime.Now.ToLongTimeString();
            lbDay.Text = DateTime.Now.ToLongDateString();
        }
    }
}
