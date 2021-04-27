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
    public partial class frmAdminLogin : Form
    {
        public string imgLink;
        public string nameAdmin;
        public string acc;
        public int checkrole;
        public frmAdminLogin()
        {
            InitializeComponent();
        }
        public frmAdminLogin(string e, string n, string a, int ck)
        {
            InitializeComponent();
            Rectangle r = new Rectangle(0, 0, ptbIconApp.Width, ptbIconApp.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ptbIconApp.Width - 3, ptbIconApp.Height - 3);
            Region rg = new Region(gp);
            ptbIconApp.Region = rg;
            imgLink = e;
            nameAdmin = n;
            acc = a;
            checkrole = ck;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void frmAdminLogin_Load(object sender, EventArgs e)
        {
            Rectangle r1 = new Rectangle(0, 0, ptbAdminAvatar.Width, ptbAdminAvatar.Height);
            System.Drawing.Drawing2D.GraphicsPath gp1 = new System.Drawing.Drawing2D.GraphicsPath();
            gp1.AddEllipse(0, 0, ptbAdminAvatar.Width - 3, ptbAdminAvatar.Height - 3);
            Region rg1 = new Region(gp1);
            ptbAdminAvatar.Region = rg1;
            ptbAdminAvatar.ImageLocation = imgLink;
            lbAdminName.Text = nameAdmin;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            frmLogin f = new frmLogin();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnInventor_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdmin frm = new frmAdmin(imgLink, nameAdmin, acc, checkrole);
            frm.ShowDialog();
            this.Close();
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain frm = new frmMain(nameAdmin);
            frm.ShowDialog();
            this.Close();
        }
    }
}
