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
            lbHour.Text = DateTime.Now.ToLongTimeString();
            lbDay.Text = DateTime.Now.ToLongDateString();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
