
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class OutOfStockForm
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
            this.requestButton = new System.Windows.Forms.Button();
            this.requestsDataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.amountBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountBox)).BeginInit();
            this.SuspendLayout();
            // 
            // requestButton
            // 
            this.requestButton.Location = new System.Drawing.Point(476, 402);
            this.requestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(91, 23);
            this.requestButton.TabIndex = 10;
            this.requestButton.Text = "CHOOSE";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.requestButton_Click);
            // 
            // requestsDataGrid
            // 
            this.requestsDataGrid.AllowUserToAddRows = false;
            this.requestsDataGrid.AllowUserToDeleteRows = false;
            this.requestsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requestsDataGrid.Location = new System.Drawing.Point(24, 24);
            this.requestsDataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.requestsDataGrid.Name = "requestsDataGrid";
            this.requestsDataGrid.ReadOnly = true;
            this.requestsDataGrid.RowHeadersWidth = 51;
            this.requestsDataGrid.RowTemplate.Height = 24;
            this.requestsDataGrid.Size = new System.Drawing.Size(752, 361);
            this.requestsDataGrid.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Amount:";
            // 
            // amountBox
            // 
            this.amountBox.Location = new System.Drawing.Point(314, 403);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(120, 22);
            this.amountBox.TabIndex = 12;
            // 
            // OutOfStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.amountBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.requestsDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OutOfStockForm";
            this.Text = "OutOfStockForm";
            ((System.ComponentModel.ISupportInitialize)(this.requestsDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amountBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button requestButton;
        private System.Windows.Forms.DataGridView requestsDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown amountBox;
    }
}