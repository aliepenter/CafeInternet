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
        public string check;
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmMain(string f)
        {
            InitializeComponent();
            dc.send_data();
            check = f;
            this.dgvLog.DefaultCellStyle.ForeColor = Color.Blue;
            this.dgvLog.DefaultCellStyle.SelectionBackColor = this.dgvLog.DefaultCellStyle.BackColor;
            this.dgvLog.DefaultCellStyle.SelectionForeColor = this.dgvLog.DefaultCellStyle.ForeColor;
        }
        //public frmMain()
        //{
        //    InitializeComponent();
        //    
        //}
        private void DisplayPcOn()
        {
            var food = from m in dc.foods
                       where m.quantity > 0
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
                    row.DefaultCellStyle.ForeColor = Color.Yellow;
                    row.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
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
                var k = new log();
                k.timenow = DateTime.Now;
                k.computer_name = f.name;
                k.notice = "Session started";
                dc.logs.InsertOnSubmit(k);
                dc.SubmitChanges();
                dgvLog.DataSource = dc.getLog();
                dgvLog.Sort(dgvLog.Columns[0], ListSortDirection.Descending);
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
            int pro = 0;
            var f = dc.computer_status.FirstOrDefault(x => x.computer_id == id);
            if (f.status == 1)
            {
                f.end_time = DateTime.Now;
                TimeSpan interval = f.end_time - f.start_time;
                int k = interval.Seconds;
                float total = Convert.ToInt32(row.Cells[4].Value.ToString()) * k / 3600;
                var o = new order();
                o.computer_id = f.computer_id;
                o.computer_name = f.name;
                o.s_time = f.start_time;
                o.e_time = f.end_time;
                o.tt_time = interval.Seconds;
                o.servie = Convert.ToDouble(f.service_charge);
                dc.orders.InsertOnSubmit(o);
                dc.SubmitChanges();
                frmOrder fo = new frmOrder(id, total);
                fo.ShowDialog();
                f.status = 0;
                f.used_times += k;
                var m = dc.computers.FirstOrDefault(n => n.name == name);
                m.profit += total;
                m.total_used_time += k;
                
                dc.deleteServiceCondition(id);
                dc.SubmitChanges();
                var p = new log();
                p.timenow = DateTime.Now;
                p.computer_name = f.name;
                p.notice = "Session stopped";
                p.total = f.service_charge + total;
                dc.logs.InsertOnSubmit(p);
                f.service_charge = 0;
                dc.SubmitChanges();
                dgvLog.DataSource = dc.getLog();
                dgvLog.Sort(dgvLog.Columns[0], ListSortDirection.Descending);
                
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
            dgvLog.DataSource = dc.getLog().ToList();
            lbNameCashier.Text = check;
            lbTotalPc.Text = (dc.countTotalPc()).ToString();
            lbOff.Text = (dc.countOfflinePc()).ToString();
            lbOn.Text = (dc.countTotalPc() - dc.countOfflinePc()).ToString();
            lbRatio.Text = ((Convert.ToDouble(dc.countTotalPc()) - Convert.ToDouble(dc.countOfflinePc())) / Convert.ToDouble(dc.countTotalPc()) * 100).ToString("0");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbHour.Text = DateTime.Now.ToLongTimeString();
            lbDay.Text = DateTime.Now.ToLongDateString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var k = dc.computer_status.FirstOrDefault(x => x.status == 1);
            if (k == null)
            {
                this.Dispose();
                dc.deleteComputerStatus();
                dc.deleteService();
                dc.deletelog();
            }
            else
            {
                MessageBox.Show("You can not exit this time!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TurnOffCom();
            DisplayAll();
            lbOff.Text = (dc.countOfflinePc()).ToString();
            lbOn.Text = (dc.countTotalPc() - dc.countOfflinePc()).ToString();
            lbRatio.Text = ((Convert.ToDouble(dc.countTotalPc()) - Convert.ToDouble(dc.countOfflinePc())) / Convert.ToDouble(dc.countTotalPc()) * 100).ToString("0");
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            OpenCom();
            DisplayPcOn();
            DisplayAll();
            lbOff.Text = (dc.countOfflinePc()).ToString();
            lbOn.Text = (dc.countTotalPc() - dc.countOfflinePc()).ToString();
            lbRatio.Text = ((Convert.ToDouble(dc.countTotalPc()) - Convert.ToDouble(dc.countOfflinePc())) / Convert.ToDouble(dc.countTotalPc())*100).ToString("0");

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
            amount--;
            txtAmount.Text = amount.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int v = dc.getComputerStatus(Convert.ToInt32(row.Cells[0].Value.ToString()));
            if (v == 1)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                var u = dc.foods.FirstOrDefault(x => x.entity_id == Convert.ToInt32(cbFoods.SelectedValue));
                var com = dc.computer_status.FirstOrDefault(x => x.computer_id == id);
                var f = dc.services.FirstOrDefault(x => x.computer_id == id);
                var l = dc.services.FirstOrDefault(x => x.service_name == cbFoods.SelectedValue.ToString());
                if (u.quantity >= Convert.ToInt32(txtAmount.Text))
                {
                    if (f == null)
                    {
                        if (amount > 0)
                        {
                            var s = new service();
                            s.computer_id = id;
                            s.service_name = cbFoods.SelectedValue.ToString();
                            s.quantity = Convert.ToInt32(txtAmount.Text);
                            s.total = u.price * Convert.ToInt32(txtAmount.Text);
                            u.quantity -= s.quantity;
                            dc.services.InsertOnSubmit(s);
                            dc.SubmitChanges();
                            amount = 0;
                            txtAmount.Text = "0";
                        }
                        else
                        {
                            MessageBox.Show("Amount must be more than 0", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        var m = dc.services.FirstOrDefault(x => x.computer_id == id && x.service_name == cbFoods.SelectedValue.ToString());
                        if (m != null)
                        {
                            if (m.quantity + Convert.ToInt32(txtAmount.Text) >= 0)
                            {
                                m.quantity += Convert.ToInt32(txtAmount.Text);
                                m.total += u.price * Convert.ToInt32(txtAmount.Text);
                                u.quantity -= amount;
                                dc.SubmitChanges();
                                amount = 0;
                                txtAmount.Text = "0";
                            }
                            else
                            {
                                MessageBox.Show("This user orders only " + m.quantity + " items", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            if (amount>0)
                            {
                                var s = new service();
                                s.computer_id = id;
                                s.service_name = cbFoods.SelectedValue.ToString();
                                s.quantity = Convert.ToInt32(txtAmount.Text);
                                s.total = u.price * Convert.ToInt32(txtAmount.Text);
                                dc.services.InsertOnSubmit(s);
                                u.quantity -= s.quantity;
                                dc.SubmitChanges();
                                amount = 0;
                                txtAmount.Text = "0";
                            }
                            else
                            {
                                MessageBox.Show("Amount must be more than 0", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //m.quantity += Convert.ToInt32(txtAmount.Text);
                            //m.total = u.price * Convert.ToInt32(txtAmount.Text);
                            //dc.services.InsertOnSubmit(m);
                            //dc.SubmitChanges();
                        }
                    }
                    dgvService.DataSource = dc.getServiceMoney(Convert.ToInt32(row.Cells[0].Value.ToString()));

                }
                else
                {
                    MessageBox.Show("There are only " + u.quantity + " items remain!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("This PC is not in used!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int v = dc.getComputerStatus(Convert.ToInt32(row.Cells[0].Value.ToString()));
            if (v == 1)
            {
                var f = dc.computer_status.FirstOrDefault(x => x.computer_id == Convert.ToInt32(row.Cells[0].Value.ToString()));
                f.service_charge = dc.getTotalServiceMoney(f.computer_id);
                dc.SubmitChanges();
                DisplayAll();
                var k = new log();
                k.timenow = DateTime.Now;
                k.computer_name = f.name;
                k.notice = "Order service";
                dc.logs.InsertOnSubmit(k);
                dc.SubmitChanges();
                dgvLog.DataSource = dc.getLog();
                dgvLog.Sort(dgvLog.Columns[0], ListSortDirection.Descending);
            }
            else
            {
                MessageBox.Show("This PC is not in used!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvAllCom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvAllCom.CurrentRow;
            int v = dc.getComputerStatus(Convert.ToInt32(row.Cells[0].Value.ToString()));
            if (v == 1)
            {
                dgvService.DataSource = dc.getServiceMoney(Convert.ToInt32(row.Cells[0].Value.ToString()));
            }
            else
            {
                dgvService.DataSource = null;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvService.CurrentRow != null)
            {
                if (MessageBox.Show("Do you want to delete?", "INFORMATION",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataGridViewRow row = dgvService.CurrentRow;
                    int v = Convert.ToInt32(row.Cells[0].Value);
                    dc.deleteS(v);
                    DataGridViewRow row2 = dgvAllCom.CurrentRow;
                    int ev = dc.getComputerStatus(Convert.ToInt32(row2.Cells[0].Value.ToString()));
                    dgvService.DataSource = dc.getServiceMoney(Convert.ToInt32(row2.Cells[0].Value.ToString()));
                }
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
                this.Dispose();
                dc.deleteComputerStatus();
                dc.deleteService();
            dc.deletelog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var k = dc.computer_status.FirstOrDefault(x => x.status == 1);
            if (k == null)
            {
                this.Dispose();
                dc.deleteComputerStatus();
                dc.deleteService();
                dc.deletelog();
                frmLogin f = new frmLogin();
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("You can not exit this time!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

    }
}
