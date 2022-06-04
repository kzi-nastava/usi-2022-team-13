
namespace HealthCareSystem.Core.GUI
{
    partial class PatientCRUDForm
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
            this.patientsDataGrid = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.blockButton = new System.Windows.Forms.Button();
            this.urgentButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // patientsDataGrid
            // 
            this.patientsDataGrid.AllowUserToAddRows = false;
            this.patientsDataGrid.AllowUserToDeleteRows = false;
            this.patientsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientsDataGrid.Location = new System.Drawing.Point(23, 13);
            this.patientsDataGrid.Name = "patientsDataGrid";
            this.patientsDataGrid.ReadOnly = true;
            this.patientsDataGrid.RowHeadersWidth = 51;
            this.patientsDataGrid.RowTemplate.Height = 24;
            this.patientsDataGrid.Size = new System.Drawing.Size(752, 361);
            this.patientsDataGrid.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(160, 398);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(82, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "ADD";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(261, 398);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(74, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "EDIT";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(356, 398);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(90, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // blockButton
            // 
            this.blockButton.Location = new System.Drawing.Point(465, 398);
            this.blockButton.Name = "blockButton";
            this.blockButton.Size = new System.Drawing.Size(82, 23);
            this.blockButton.TabIndex = 4;
            this.blockButton.Text = "BLOCK";
            this.blockButton.UseVisualStyleBackColor = true;
            this.blockButton.Click += new System.EventHandler(this.blockButton_Click);
            // 
            // urgentButton
            // 
            this.urgentButton.Location = new System.Drawing.Point(563, 398);
            this.urgentButton.Name = "urgentButton";
            this.urgentButton.Size = new System.Drawing.Size(82, 23);
            this.urgentButton.TabIndex = 5;
            this.urgentButton.Text = "URGENT";
            this.urgentButton.UseVisualStyleBackColor = true;
            this.urgentButton.Click += new System.EventHandler(this.urgentButton_Click);
            // 
            // PatientCRUDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.urgentButton);
            this.Controls.Add(this.blockButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.patientsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PatientCRUDForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView patientsDataGrid;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button blockButton;
        private System.Windows.Forms.Button urgentButton;
    }
}