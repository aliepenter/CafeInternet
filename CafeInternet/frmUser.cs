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
    public partial class frmUser : Form
    {
        public int checkrole;
        string imgLocation = "";
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmUser()
        {
            InitializeComponent();
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox1.Width - 3, pictureBox1.Height - 3);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;
        }


        public frmUser(int a)
        {
            InitializeComponent();
            checkrole = a;
        }
        private void DisplayRole()
        {
            var ar = from n in dc.roles
                     select new
                     {
                         id = n.entity_id,
                         name = n.name
                     };
            cbRole.DataSource = ar;
            cbRole.DisplayMember = "name";
            cbRole.ValueMember = "id";
        }
        private void DisplayUser()
        {
            var c = from f in dc.users
                    select new
                    {
                        Id = f.entity_id,
                        Account = f.account,
                        Password = f.password,
                        Name = f.name,
                        Role = f.role_id,
                        Image_Link = f.image
                    };

            dgvUser.DataSource = c;
            DisplayUserDetail();
        }
        private void DisplayUserDetail()
        {
            if (dgvUser.CurrentRow != null)
            {
                DataGridViewRow row = dgvUser.CurrentRow;
                txtAcc.Text = row.Cells[1].Value.ToString();
                //txtPrice.Text = row.Cells[2].Value.ToString();
                txtPass.Text = row.Cells[2].Value.ToString();
                //txtImage.Text = row.Cells[5].Value.ToString();
                txtNam.Text = row.Cells[3].Value.ToString();
                cbRole.SelectedValue = row.Cells[4].Value;
                imgLocation = row.Cells[5].Value.ToString();
                ptbAvatar.ImageLocation = imgLocation;
                txtImage.Text = imgLocation;
                txtPass.ReadOnly = true;
                //txtProfit.ReadOnly = true;
                //txtUsedTime.ReadOnly = true;
                //dgvUser.Sort(dgvUser.Columns[0], ListSortDirection.Ascending);
                Rectangle r = new Rectangle(0, 0, ptbAvatar.Width, ptbAvatar.Height);
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, ptbAvatar.Width - 3, ptbAvatar.Height - 3);
                Region rg = new Region(gp);
                ptbAvatar.Region = rg;
            }
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            DisplayRole();
            DisplayUser();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayUserDetail();
        }

        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (checkrole == 1)
            {
                if (txtAcc.Text.Length != 0 && txtPass.Text.Length != 0 && txtNam.Text.Length != 0 && txtImage.Text.Length != 0)
                {
                    var u = dc.users.FirstOrDefault(x => x.account == txtAcc.Text);
                    if (u == null)
                    {
                        var f = new user();
                        f.account = txtAcc.Text;
                        f.password = txtPass.Text;
                        f.name = txtNam.Text;
                        f.role_id = Convert.ToInt32(cbRole.SelectedIndex.ToString()) + 1;
                        f.image = imgLocation;
                        dc.users.InsertOnSubmit(f);
                        dc.SubmitChanges();
                        DisplayUser();
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
                MessageBox.Show("You are not adminstrator!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ptbAvatar.ImageLocation = imgLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkrole == 1)
            {
                if (txtAcc.Text.Length != 0 && txtPass.Text.Length != 0 && txtNam.Text.Length != 0 && txtImage.Text.Length != 0)
                {
                    var u = dc.users.FirstOrDefault(x => x.account == txtAcc.Text && x.password == txtPass.Text && x.name == txtNam.Text && x.role_id == Convert.ToInt32(cbRole.SelectedIndex.ToString()) + 1 && x.image == imgLocation);
                    if (u == null)
                    {
                        DataGridViewRow row = dgvUser.CurrentRow;
                        int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                        var f = dc.users.FirstOrDefault(x => x.entity_id == id);
                            f.account = txtAcc.Text;
                            f.name = txtNam.Text;
                            f.role_id = Convert.ToInt32(cbRole.SelectedIndex.ToString()) + 1;
                            f.image = imgLocation;
                            dc.SubmitChanges();
                            DisplayUser();   
                    }
                    else
                    {
                        MessageBox.Show("Nothing change!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You must fill all the textbox!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You are not adminstrator!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtPass_Click(object sender, EventArgs e)
        {
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {      
        }
    }
}
