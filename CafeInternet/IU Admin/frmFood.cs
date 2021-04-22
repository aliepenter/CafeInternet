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
    public partial class frmFood : Form
    {
        string imgLocation = "";
        int position;
        public string checkrole;
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmFood()
        {
            InitializeComponent();
            Rectangle r = new Rectangle(0, 0, ptbImageFood.Width, ptbImageFood.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ptbImageFood.Width - 3, ptbImageFood.Height - 3);
            Region rg = new Region(gp);
            ptbImageFood.Region = rg;

        }
        public frmFood(string c)
        {
            InitializeComponent();
            checkrole = c;
        }
        private void DisplayFoodType()
        {
            //lấy danh sách phòng ban
            var ft = from n in dc.food_types
                     select new
                     {
                         id = n.entity_id,
                         name = n.name
                     };
            comboBox1.DataSource = ft;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }
        //Phương thức hiển thị dữ liệu lên lưới
        private void DisplayFood()
        {
            //truy vấn lấy các thông tin cần thiết trong bảng Employee
            var food = from f in dc.foods
                       select new
                       {
                           Id = f.entity_id,
                           Name = f.name,
                           Price = f.price,
                           Quantity = f.quantity,
                           Food_Type = f.food_type_id == 1 ? "Đồ ăn" : f.food_type_id == 2 ? "Đồ uống đóng chai" : "Đồ uống pha chế",
                           Image = f.image
                       };
            //hiển thị lên lưới
            dgvShowFood.DataSource = food;
            DisplayFoodDetail();
        }
        private void DisplayFoodDetail()
        {
            string check = "";
            //nếu dòng hiện tại trên lưới khác null
            if (dgvShowFood.CurrentRow != null)
            {
                DataGridViewRow row = dgvShowFood.CurrentRow;
                txtName.Text = row.Cells[1].Value.ToString();
                txtPrice.Text = row.Cells[2].Value.ToString();
                txtQuantity.Text = row.Cells[3].Value.ToString();
                //txtImage.Text = row.Cells[5].Value.ToString();
                imgLocation = row.Cells[5].Value.ToString();
                ptbImageFood.ImageLocation = imgLocation;
                check = row.Cells[4].Value.ToString();
                if (check == "Đồ ăn")
                {
                    comboBox1.SelectedValue = 1;
                }
                else if (check == "Đồ uống đóng chai")
                {
                    comboBox1.SelectedValue = 2;
                }
                else
                {
                    comboBox1.SelectedValue = 3;
                }
                position = dgvShowFood.CurrentRow.Index;
                //edit = true;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayFoodType();
            DisplayFood();

        }

        private void frmFood_Load(object sender, EventArgs e)
        {
            DisplayFoodType();
            DisplayFood();
        }

        private void dgvShowFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayFoodDetail();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtReport.Text.Length != 0)
            {
                if (txtName.Text.Length != 0 && txtPrice.Text.Length != 0 && txtQuantity.Text.Length != 0)
                {
                    var u = dc.foods.FirstOrDefault(x => x.name == txtName.Text);
                    if (u == null)
                    {
                        if (Convert.ToDouble(txtPrice.Text) >= 0 && Convert.ToInt32(txtQuantity.Text) >= 0)
                        {
                            var f = new food();
                            f.name = txtName.Text;
                            f.price = Convert.ToDouble(txtPrice.Text);
                            f.quantity = Convert.ToInt32(txtQuantity.Text);
                            f.food_type_id = Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1;
                            f.image = imgLocation;
                            dc.foods.InsertOnSubmit(f);

                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtReport.Text;
                            k.performer = checkrole;
                            k.activity = "Add new food";
                            k.type = 1;
                            dc.reports.InsertOnSubmit(k);
                            dc.SubmitChanges();
                            DisplayFood();
                            txtReport.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Price and Quantity must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        if (u.food_type_id == Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1)
                        {
                            if (u.price == Convert.ToDouble(txtPrice.Text))
                            {
                                if (Convert.ToDouble(txtPrice.Text) >= 0)
                                {
                                    if (u.quantity + Convert.ToInt32(txtQuantity.Text) >= 0)
                                    {
                                        u.quantity += Convert.ToInt32(txtQuantity.Text);
                                        u.image = imgLocation;
                                        var k = new report();
                                        k.date = DateTime.Today;
                                        k.time = DateTime.Now;
                                        k.information = txtReport.Text;
                                        k.performer = checkrole;
                                        k.activity = "Add Quantity";
                                        k.type = 1;
                                        dc.reports.InsertOnSubmit(k);
                                        dc.SubmitChanges();
                                        DisplayFood();
                                        txtReport.Text = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Quantity must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Price must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("You can only update price for this item!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(txtPrice.Text) >= 0)
                            {
                                var n = dc.foods.FirstOrDefault(x => x.name == txtName.Text && x.food_type_id == Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1);
                                if (n != null)
                                {
                                    if (n.quantity + Convert.ToInt32(txtQuantity.Text) >= 0)
                                    {
                                        n.quantity += Convert.ToInt32(txtQuantity.Text);
                                        n.image = imgLocation;
                                        var k = new report();
                                        k.date = DateTime.Today;
                                        k.time = DateTime.Now;
                                        k.information = txtReport.Text;
                                        k.performer = checkrole;
                                        k.activity = "Add Quantity";
                                        k.type = 1;
                                        dc.reports.InsertOnSubmit(k);
                                        dc.SubmitChanges();
                                        DisplayFood();
                                        txtReport.Text = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Quantity must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    
                                }
                                else
                                {
                                    var f = new food();
                                    f.name = txtName.Text;
                                    f.price = Convert.ToDouble(txtPrice.Text);
                                    f.quantity = Convert.ToInt32(txtQuantity.Text);
                                    f.food_type_id = Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1;
                                    f.image = imgLocation;
                                    dc.foods.InsertOnSubmit(f);
                                    var k = new report();
                                    k.date = DateTime.Today;
                                    k.time = DateTime.Now;
                                    k.information = txtReport.Text;
                                    k.performer = checkrole;
                                    k.activity = "Add new food";
                                    k.type = 1;
                                    dc.reports.InsertOnSubmit(k);
                                    dc.SubmitChanges();
                                    DisplayFood();
                                    txtReport.Text = "";
                                }

                            }
                            else
                            {
                                MessageBox.Show("Price must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtReport.Text.Length != 0)
            {
                if (dgvShowFood.CurrentRow != null)
                {
                    if (MessageBox.Show("Do you want to delete?", "INFORMATION",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var f = dc.foods.FirstOrDefault(x => x.name ==
                        txtName.Text);
                        if (f != null)
                        {
                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtReport.Text;
                            k.performer = checkrole;
                            k.activity = "Delete food";
                            k.type = 1;
                            dc.reports.InsertOnSubmit(k);
                            dc.foods.DeleteOnSubmit(f);
                            dc.SubmitChanges();
                            DisplayFood();
                            txtReport.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You must commit the activity!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtReport.Text.Length != 0)
            {
                if (txtName.Text.Length != 0 && txtPrice.Text.Length != 0 && txtQuantity.Text.Length != 0)
                {
                    if (Convert.ToDouble(txtPrice.Text) >= 0 && Convert.ToInt32(txtQuantity.Text) >= 0)
                    {
                        var u = dc.foods.FirstOrDefault(x => x.name == txtName.Text && x.price == Convert.ToDouble(txtPrice.Text) && x.quantity == Convert.ToInt32(txtQuantity.Text) && x.food_type_id == Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1 && x.image == imgLocation);
                        if (u == null)
                        {
                            DataGridViewRow row = dgvShowFood.CurrentRow;
                            int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                            var f = dc.foods.FirstOrDefault(x => x.entity_id == id);
                            f.name = txtName.Text;
                            f.price = Convert.ToDouble(txtPrice.Text);
                            f.quantity = Convert.ToInt32(txtQuantity.Text);
                            f.food_type_id = Convert.ToInt32(comboBox1.SelectedIndex.ToString()) + 1;
                            f.image = imgLocation;
                            var k = new report();
                            k.date = DateTime.Today;
                            k.time = DateTime.Now;
                            k.information = txtReport.Text;
                            k.performer = checkrole;
                            k.activity = "Update food";
                            k.type = 1;
                            dc.reports.InsertOnSubmit(k);
                            dc.SubmitChanges();
                            DisplayFood();
                            txtReport.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Nothing change!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Price and Quantity must be equal or more than 0!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imgLocation = op.FileName;
                    ptbImageFood.ImageLocation = imgLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                var food = from f in dc.foods.Where(p => p.name.Contains(txtSearch.Text))
                           select new
                           {
                               Id = f.entity_id,
                               Name = f.name,
                               Price = f.price,
                               Quantity = f.quantity,
                               Food_Type = f.food_type_id == 1 ? "Đồ ăn" : f.food_type_id == 2 ? "Đồ uống đóng chai" : "Đồ uống pha chế",
                               Image = f.image,
                           };
                //hiển thị lên lưới
                dgvShowFood.DataSource = food;
                DisplayFoodDetail();
            }
            else
            {
                DisplayFoodType();
                DisplayFood();
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ('-') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ('.') && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           ReportAdmin.crpFood c = new ReportAdmin.crpFood();
            var food = dc.foods.Select(f => new
            {
                Entity_id = f.entity_id,
                Name = f.name,
                Price = f.price,
                Quantity = f.quantity,
                Food_type_id = f.food_type_id == 1 ? "Foods" : f.food_type_id == 2 ? "Bottled drinks" : "Concoction drinks"
            }).ToList();
            c.SetDataSource(food);
            frmViewer fv = new frmViewer();
            fv.crpView.ReportSource = c;
            fv.crpView.Refresh();
            fv.Show();
        }
    }
}
