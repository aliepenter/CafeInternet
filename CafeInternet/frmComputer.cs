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
        public frmComputer()
        {
            InitializeComponent();
            Rectangle r = new Rectangle(0, 0, ptbComputer.Width, ptbComputer.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ptbComputer.Width - 3, ptbComputer.Height - 3);
            Region rg = new Region(gp);
            ptbComputer.Region = rg;
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
                        Status = f.status == 0 ? "OFFLINE" : "ONLINE",
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
            var u = dc.computers.FirstOrDefault(x => x.name == txtName.Text);
            if (u == null)
            {
                var f = new computer();
                //gán giá trị
                f.name = txtName.Text;
                f.area_id = Convert.ToInt32(cbArea.SelectedIndex.ToString()) + 1;
                dc.computers.InsertOnSubmit(f);
                dc.SubmitChanges();
                DisplayComputer();
            }
            else
            {
                MessageBox.Show("This name has already existed!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
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
                dc.SubmitChanges();
                DisplayComputer();
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
                    //f.image = txtImage.Text;
                    dc.SubmitChanges();
                    DisplayComputer();
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
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
                        //xóa dữ liệu
                        dc.computers.DeleteOnSubmit(f);
                        //lưu
                        dc.SubmitChanges();
                        //hiển thị lại dữ liệu
                        DisplayComputer();
                    }
                }
            }
            else
            {
                MessageBox.Show("Can not find the data", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int a = Convert.ToInt32(txtSearch.Text);
                var com = from f in dc.computers.Where(p => p.name.Contains(txtSearch.Text) || p.profit >= a)
                          select new
                          {
                              Id = f.entity_id,
                              Name = f.name,
                              Area = f.area_id,
                              Status = f.status == 0 ? "OFFLINE" : "ONLINE",
                              Total_Used_Times = f.total_used_time,
                              Profit = f.profit
                          };
                //hiển thị lên lưới
                dgvShowComputer.DataSource = com;
                DisplayComputerDetail();
            }
            else
            {
                MessageBox.Show("Search box can't be null", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtNa.Text = row2.Cells[0].Value.ToString();
                txtPr.Text = row2.Cells[1].Value.ToString();
                txtNumofCom.Text = row2.Cells[2].Value.ToString();
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

        private void btnAddAr_Click(object sender, EventArgs e)
        {
            var u = dc.areas.FirstOrDefault(x => x.name == txtNa.Text);
            if (u == null)
            {
                var k = new area();
                //gán giá trị
                k.name = txtNa.Text;
                k.price = Convert.ToDouble(txtPr.Text);
                dc.areas.InsertOnSubmit(k);
                dc.SubmitChanges();
                DisplayA();
                DisplayADetail();
            }
            else
            {
                MessageBox.Show("This name has already existed!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabComputer_Selected(object sender, TabControlEventArgs e)
        {
            DisplayArea();
            DisplayComputer();
            DisplayA();
            DisplayADetail();
        }

        private void btnUpdateAr_Click(object sender, EventArgs e)
        {
            var u = dc.areas.FirstOrDefault(x => x.name == txtNa.Text);
            if (u == null)
            {
                DataGridViewRow row = dgvShowArea.CurrentRow;
                int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                var f = dc.areas.FirstOrDefault(x => x.entity_id == id);
                f.name = txtNa.Text;
                f.price = Convert.ToDouble(txtPr.Text); ;
                //f.image = txtImage.Text;
                dc.SubmitChanges();
                DisplayA();
                DisplayADetail();
            }
            else
            {
                if (u.price != Convert.ToDouble(txtPr.Text))
                {
                    DataGridViewRow row = dgvShowArea.CurrentRow;
                    int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                    var f = dc.areas.FirstOrDefault(x => x.entity_id == id);
                    f.name = txtNa.Text;
                    f.price = Convert.ToDouble(txtPr.Text); ;
                    //f.image = txtImage.Text;
                    dc.SubmitChanges();
                    DisplayA();
                    DisplayADetail();
                }
                else
                {
                    MessageBox.Show("Nothing change!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDelAr_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvShowArea.CurrentRow;
            int id = Convert.ToInt32(row.Cells[2].Value.ToString());
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
                            dc.areas.DeleteOnSubmit(f);
                            dc.SubmitChanges();
                            DisplayA();
                            DisplayADetail();
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
    }
}
