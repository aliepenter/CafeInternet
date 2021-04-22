
namespace CafeInternet
{
    partial class frmViewer
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crpView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(800, 450);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // crpView
            // 
            this.crpView.ActiveViewIndex = -1;
            this.crpView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crpView.Cursor = System.Windows.Forms.Cursors.Default;
            this.crpView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crpView.Location = new System.Drawing.Point(0, 0);
            this.crpView.Name = "crpView";
            this.crpView.Size = new System.Drawing.Size(800, 450);
            this.crpView.TabIndex = 1;
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crpView);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmViewer";
            this.Text = "frmViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crpView;
    }
}