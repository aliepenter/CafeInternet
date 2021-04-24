
namespace CafeInternet
{
    partial class frmAll
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
            this.dgvAllCom = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllCom)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAllCom
            // 
            this.dgvAllCom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllCom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllCom.Location = new System.Drawing.Point(0, 0);
            this.dgvAllCom.Name = "dgvAllCom";
            this.dgvAllCom.ReadOnly = true;
            this.dgvAllCom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllCom.Size = new System.Drawing.Size(800, 450);
            this.dgvAllCom.TabIndex = 0;
            this.dgvAllCom.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllCom_CellDoubleClick);
            // 
            // frmAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvAllCom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAll";
            this.Text = "frmAll";
            this.Load += new System.EventHandler(this.frmAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllCom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllCom;
    }
}