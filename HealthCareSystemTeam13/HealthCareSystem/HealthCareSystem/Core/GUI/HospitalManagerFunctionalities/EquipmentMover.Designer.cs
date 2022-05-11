
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
            this.tbxSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.cmbEquipmentType = new System.Windows.Forms.ComboBox();
            this.cmbAmount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.btnManage.Font = new System.Drawing.Font("Lucida Bright", 10F, System.Drawing.FontStyle.Bold);
            this.btnManage.Location = new System.Drawing.Point(159, 462);
            this.btnManage.Margin = new System.Windows.Forms.Padding(4);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(78, 47);
            this.btnManage.TabIndex = 2;
            this.btnManage.Text = "Move";
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
            // tbxSearch
            // 
            this.tbxSearch.Location = new System.Drawing.Point(559, 439);
            this.tbxSearch.Name = "tbxSearch";
            this.tbxSearch.Size = new System.Drawing.Size(182, 31);
            this.tbxSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(647, 478);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 41);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Location = new System.Drawing.Point(430, 439);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(121, 31);
            this.cmbRoomType.TabIndex = 12;
            // 
            // cmbEquipmentType
            // 
            this.cmbEquipmentType.FormattingEnabled = true;
            this.cmbEquipmentType.Location = new System.Drawing.Point(430, 476);
            this.cmbEquipmentType.Name = "cmbEquipmentType";
            this.cmbEquipmentType.Size = new System.Drawing.Size(121, 31);
            this.cmbEquipmentType.TabIndex = 13;
            // 
            // cmbAmount
            // 
            this.cmbAmount.FormattingEnabled = true;
            this.cmbAmount.Location = new System.Drawing.Point(430, 513);
            this.cmbAmount.Name = "cmbAmount";
            this.cmbAmount.Size = new System.Drawing.Size(121, 31);
            this.cmbAmount.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Bright", 10F);
            this.label1.Location = new System.Drawing.Point(296, 450);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tip prostorije";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Bright", 10F);
            this.label2.Location = new System.Drawing.Point(296, 487);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Tip opreme";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Bright", 10F);
            this.label3.Location = new System.Drawing.Point(296, 524);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Kolicina";
            // 
            // EquipmentMover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(795, 556);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbAmount);
            this.Controls.Add(this.cmbEquipmentType);
            this.Controls.Add(this.cmbRoomType);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbxSearch);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwEquipment;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox tbxSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.ComboBox cmbEquipmentType;
        private System.Windows.Forms.ComboBox cmbAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}