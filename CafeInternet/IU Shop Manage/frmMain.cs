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
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmMain()
        {
            InitializeComponent();
            dc.send_data();

        }
        private void DisplayPcOn()
        {
            var ft = from n in dc.computer_status
                     select new
                     {
                         id = n.entity_id,
                         name = n.name
                     };
            comboBox1.DataSource = ft;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }
        private void DisplayAll()
        {
            //dgvAllCom.DataSource = dc.getAllComputersFromArea().ToList();
            dgvAllCom.DataSource = dc.getAllComputersFromComputerStatus().ToList();
            foreach (DataGridViewRow row in dgvAllCom.Rows)
            {
                if (row.Cells[2].Value.ToString() == "ONLINE")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }
        private void OpenCom()
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
            var f = dc.computer_status.FirstOrDefault(x => x.computer_id == id);
            f.status = 1;
            f.start_time = DateTime.Now.ToString();
            dc.SubmitChanges();
            //txtImage.Text = row.Cells[5].Value.ToString();
            //edit = true;
        }
        private void TurnOffCom()
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
            var f = dc.computer_status.FirstOrDefault(x => x.computer_id == id);
            f.status = 0;
            f.end_time = DateTime.Now.ToString();
            dc.SubmitChanges();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DisplayAll();
            DisplayPcOn();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbHour.Text = DateTime.Now.ToLongTimeString();
            lbDay.Text = DateTime.Now.ToLongDateString();
        }

        private void dgvAllCom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenCom();
            DisplayPcOn();
            DisplayAll();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            dc.deleteComputerStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }
    }
}
