
namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    partial class EquipmentMover
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
            this.dgwEquipment = new System.Windows.Forms.DataGridView();
            this.btnManage = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwEquipment)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwEquipment
            // 
            this.dgwEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwEquipment.Location = new System.Drawing.Point(4, 13);
            this.dgwEquipment.Margin = new System.Windows.Forms.Padding(4);
            this.dgwEquipment.Name = "dgwEquipment";
            this.dgwEquipment.RowHeadersWidth = 51;
            this.dgwEquipment.RowTemplate.Height = 24;
            this.dgwEquipment.Size = new System.Drawing.Size(737, 419);
            this.dgwEquipment.TabIndex = 5;
            // 
            // btnManage
            // 
            this.btnManage.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnManage.Location = new System.Drawing.Point(285, 450);
            this.btnManage.Margin = new System.Windows.Forms.Padding(4);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(170, 59);
            this.btnManage.TabIndex = 2;
            this.btnManage.Text = "MANAGE";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(14, 462);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(137, 47);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // EquipmentMover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(795, 556);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgwEquipment);
            this.Font = new System.Drawing.Font("Lucida Bright", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "EquipmentMover";
            this.Text = "EquipmentMover";
            ((System.ComponentModel.ISupportInitialize)(this.dgwEquipment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwEquipment;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Button btnRefresh;
    }
}