﻿using System;
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
    public partial class frmAdmin : Form
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public string imgLink;
        public string nameAdmin;
        public string acc;
        public int checkrole;
        public frmAdmin()
        {
            InitializeComponent();

        }
        public frmAdmin(string e, string f, string a, int cr)
        {
            InitializeComponent();

            imgLink = e;
            nameAdmin = f;
            acc = a;
            checkrole = cr;
        }
        private void frmAdmin_Leave(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void hover()
        {
            this.BackColor = System.Drawing.SystemColors.GrayText;
        }
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Rectangle r = new Rectangle(0, 0, ptbAdminAvatar.Width, ptbAdminAvatar.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ptbAdminAvatar.Width - 3, ptbAdminAvatar.Height - 3);
            Region rg = new Region(gp);
            ptbAdminAvatar.Region = rg;
            ptbAdminAvatar.ImageLocation = imgLink;
            lbNameAdmin.Text = nameAdmin;
            var u = dc.users.FirstOrDefault(x => x.account == acc); ;
            int role_id = u.role_id;

            if (role_id == 1)
            {
                lbPosition.Text = "Adminstrator";
            }
            else
            {
                lbPosition.Text = "Inventory Manager";
            }
            frmDashbroad fD = new frmDashbroad();
            fD.TopLevel = false;
            Size s = pnlMain.Size;
            pnlMain.Controls.Clear();
            fD.Size = s;
            fD.Dock = DockStyle.Fill;
            fD.Show();
            pnlMain.Controls.Add(fD);
        }

        private void ptbAdminAvatar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmFood frmfood = new frmFood(nameAdmin);
            frmfood.TopLevel = false;
            Size s = pnlMain.Size;
            pnlMain.Controls.Clear();
            frmfood.Size = s;
            frmfood.Dock = DockStyle.Fill;
            frmfood.Show();
            pnlMain.Controls.Add(frmfood);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Do you want to exit?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
            
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            //hover();
            //button3.BackColor = System.Drawing.SystemColors.GrayText;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            //button3.BackColor = System.Drawing.SystemColors.AppWorkspace;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmCommit fc = new frmCommit();
            fc.TopLevel = false;
            Size s = pnlMain.Size;
            pnlMain.Controls.Clear();
            fc.Size = s;
            fc.Dock = DockStyle.Fill;
            fc.Show();
            pnlMain.Controls.Add(fc);
        }

        private void btnDashbroad_Click(object sender, EventArgs e)
        {
            frmDashbroad fD = new frmDashbroad();
            fD.TopLevel = false;
            Size s = pnlMain.Size;
            pnlMain.Controls.Clear();
            fD.Size = s;
            fD.Dock = DockStyle.Fill;
            fD.Show();
            pnlMain.Controls.Add(fD);
        }

        private void btnComputer_Click(object sender, EventArgs e)
        {
            frmComputer frmc = new frmComputer(nameAdmin);
            frmc.TopLevel = false;
            Size s = pnlMain.Size;
            pnlMain.Controls.Clear();
            frmc.Size = s;
            frmc.Dock = DockStyle.Fill;
            frmc.Show();
            pnlMain.Controls.Add(frmc);
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            //frmArea frmar = new frmArea();
            //frmar.TopLevel = false;
            //Size s = pnlMain.Size;
            //pnlMain.Controls.Clear();
            //frmar.Size = s;
            //frmar.Dock = DockStyle.Fill;
            //frmar.Show();
            //pnlMain.Controls.Add(frmar);
            frmLogin f = new frmLogin();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            frmUser fu = new frmUser(checkrole);
            fu.TopLevel = false;
            Size s = pnlMain.Size;
            pnlMain.Controls.Clear();
            fu.Size = s;
            fu.Dock = DockStyle.Fill;
            fu.Show();
            pnlMain.Controls.Add(fu);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbHour.Text = DateTime.Now.ToLongTimeString();
            lbDay.Text = DateTime.Now.ToLongDateString();
        }
    }
}
