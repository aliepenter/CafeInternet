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
    public partial class frmAll : Form
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmAll()
        {
            InitializeComponent();
        }
        private void DisplayAll()
        {
            dgvAllCom.DataSource = dc.getAllComputersFromArea().ToList();
        }
        private void OpenCom()
        {
            var f = new computer_status();
            if (dgvAllCom.CurrentRow != null)
            {
                DataGridViewRow row = dgvAllCom.CurrentRow;
                f.computer_id = Convert.ToInt32(row.Cells[0].Value.ToString());
                f.status = 1;
                f.start_time = DateTime.Now;
                f.end_time = DateTime.Now; ;
                dc.computer_status.InsertOnSubmit(f);
                dc.SubmitChanges();
                //txtImage.Text = row.Cells[5].Value.ToString();
                //edit = true;
            }
        }
        private void frmAll_Load(object sender, EventArgs e)
        {
            DisplayAll();
        }

        private void dgvAllCom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenCom();
        }
    }
}
