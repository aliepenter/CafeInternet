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
    public partial class frmChangePass : Form
    {
        public int id;
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmChangePass()
        {
            InitializeComponent();
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
        }
    }
}
