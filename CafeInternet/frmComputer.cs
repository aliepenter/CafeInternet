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
    public partial class frmComputer : Form
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        int position;
        public string checkrole;
        public frmComputer()
        {
            InitializeComponent();
            Rectangle r = new Rectangle(0, 0, ptbComputer.Width, ptbComputer.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ptbComputer.Width - 3, ptbComputer.Height - 3);
            Region rg = new Region(gp);
            ptbComputer.Region = rg;
        }
        public frmComputer(string c)
        {
            InitializeComponent();
            checkrole = c;
        }
        private void DisplayArea()
        {
            var ar = from n in dc.areas
                     select new
                     {
                         id = n.entity_id,
                         name = n.name,
                         price = n.price
                     };
            cbArea.DataSource = ar;
            cbArea.DisplayMember = "name";
            cbArea.ValueMember = "id";
        }
        private void DisplayComputer()
        {
            var c = from f in dc.computers
                    select new
                    {
                        Id = f.entity_id,
                        Name = f.name,
                        Area = f.area_id,
                        Status = f.status == 0 ? "GOOD" : "BREAK",
                        Total_Used_Times = f.total_used_time,
                        Profit = f.profit
                    };

            dgvShowComputer.DataSource = c;
            DisplayComputerDetail();
        }
        private void DisplayComputerDetail()
        {
            if (dgvShowComputer.CurrentRow != null)
            {
                DataGridViewRow row = dgvShowComputer.CurrentRow;
                txtName.Text = row.Cells[1].Value.ToString();
                //txtPrice.Text = row.Cells[2].Value.ToString();
                txtUsedTime.Text = row.Cells[4].Value.ToString();
                //txtImage.Text = row.Cells[5].Value.ToString();
                txtProfit.Text = row.Cells[5].Value.ToString();
                cbArea.SelectedValue = row.Cells[2].Value;
                position = dgvShowComputer.CurrentRow.Index;
                txtPrice.ReadOnly = true;
                txtProfit.ReadOnly = true;
                txtUsedTime.ReadOnly = true;
                var u = dc.areas.FirstOrDefault(x => x.entity_id == Convert.ToInt32(row.Cells[2].Value.ToString()));
                txtPrice.Text = u.price.ToString();
                dgvShowComputer.Sort(dgvShowComputer.Columns[1], ListSortDirection.Ascending);
            }
        }
        private void frmComputer_Load(object sender, EventArgs e)
        {
            DisplayArea();
            DisplayComputer();
        }

        private void dgvShowComputer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayComputerDetail();
        }
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (txtReport.Text.Length != 0)
            {
                if (txtName.Text.Length != 0)
                {
                    var u = dc.computers.FirstOrDefault(x => x.name == txtName.Text);
                    if (u == null)
                    {
                        var f = new computer();
                        //gán giá trị
                        f.name = txtName.Text;
                        f.area_id = Convert.ToInt32(cbArea.SelectedIndex.ToString()) + 1;
                        var k = new report();
                        k.date = DateTime.Today;
                        k.time = DateTime.Now;
                        k.information = txtReport.Text;
                        k.performer = checkrole;
                        k.activity = "Add new computer";
                        k.type = 2;
                        dc.reports.InsertOnSubmit(k);
                        dc.computers.InsertOnSubmit(f);
                        dc.SubmitChanges();
                        DisplayComputer();
                        txtReport.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("This name has already existed!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("You must fill all the textbox!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (txtReport.Text.Length != 0)
            {
                if (txtName.Text.Length != 0)
                {
                    var u = dc.computers.FirstOrDefault(x => x.name == txtName.Text);
                    if (u == null)
                    {
                        DataGridViewRow row = dgvShowComputer.CurrentRow;
                        int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                        var f = dc.computers.FirstOrDefault(x => x.entity_id == id);
                        f.name = txtName.Text;
                        f.area_id = Convert.ToInt32(cbArea.SelectedIndex.ToString()) + 1;
                        //f.image = txtImage.Text;
                        var k = new report();
                        k.date = DateTime.Today;
                        k.time = DateTime.Now;
                        k.information = txtReport.Text;
                        k.performer = checkrole;
                        k.activity = "Update computer";
                        k.type = 2;
                        dc.reports.InsertOnSubmit(k);
                        dc.SubmitChanges();
                        DisplayComputer();
                        txtReport.Text = "";
                    }
                    else
                    {
                        if (u.area_id == Convert.ToInt32(cbArea.SelectedIndex.ToString()) + 1)
                        {
                            MessageBox.Show("Nothing change!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DataGridViewRow row = dgvShowComputer.CurrentRow;
                            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                            var f = dc.computers.FirstOrDefault(x => x.entity_id == id);
                            f.area_id = Convert.ToInt32(cbArea.SelectedIndex.ToString()) + 1;
                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtReport.Text;
                            k.performer = checkrole;
                            k.activity = "Update area of computer";
                            k.type = 2;
                            dc.reports.InsertOnSubmit(k);
                            //f.image = txtImage.Text;
                            dc.SubmitChanges();
                            DisplayComputer();
                            txtReport.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must fill all the textbox!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (txtReport.Text.Length != 0)
            {
                if (dgvShowComputer.CurrentRow != null)
                {
                    if (MessageBox.Show("Do you want to delete?", "Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //tìm nhân viên có mã như trên form
                        var f = dc.computers.FirstOrDefault(x => x.name ==
                        txtName.Text);
                        if (f != null)
                        {
                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtReport.Text;
                            k.performer = checkrole;
                            k.activity = "Delete computer";
                            k.type = 2;
                            dc.reports.InsertOnSubmit(k);
                            //xóa dữ liệu
                            dc.computers.DeleteOnSubmit(f);
                            //lưu
                            dc.SubmitChanges();
                            //hiển thị lại dữ liệu
                            DisplayComputer();
                            txtReport.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Can not find the data", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayArea();
            DisplayComputer();
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                if (Int32.TryParse(txtSearch.Text, out int value))
                {
                    int a = Convert.ToInt32(txtSearch.Text);
                    var com = from f in dc.computers.Where(p => p.name.Contains(txtSearch.Text) || p.profit >= a)
                              select new
                              {
                                  Id = f.entity_id,
                                  Name = f.name,
                                  Area = f.area_id,
                                  Status = f.status == 0 ? "GOOD" : "BREAK",
                                  Total_Used_Times = f.total_used_time,
                                  Profit = f.profit
                              };
                    //hiển thị lên lưới
                    dgvShowComputer.DataSource = com;
                    DisplayComputerDetail();
                }
                else
                {
                    var com = from f in dc.computers.Where(p => p.name.Contains(txtSearch.Text))
                              select new
                              {
                                  Id = f.entity_id,
                                  Name = f.name,
                                  Area = f.area_id,
                                  Status = f.status == 0 ? "GOOD" : "BREAK",
                                  Total_Used_Times = f.total_used_time,
                                  Profit = f.profit
                              };
                    //hiển thị lên lưới
                    dgvShowComputer.DataSource = com;
                    DisplayComputerDetail();
                }
            }
            else
            {
                DisplayArea();
                DisplayComputer();
            }
        }

        private void dgvShowComputer_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DisplayComputerDetail();
        }
        private void DisplayA()
        {
            var count = dc.get_price_area();
            dgvShowArea.DataSource = count.ToList();

        }
        private void DisplayADetail()
        {
            if (dgvShowArea.CurrentRow != null)
            {
                DataGridViewRow row2 = dgvShowArea.CurrentRow;
                txtNa.Text = row2.Cells[1].Value.ToString();
                txtPr.Text = row2.Cells[2].Value.ToString();
                txtNumofCom.Text = row2.Cells[3].Value.ToString();
                txtNumofCom.ReadOnly = true;
            }
        }
        private void tabComputer_Selecting(object sender, TabControlCancelEventArgs e)
        {
            DisplayA();
            DisplayADetail();
        }
        private void dgvShowArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayADetail();
        }
        private void tabComputer_Selected(object sender, TabControlEventArgs e)
        {
            DisplayArea();
            DisplayComputer();
            DisplayA();
            DisplayADetail();
        }
        private void txtPr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ('.') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddAr_Click_1(object sender, EventArgs e)
        {
            if (txtRe.Text.Length != 0)
            {
                if (txtNa.Text.Length != 0 && txtPr.Text.Length != 0)
                {
                    if (Convert.ToDouble(txtPr.Text) >= 0)
                    {
                        var u = dc.areas.FirstOrDefault(x => x.name == txtNa.Text);
                        if (u == null)
                        {
                            var a = new area();
                            //gán giá trị
                            a.name = txtNa.Text;
                            a.price = Convert.ToDouble(txtPr.Text);
                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtRe.Text;
                            k.performer = checkrole;
                            k.activity = "Add area";
                            k.type = 3;
                            dc.reports.InsertOnSubmit(k);
                            dc.areas.InsertOnSubmit(a);
                            dc.SubmitChanges();
                            DisplayA();
                            DisplayADetail();
                            txtRe.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("This name has already existed!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Price must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You must fill all the textbox!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateAr_Click_1(object sender, EventArgs e)
        {
            if (txtRe.Text.Length != 0)
            {
                if (txtNa.Text.Length != 0 && txtPr.Text.Length != 0)
                {
                    if (Convert.ToDouble(txtPr.Text) >= 0)
                    {
                        var u = dc.areas.FirstOrDefault(x => x.name == txtNa.Text);
                        if (u == null)
                        {
                            DataGridViewRow row = dgvShowArea.CurrentRow;
                            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                            var f = dc.areas.FirstOrDefault(x => x.entity_id == id);
                            f.name = txtNa.Text;
                            f.price = Convert.ToDouble(txtPr.Text);
                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtRe.Text;
                            k.performer = checkrole;
                            k.activity = "Update area";
                            k.type = 3;
                            dc.reports.InsertOnSubmit(k);
                            //f.image = txtImage.Text;
                            dc.SubmitChanges();
                            DisplayA();
                            DisplayADetail();
                            txtRe.Text = "";
                        }
                        else
                        {
                            if (u.price != Convert.ToDouble(txtPr.Text))
                            {
                                DataGridViewRow row = dgvShowArea.CurrentRow;
                                int kx = Convert.ToInt32(row.Cells[0].Value.ToString());
                                var f = dc.areas.FirstOrDefault(x => x.entity_id == kx);
                                f.name = txtNa.Text;
                                f.price = Convert.ToDouble(txtPr.Text);
                                var k = new report();
                                k.date = DateTime.Today;
                                k.time = DateTime.Now;
                                k.information = txtRe.Text;
                                k.performer = checkrole;
                                k.activity = "Update area";
                                k.type = 3;
                                dc.reports.InsertOnSubmit(k);
                                //f.image = txtImage.Text;
                                dc.SubmitChanges();
                                DisplayA();
                                DisplayADetail();
                                txtRe.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Nothing change!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Price must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You must fill all the textbox!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelAr_Click(object sender, EventArgs e)
        {
            if (txtRe.Text.Length != 0)
            {
                DataGridViewRow row = dgvShowArea.CurrentRow;
                int id = Convert.ToInt32(row.Cells[3].Value.ToString());
                if (id != 0)
                {
                    MessageBox.Show("You can't delete this area, this area contains computers!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    if (dgvShowArea.CurrentRow != null)
                    {
                        if (MessageBox.Show("Do you want to delete?", "Delete",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //tìm nhân viên có mã như trên form
                            var f = dc.areas.FirstOrDefault(x => x.name == txtNa.Text);
                            if (f != null)
                            {
                                var k = new report();
                                k.date = DateTime.Today;
                                k.time = DateTime.Now;
                                k.information = txtRe.Text;
                                k.performer = checkrole;
                                k.activity = "Delete area";
                                k.type = 3;
                                dc.reports.InsertOnSubmit(k);
                                dc.areas.DeleteOnSubmit(f);
                                dc.SubmitChanges();
                                DisplayA();
                                DisplayADetail();
                                txtRe.Text = "";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can not find the data", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DisplayArea();
            DisplayADetail();
        }

        private void btnSearchAr_Click(object sender, EventArgs e)
        {
            if (txtSearchAr.Text != "")
            {

                var count = dc.search_area(txtSearchAr.Text);


                //hiển thị lên lưới
                dgvShowArea.DataSource = count.ToList();
                DisplayADetail();
            }
            else
            {
                var count = dc.get_price_area();
                //hiển thị lên lưới
                dgvShowArea.DataSource = count.ToList();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmViewer fv = new frmViewer();
            crpComputer c = new crpComputer();
            var com = dc.computers.Select(f => new
            {
                Entity_id = f.entity_id,
                Name = f.name,
                Status = f.status == 1 ? "Good" : "Break",
                Total_used_time = f.total_used_time,
                Profit = f.profit,
                Area_Id = f.area_id
            }).ToList();
            c.SetDataSource(com);
            fv.crpView.ReportSource = c;
            fv.crpView.Refresh();
            fv.Show();
        }

        private void btnPr_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Price", typeof(double));
            dt.Columns.Add("Number of Computer", typeof(int));
            foreach (DataGridViewRow dgv in dgvShowArea.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("area.xml");
            frmViewer fv = new frmViewer();
            crpArea1 ca = new crpArea1();
            ca.SetDataSource(ds);
            fv.crpView.ReportSource = ca;
            fv.crpView.Refresh();
            fv.Show();
        }
    }
}
