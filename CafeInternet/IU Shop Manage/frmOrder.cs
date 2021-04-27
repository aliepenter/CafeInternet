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
    public partial class frmOrder : Form
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public int id;
        public float total;
        public frmOrder(int e,float f)
        {
            InitializeComponent();
            id = e;
            total = f;
        }
        private void DisplayTotal()
        {
            DataGridViewRow r = dgvOrder.CurrentRow;
            lbTotal.Text =(Convert.ToInt32(r.Cells[4].Value)+total).ToString();
        }
        
        private void frmOrder_Load(object sender, EventArgs e)
        {
            dgvOrder.DataSource = dc.getOrder(id);
            DisplayTotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
