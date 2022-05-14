
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class UrgentExaminations
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
            this.chooseButton = new System.Windows.Forms.Button();
            this.examinationsDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.examinationsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // chooseButton
            // 
            this.chooseButton.Location = new System.Drawing.Point(327, 388);
            this.chooseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chooseButton.Name = "chooseButton";
            this.chooseButton.Size = new System.Drawing.Size(125, 43);
            this.chooseButton.TabIndex = 3;
            this.chooseButton.Text = "CHOOSE";
            this.chooseButton.UseVisualStyleBackColor = true;
            this.chooseButton.Click += new System.EventHandler(this.chooseButton_Click);
            // 
            // examinationsDataGrid
            // 
            this.examinationsDataGrid.AllowUserToAddRows = false;
            this.examinationsDataGrid.AllowUserToDeleteRows = false;
            this.examinationsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.examinationsDataGrid.Location = new System.Drawing.Point(36, 20);
            this.examinationsDataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.examinationsDataGrid.Name = "examinationsDataGrid";
            this.examinationsDataGrid.ReadOnly = true;
            this.examinationsDataGrid.RowHeadersWidth = 51;
            this.examinationsDataGrid.RowTemplate.Height = 24;
            this.examinationsDataGrid.Size = new System.Drawing.Size(728, 361);
            this.examinationsDataGrid.TabIndex = 2;
            // 
            // UrgentExaminations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chooseButton);
            this.Controls.Add(this.examinationsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UrgentExaminations";
            this.Text = "UrgentExaminations";
            ((System.ComponentModel.ISupportInitialize)(this.examinationsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button chooseButton;
        private System.Windows.Forms.DataGridView examinationsDataGrid;
    }
}