
namespace HealthCareSystem.GUI.SecretaryFunctionalities
{
    partial class DaysOffRequestsForm
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
            this.requestsDataGrid = new System.Windows.Forms.DataGridView();
            this.denyButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // requestsDataGrid
            // 
            this.requestsDataGrid.AllowUserToAddRows = false;
            this.requestsDataGrid.AllowUserToDeleteRows = false;
            this.requestsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requestsDataGrid.Location = new System.Drawing.Point(18, 20);
            this.requestsDataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.requestsDataGrid.Name = "requestsDataGrid";
            this.requestsDataGrid.ReadOnly = true;
            this.requestsDataGrid.RowHeadersWidth = 51;
            this.requestsDataGrid.RowTemplate.Height = 24;
            this.requestsDataGrid.Size = new System.Drawing.Size(564, 293);
            this.requestsDataGrid.TabIndex = 11;
            // 
            // denyButton
            // 
            this.denyButton.Location = new System.Drawing.Point(303, 326);
            this.denyButton.Margin = new System.Windows.Forms.Padding(2);
            this.denyButton.Name = "denyButton";
            this.denyButton.Size = new System.Drawing.Size(75, 19);
            this.denyButton.TabIndex = 13;
            this.denyButton.Text = "DENY";
            this.denyButton.UseVisualStyleBackColor = true;
            this.denyButton.Click += new System.EventHandler(this.denyButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(225, 326);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(65, 19);
            this.acceptButton.TabIndex = 12;
            this.acceptButton.Text = "ACCEPT";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // DaysOffRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.denyButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.requestsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DaysOffRequestsForm";
            this.Text = "DaysOffRequestsForm";
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView requestsDataGrid;
        private System.Windows.Forms.Button denyButton;
        private System.Windows.Forms.Button acceptButton;
    }
}