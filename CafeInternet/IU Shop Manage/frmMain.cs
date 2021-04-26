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
        public int amount = 0;
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmMain()
        {
            InitializeComponent();
            dc.send_data();
        }
        private void DisplayPcOn()
        {
            var food = from m in dc.foods where m.quantity > 0
                     select new
                     {
                         idf = m.entity_id,
                         namef = m.name
                     };
            cbFoods.DataSource = food;
            cbFoods.DisplayMember = "namef";
            cbFoods.ValueMember = "idf";
            //var food1 = dc.getFoods(1);
            //dgvFoods.DataSource = food1.ToList();
            //var food2 = dc.getFoods(2);
            //dgvBdrinks.DataSource = food2.ToList();
            //var food3 = dc.getFoods(3);
            //dgvCdrinks.DataSource = food3.ToList();
            //var food4 = dc.getFoods(4);
            //dgvOthers.DataSource = food4.ToList();
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
                dc.deleteServiceCondition(name);
            }
            DisplayPcOn();
            DisplayAll();
            dgvService.DataSource = dc.get_service_price();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            dc.deleteComputerStatus();
            dc.deleteService();
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

        private void dgvFoods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            DisplayPcOn();
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            
            
                amount++;
                txtAmount.Text = amount.ToString(); 
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            if (amount != 0 )
            {
                amount--;
                txtAmount.Text = amount.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            string name = row.Cells[1].Value.ToString();
            var u = dc.foods.FirstOrDefault(x => x.entity_id == Convert.ToInt32(cbFoods.SelectedValue));
            var com = dc.computer_status.FirstOrDefault(x => x.name == name);
            var f = dc.services.FirstOrDefault(x => x.computer_name == name);
            if (u.quantity >= Convert.ToInt32(txtAmount.Text))
            {
                if (f == null)
                {
                    var s = new service();
                    s.computer_name = name;
                    s.service_name = cbFoods.SelectedValue.ToString();
                    s.quantity = Convert.ToInt32(txtAmount.Text);
                    s.total = u.price * Convert.ToInt32(txtAmount.Text);
                    dc.services.InsertOnSubmit(s);
                    dc.SubmitChanges();
                    
                }
                else
                {
                    if (f.service_name == (cbFoods.SelectedValue.ToString()))
                    {
                        var m = dc.services.FirstOrDefault(x => x.computer_name == name && x.service_name == cbFoods.SelectedValue.ToString());
                        m.quantity += Convert.ToInt32(txtAmount.Text);
                        m.total = u.price * Convert.ToInt32(txtAmount.Text);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        var s = new service();
                        s.computer_name = name;
                        s.service_name = cbFoods.SelectedValue.ToString();
                        s.quantity = Convert.ToInt32(txtAmount.Text);
                        s.total = u.price * Convert.ToInt32(txtAmount.Text);
                        dc.services.InsertOnSubmit(s);
                        dc.SubmitChanges();
                        //m.quantity += Convert.ToInt32(txtAmount.Text);
                        //m.total = u.price * Convert.ToInt32(txtAmount.Text);
                        //dc.services.InsertOnSubmit(m);
                        //dc.SubmitChanges();
                    }
                }
                dgvService.DataSource = dc.get_service_price();
            }
            else
            {
                MessageBox.Show("There are only " + u.quantity + " items remain!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
