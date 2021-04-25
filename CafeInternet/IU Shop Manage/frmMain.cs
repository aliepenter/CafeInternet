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
            if (f.status == 0)
            {
                f.status = 1;
                f.start_time = DateTime.Now;
                dc.SubmitChanges();
            }
            else
            {
                MessageBox.Show("This PC has been already in used!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //txtImage.Text = row.Cells[5].Value.ToString();
            //edit = true;
        }
        private void TurnOffCom()
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
            string name = row.Cells[1].Value.ToString();
            var f = dc.computer_status.FirstOrDefault(x => x.computer_id == id);
            if (f.status == 1)
            {
                f.status = 0;
                f.end_time = DateTime.Now;
                TimeSpan interval = f.end_time - f.start_time;
                int k = interval.Seconds;
                float total = Convert.ToInt32(row.Cells[4].Value.ToString()) * k / 3600;
                f.used_times += k;
                var m = dc.computers.FirstOrDefault(n => n.name == name);
                m.profit += total;
                m.total_used_time += k;
                dc.SubmitChanges();
            }
            else
            {
                MessageBox.Show("This PC isn't in used!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
            string name = row.Cells[1].Value.ToString();
            var f = dc.computer_status.FirstOrDefault(x => x.computer_id == id);
            if (f.status == 0)
            {
                f.status = 1;
                f.start_time = DateTime.Now;
                dc.SubmitChanges();
            }
            else
            {
                f.status = 0;
                f.end_time = DateTime.Now;
                TimeSpan interval = f.end_time - f.start_time;
                int k = interval.Seconds;
                float total = Convert.ToInt32(row.Cells[4].Value.ToString()) * k / 3600;
                f.used_times += k;
                var m = dc.computers.FirstOrDefault(n => n.name == name);
                m.profit += total;
                m.total_used_time += k;
                dc.SubmitChanges();
            }
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
            TurnOffCom();
            DisplayAll();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            OpenCom();
            DisplayPcOn();
            DisplayAll();
        }
    }
}
