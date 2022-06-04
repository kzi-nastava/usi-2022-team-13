
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class OutOfStockInRoomsForm
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
            this.dynamicEquipmentGrid = new System.Windows.Forms.DataGridView();
            this.transferButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicEquipmentGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dynamicEquipmentGrid
            // 
            this.dynamicEquipmentGrid.AllowUserToAddRows = false;
            this.dynamicEquipmentGrid.AllowUserToDeleteRows = false;
            this.dynamicEquipmentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dynamicEquipmentGrid.Location = new System.Drawing.Point(22, 22);
            this.dynamicEquipmentGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dynamicEquipmentGrid.Name = "dynamicEquipmentGrid";
            this.dynamicEquipmentGrid.ReadOnly = true;
            this.dynamicEquipmentGrid.RowHeadersWidth = 51;
            this.dynamicEquipmentGrid.RowTemplate.Height = 24;
            this.dynamicEquipmentGrid.Size = new System.Drawing.Size(752, 361);
            this.dynamicEquipmentGrid.TabIndex = 10;
            // 
            // transferButton
            // 
            this.transferButton.Location = new System.Drawing.Point(323, 400);
            this.transferButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(133, 23);
            this.transferButton.TabIndex = 11;
            this.transferButton.Text = "TRANSFER TO";
            this.transferButton.UseVisualStyleBackColor = true;
            this.transferButton.Click += new System.EventHandler(this.transferButton_Click);
            // 
            // OutOfStockInRoomsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.transferButton);
            this.Controls.Add(this.dynamicEquipmentGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OutOfStockInRoomsForm";
            this.Text = "OutOfStockInRoomsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dynamicEquipmentGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dynamicEquipmentGrid;
        private System.Windows.Forms.Button transferButton;
    }
}