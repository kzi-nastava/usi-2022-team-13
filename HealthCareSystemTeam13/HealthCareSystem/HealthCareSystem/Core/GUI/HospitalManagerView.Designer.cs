
namespace HealthCareSystem.Core.GUI
{
    partial class HospitalManagerView
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
            this.pnlHospitalManager = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRooms = new System.Windows.Forms.Button();
            this.btnEquipment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlHospitalManager
            // 
            this.pnlHospitalManager.Location = new System.Drawing.Point(194, 3);
            this.pnlHospitalManager.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHospitalManager.Name = "pnlHospitalManager";
            this.pnlHospitalManager.Size = new System.Drawing.Size(999, 705);
            this.pnlHospitalManager.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(4, 633);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(183, 64);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Logout";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRooms
            // 
            this.btnRooms.Location = new System.Drawing.Point(4, 34);
            this.btnRooms.Margin = new System.Windows.Forms.Padding(4);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(183, 108);
            this.btnRooms.TabIndex = 4;
            this.btnRooms.Text = "Room manager";
            this.btnRooms.UseVisualStyleBackColor = true;
            this.btnRooms.Click += new System.EventHandler(this.btnRooms_Click);
            // 
            // btnEquipment
            // 
            this.btnEquipment.Location = new System.Drawing.Point(4, 149);
            this.btnEquipment.Margin = new System.Windows.Forms.Padding(4);
            this.btnEquipment.Name = "btnEquipment";
            this.btnEquipment.Size = new System.Drawing.Size(183, 108);
            this.btnEquipment.TabIndex = 7;
            this.btnEquipment.Text = "Equipment manager";
            this.btnEquipment.UseVisualStyleBackColor = true;
            this.btnEquipment.Click += new System.EventHandler(this.btnEquipment_Click);
            // 
            // HospitalManagerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 710);
            this.Controls.Add(this.btnEquipment);
            this.Controls.Add(this.pnlHospitalManager);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRooms);
            this.Name = "HospitalManagerView";
            this.Text = "HospitalManagerView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHospitalManager;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRooms;
        private System.Windows.Forms.Button btnEquipment;
    }
}