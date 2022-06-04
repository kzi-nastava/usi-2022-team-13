
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class ReferralLettersForm
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
            this.acceptButton = new System.Windows.Forms.Button();
            this.lettersDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.lettersDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(352, 404);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(82, 23);
            this.acceptButton.TabIndex = 5;
            this.acceptButton.Text = "ACCEPT";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // lettersDataGrid
            // 
            this.lettersDataGrid.AllowUserToAddRows = false;
            this.lettersDataGrid.AllowUserToDeleteRows = false;
            this.lettersDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lettersDataGrid.Location = new System.Drawing.Point(24, 23);
            this.lettersDataGrid.Name = "lettersDataGrid";
            this.lettersDataGrid.ReadOnly = true;
            this.lettersDataGrid.RowHeadersWidth = 51;
            this.lettersDataGrid.RowTemplate.Height = 24;
            this.lettersDataGrid.Size = new System.Drawing.Size(752, 361);
            this.lettersDataGrid.TabIndex = 4;
            // 
            // ReferralLettersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.lettersDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReferralLettersForm";
            this.Text = "ReferralLettersForm";
            ((System.ComponentModel.ISupportInitialize)(this.lettersDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.DataGridView lettersDataGrid;
    }
}