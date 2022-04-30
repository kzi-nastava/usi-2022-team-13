
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class ExaminationRequests
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
            this.denyButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.requestsDataGrid = new System.Windows.Forms.DataGridView();
            this.requestIdBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // denyButton
            // 
            this.denyButton.Location = new System.Drawing.Point(393, 326);
            this.denyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.denyButton.Name = "denyButton";
            this.denyButton.Size = new System.Drawing.Size(68, 19);
            this.denyButton.TabIndex = 8;
            this.denyButton.Text = "DENY";
            this.denyButton.UseVisualStyleBackColor = true;
            this.denyButton.Click += new System.EventHandler(this.denyButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(315, 326);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(58, 19);
            this.acceptButton.TabIndex = 7;
            this.acceptButton.Text = "ACCEPT";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // requestsDataGrid
            // 
            this.requestsDataGrid.AllowUserToAddRows = false;
            this.requestsDataGrid.AllowUserToDeleteRows = false;
            this.requestsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requestsDataGrid.Location = new System.Drawing.Point(18, 18);
            this.requestsDataGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.requestsDataGrid.Name = "requestsDataGrid";
            this.requestsDataGrid.ReadOnly = true;
            this.requestsDataGrid.RowHeadersWidth = 51;
            this.requestsDataGrid.RowTemplate.Height = 24;
            this.requestsDataGrid.Size = new System.Drawing.Size(564, 293);
            this.requestsDataGrid.TabIndex = 5;
            // 
            // requestIdBox
            // 
            this.requestIdBox.Location = new System.Drawing.Point(203, 326);
            this.requestIdBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.requestIdBox.Name = "requestIdBox";
            this.requestIdBox.Size = new System.Drawing.Size(91, 20);
            this.requestIdBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 328);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Reqest ID:";
            // 
            // ExaminationRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.requestIdBox);
            this.Controls.Add(this.denyButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.requestsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ExaminationRequests";
            this.Text = "EaminationRequests";
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button denyButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.DataGridView requestsDataGrid;
        private System.Windows.Forms.TextBox requestIdBox;
        private System.Windows.Forms.Label label1;
    }
}