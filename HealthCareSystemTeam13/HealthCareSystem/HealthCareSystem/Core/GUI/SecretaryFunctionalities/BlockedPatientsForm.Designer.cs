
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class BlockedPatientsForm
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
            this.blockedPatientsDataGrid = new System.Windows.Forms.DataGridView();
            this.unblockButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.blockedPatientsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // blockedPatientsDataGrid
            // 
            this.blockedPatientsDataGrid.AllowUserToAddRows = false;
            this.blockedPatientsDataGrid.AllowUserToDeleteRows = false;
            this.blockedPatientsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blockedPatientsDataGrid.Location = new System.Drawing.Point(25, 11);
            this.blockedPatientsDataGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.blockedPatientsDataGrid.Name = "blockedPatientsDataGrid";
            this.blockedPatientsDataGrid.ReadOnly = true;
            this.blockedPatientsDataGrid.RowHeadersWidth = 51;
            this.blockedPatientsDataGrid.RowTemplate.Height = 24;
            this.blockedPatientsDataGrid.Size = new System.Drawing.Size(546, 293);
            this.blockedPatientsDataGrid.TabIndex = 0;
            // 
            // unblockButton
            // 
            this.unblockButton.Location = new System.Drawing.Point(243, 310);
            this.unblockButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.unblockButton.Name = "unblockButton";
            this.unblockButton.Size = new System.Drawing.Size(94, 35);
            this.unblockButton.TabIndex = 1;
            this.unblockButton.Text = "UNBLOCK";
            this.unblockButton.UseVisualStyleBackColor = true;
            this.unblockButton.Click += new System.EventHandler(this.unblockButton_Click);
            // 
            // BlockedPatientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.unblockButton);
            this.Controls.Add(this.blockedPatientsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BlockedPatientsForm";
            this.Text = "BlockedPatients";
            ((System.ComponentModel.ISupportInitialize)(this.blockedPatientsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView blockedPatientsDataGrid;
        private System.Windows.Forms.Button unblockButton;
    }
}