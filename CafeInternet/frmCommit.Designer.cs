﻿
namespace CafeInternet
{
    partial class frmCommit
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvCommit = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommit)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.875F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.125F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvCommit
            // 
            this.dgvCommit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCommit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCommit.Location = new System.Drawing.Point(0, 100);
            this.dgvCommit.Name = "dgvCommit";
            this.dgvCommit.ReadOnly = true;
            this.dgvCommit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommit.Size = new System.Drawing.Size(800, 350);
            this.dgvCommit.TabIndex = 1;
            // 
            // frmCommit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvCommit);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCommit";
            this.Text = "frmCommit";
            this.Load += new System.EventHandler(this.frmCommit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvCommit;
    }
}