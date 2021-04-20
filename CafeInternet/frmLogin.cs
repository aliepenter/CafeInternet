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
    public partial class frmLogin : Form
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public frmLogin()
        {
            InitializeComponent();
        }
        private void login()
        {
            var u = dc.users.FirstOrDefault(x => x.account == txtAccount.Text);
            if (u != null)
            {
                lbErrorAccount.Text = "";
                if (u.password == txtPassword.Text)
                {
                    lbErrorPassword.Text = "";
                    if (u.role_id != 1)
                    {
                        if (u.role_id == 2)
                        {
                            frmAdmin frm = new frmAdmin(u.image, u.name, u.account, u.role_id);
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            frmMain frm1 = new frmMain();
                            frm1.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        frmAdminLogin frmA = new frmAdminLogin(u.image, u.name, u.account, u.role_id);
                        frmA.Show();
                        this.Hide();
                    }
                }
                else lbErrorPassword.Text = "Mật khẩu không chính xác!";
            }
            else
            {
                lbErrorAccount.Text = "Tài khoản không tồn tại, vui lòng thử lại!";
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void lbErrorPassword_Leave(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void txtAccount_Enter(object sender, EventArgs e)
        {
            login();
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnCancel_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
