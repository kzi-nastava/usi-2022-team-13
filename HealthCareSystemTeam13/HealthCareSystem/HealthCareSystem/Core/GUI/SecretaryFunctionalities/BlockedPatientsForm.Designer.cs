
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
            this.blockedPatientsDataGrid.Location = new System.Drawing.Point(33, 14);
            this.blockedPatientsDataGrid.Name = "blockedPatientsDataGrid";
            this.blockedPatientsDataGrid.ReadOnly = true;
            this.blockedPatientsDataGrid.RowHeadersWidth = 51;
            this.blockedPatientsDataGrid.RowTemplate.Height = 24;
            this.blockedPatientsDataGrid.Size = new System.Drawing.Size(728, 361);
            this.blockedPatientsDataGrid.TabIndex = 0;
            // 
            // unblockButton
            // 
            this.unblockButton.Location = new System.Drawing.Point(324, 381);
            this.unblockButton.Name = "unblockButton";
            this.unblockButton.Size = new System.Drawing.Size(125, 43);
            this.unblockButton.TabIndex = 1;
            this.unblockButton.Text = "UNBLOCK";
            this.unblockButton.UseVisualStyleBackColor = true;
            this.unblockButton.Click += new System.EventHandler(this.unblockButton_Click);
            // 
            // BlockedPatients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.unblockButton);
            this.Controls.Add(this.blockedPatientsDataGrid);
            this.Name = "BlockedPatients";
            this.Text = "BlockedPatients";
            ((System.ComponentModel.ISupportInitialize)(this.blockedPatientsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView blockedPatientsDataGrid;
        private System.Windows.Forms.Button unblockButton;
    }
}