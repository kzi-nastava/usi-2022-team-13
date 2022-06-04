
namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    partial class SetUsedDynamicEquipment
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
            this.lbDynamicEquipment = new System.Windows.Forms.ListBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.btnUpdateDynamicEquipment = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbDynamicEquipment
            // 
            this.lbDynamicEquipment.FormattingEnabled = true;
            this.lbDynamicEquipment.Location = new System.Drawing.Point(129, 72);
            this.lbDynamicEquipment.Name = "lbDynamicEquipment";
            this.lbDynamicEquipment.Size = new System.Drawing.Size(172, 147);
            this.lbDynamicEquipment.TabIndex = 0;
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(167, 281);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNumber.TabIndex = 1;
            // 
            // btnUpdateDynamicEquipment
            // 
            this.btnUpdateDynamicEquipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDynamicEquipment.Location = new System.Drawing.Point(49, 347);
            this.btnUpdateDynamicEquipment.Name = "btnUpdateDynamicEquipment";
            this.btnUpdateDynamicEquipment.Size = new System.Drawing.Size(176, 54);
            this.btnUpdateDynamicEquipment.TabIndex = 2;
            this.btnUpdateDynamicEquipment.Text = "Update selected equipment";
            this.btnUpdateDynamicEquipment.UseVisualStyleBackColor = true;
            this.btnUpdateDynamicEquipment.Click += new System.EventHandler(this.btnUpdateDynamicEquipment_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(77, 18);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(285, 24);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Throw away the used equipment";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(126, 253);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(179, 15);
            this.lblAmount.TabIndex = 4;
            this.lblAmount.Text = "Insert the amount to throw away";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(289, 347);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 54);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SetUsedDynamicEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 428);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnUpdateDynamicEquipment);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.lbDynamicEquipment);
            this.Name = "SetUsedDynamicEquipment";
            this.Text = "SetUsedDynamicEquipment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbDynamicEquipment;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Button btnUpdateDynamicEquipment;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Button btnExit;
    }
}