
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
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // patientsDataGrid
            // 
            this.patientsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientsDataGrid.Location = new System.Drawing.Point(23, 13);
            this.patientsDataGrid.Name = "patientsDataGrid";
            this.patientsDataGrid.RowHeadersWidth = 51;
            this.patientsDataGrid.RowTemplate.Height = 24;
            this.patientsDataGrid.Size = new System.Drawing.Size(752, 361);
            this.patientsDataGrid.TabIndex = 0;
            // 
            // PatientCRUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.patientsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PatientCRUD";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView patientsDataGrid;
    }
}