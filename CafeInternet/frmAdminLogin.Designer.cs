
namespace CafeInternet
{
    partial class frmAdminLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ptbIconApp = new System.Windows.Forms.PictureBox();
            this.btnInventor = new System.Windows.Forms.Button();
            this.btnShop = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.ptbAdminAvatar = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbAdminName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbIconApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAdminAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ptbIconApp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 269);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(23, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "application of the world";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(23, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "The best Cafe Internet Manage ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(3, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cafe Internet Manager V1.0";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lbAdminName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.ptbAdminAvatar);
            this.panel2.Controls.Add(this.btnLogOut);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnShop);
            this.panel2.Controls.Add(this.btnInventor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(245, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 269);
            this.panel2.TabIndex = 1;
            // 
            // ptbIconApp
            // 
            this.ptbIconApp.Image = global::CafeInternet.Properties.Resources.shop_cafe_22672;
            this.ptbIconApp.Location = new System.Drawing.Point(68, 12);
            this.ptbIconApp.Name = "ptbIconApp";
            this.ptbIconApp.Size = new System.Drawing.Size(100, 92);
            this.ptbIconApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbIconApp.TabIndex = 0;
            this.ptbIconApp.TabStop = false;
            // 
            // btnInventor
            // 
            this.btnInventor.Location = new System.Drawing.Point(11, 176);
            this.btnInventor.Name = "btnInventor";
            this.btnInventor.Size = new System.Drawing.Size(129, 38);
            this.btnInventor.TabIndex = 0;
            this.btnInventor.Text = "Manage Inventory ";
            this.btnInventor.UseVisualStyleBackColor = true;
            this.btnInventor.Click += new System.EventHandler(this.btnInventor_Click);
            // 
            // btnShop
            // 
            this.btnShop.Location = new System.Drawing.Point(145, 175);
            this.btnShop.Name = "btnShop";
            this.btnShop.Size = new System.Drawing.Size(129, 38);
            this.btnShop.TabIndex = 1;
            this.btnShop.Text = "Manage Shop";
            this.btnShop.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(145, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 38);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(11, 219);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(129, 38);
            this.btnLogOut.TabIndex = 3;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // ptbAdminAvatar
            // 
            this.ptbAdminAvatar.Location = new System.Drawing.Point(92, 12);
            this.ptbAdminAvatar.Name = "ptbAdminAvatar";
            this.ptbAdminAvatar.Size = new System.Drawing.Size(100, 92);
            this.ptbAdminAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbAdminAvatar.TabIndex = 4;
            this.ptbAdminAvatar.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Welcome back Admin";
            // 
            // lbAdminName
            // 
            this.lbAdminName.AutoSize = true;
            this.lbAdminName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdminName.Location = new System.Drawing.Point(157, 115);
            this.lbAdminName.Name = "lbAdminName";
            this.lbAdminName.Size = new System.Drawing.Size(0, 16);
            this.lbAdminName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(70, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "What do you want to do?";
            // 
            // frmAdminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 269);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAdminLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdminLogin";
            this.Load += new System.EventHandler(this.frmAdminLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbIconApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAdminAvatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ptbIconApp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbAdminName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox ptbAdminAvatar;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnShop;
        private System.Windows.Forms.Button btnInventor;
    }
}