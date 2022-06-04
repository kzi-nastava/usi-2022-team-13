
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class TransferForm
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
            this.amountBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.requestButton = new System.Windows.Forms.Button();
            this.equipmentDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.amountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // amountBox
            // 
            this.amountBox.Location = new System.Drawing.Point(314, 404);
            this.amountBox.Name = "amountBox";
            this.amountBox.Size = new System.Drawing.Size(120, 22);
            this.amountBox.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Amount:";
            // 
            // requestButton
            // 
            this.requestButton.Location = new System.Drawing.Point(476, 403);
            this.requestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(91, 23);
            this.requestButton.TabIndex = 14;
            this.requestButton.Text = "CHOOSE";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.requestButton_Click);
            // 
            // equipmentDataGrid
            // 
            this.equipmentDataGrid.AllowUserToAddRows = false;
            this.equipmentDataGrid.AllowUserToDeleteRows = false;
            this.equipmentDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.equipmentDataGrid.Location = new System.Drawing.Point(24, 25);
            this.equipmentDataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.equipmentDataGrid.Name = "equipmentDataGrid";
            this.equipmentDataGrid.ReadOnly = true;
            this.equipmentDataGrid.RowHeadersWidth = 51;
            this.equipmentDataGrid.RowTemplate.Height = 24;
            this.equipmentDataGrid.Size = new System.Drawing.Size(752, 361);
            this.equipmentDataGrid.TabIndex = 13;
            // 
            // TransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.amountBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.equipmentDataGrid);
            this.Name = "TransferForm";
            this.Text = "TransferForm";
            ((System.ComponentModel.ISupportInitialize)(this.amountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown amountBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button requestButton;
        private System.Windows.Forms.DataGridView equipmentDataGrid;
    }
}