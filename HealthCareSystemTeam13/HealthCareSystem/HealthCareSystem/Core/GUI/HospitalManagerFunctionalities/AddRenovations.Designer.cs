
namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    partial class AddRenovations
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
            this.cmbFirstRoom = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblFirstRoom = new System.Windows.Forms.Label();
            this.lblDateStart = new System.Windows.Forms.Label();
            this.lblDateEnd = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.lblSecondRoom = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbSecondRoom = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbFirstRoom
            // 
            this.cmbFirstRoom.FormattingEnabled = true;
            this.cmbFirstRoom.Location = new System.Drawing.Point(331, 66);
            this.cmbFirstRoom.Name = "cmbFirstRoom";
            this.cmbFirstRoom.Size = new System.Drawing.Size(200, 24);
            this.cmbFirstRoom.TabIndex = 1;
            this.cmbFirstRoom.SelectedIndexChanged += new System.EventHandler(this.cmbFirstRoom_SelectedIndexChanged);
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(331, 177);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 24);
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblFirstRoom
            // 
            this.lblFirstRoom.AutoSize = true;
            this.lblFirstRoom.Location = new System.Drawing.Point(200, 73);
            this.lblFirstRoom.Name = "lblFirstRoom";
            this.lblFirstRoom.Size = new System.Drawing.Size(71, 17);
            this.lblFirstRoom.TabIndex = 3;
            this.lblFirstRoom.Text = "First room";
            this.lblFirstRoom.Click += new System.EventHandler(this.lblFirstRoom_Click);
            // 
            // lblDateStart
            // 
            this.lblDateStart.AutoSize = true;
            this.lblDateStart.Location = new System.Drawing.Point(200, 237);
            this.lblDateStart.Name = "lblDateStart";
            this.lblDateStart.Size = new System.Drawing.Size(112, 17);
            this.lblDateStart.TabIndex = 4;
            this.lblDateStart.Text = "Renovation start";
            this.lblDateStart.Click += new System.EventHandler(this.lblDateStart_Click);
            // 
            // lblDateEnd
            // 
            this.lblDateEnd.AutoSize = true;
            this.lblDateEnd.Location = new System.Drawing.Point(200, 286);
            this.lblDateEnd.Name = "lblDateEnd";
            this.lblDateEnd.Size = new System.Drawing.Size(108, 17);
            this.lblDateEnd.TabIndex = 5;
            this.lblDateEnd.Text = "Renovation end";
            this.lblDateEnd.Click += new System.EventHandler(this.lblDateEnd_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(311, 341);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(109, 45);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.Location = new System.Drawing.Point(331, 237);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(200, 22);
            this.dtpDateStart.TabIndex = 7;
            this.dtpDateStart.ValueChanged += new System.EventHandler(this.dtpDateStart_ValueChanged);
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.Location = new System.Drawing.Point(331, 286);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(200, 22);
            this.dtpDateEnd.TabIndex = 8;
            this.dtpDateEnd.ValueChanged += new System.EventHandler(this.dtpDateEnd_ValueChanged);
            // 
            // lblSecondRoom
            // 
            this.lblSecondRoom.AutoSize = true;
            this.lblSecondRoom.Location = new System.Drawing.Point(200, 130);
            this.lblSecondRoom.Name = "lblSecondRoom";
            this.lblSecondRoom.Size = new System.Drawing.Size(92, 17);
            this.lblSecondRoom.TabIndex = 9;
            this.lblSecondRoom.Text = "Second room";
            this.lblSecondRoom.Click += new System.EventHandler(this.lblSecondRoom_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(200, 180);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(111, 17);
            this.lblType.TabIndex = 10;
            this.lblType.Text = "Renovation type";
            this.lblType.Click += new System.EventHandler(this.lblType_Click);
            // 
            // cmbSecondRoom
            // 
            this.cmbSecondRoom.FormattingEnabled = true;
            this.cmbSecondRoom.Location = new System.Drawing.Point(331, 130);
            this.cmbSecondRoom.Name = "cmbSecondRoom";
            this.cmbSecondRoom.Size = new System.Drawing.Size(200, 24);
            this.cmbSecondRoom.TabIndex = 11;
            this.cmbSecondRoom.SelectedIndexChanged += new System.EventHandler(this.cmbSecondRoom_SelectedIndexChanged);
            // 
            // AddRenovations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 450);
            this.Controls.Add(this.cmbSecondRoom);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblSecondRoom);
            this.Controls.Add(this.dtpDateEnd);
            this.Controls.Add(this.dtpDateStart);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblDateEnd);
            this.Controls.Add(this.lblDateStart);
            this.Controls.Add(this.lblFirstRoom);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbFirstRoom);
            this.Name = "AddRenovations";
            this.Text = "AddRenovations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFirstRoom;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblFirstRoom;
        private System.Windows.Forms.Label lblDateStart;
        private System.Windows.Forms.Label lblDateEnd;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.Label lblSecondRoom;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbSecondRoom;
    }
}