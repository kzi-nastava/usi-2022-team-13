
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
            this.SuspendLayout();
            // 
            // lbDynamicEquipment
            // 
            this.lbDynamicEquipment.FormattingEnabled = true;
            this.lbDynamicEquipment.Location = new System.Drawing.Point(119, 41);
            this.lbDynamicEquipment.Name = "lbDynamicEquipment";
            this.lbDynamicEquipment.Size = new System.Drawing.Size(172, 147);
            this.lbDynamicEquipment.TabIndex = 0;
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(155, 222);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNumber.TabIndex = 1;
            // 
            // btnUpdateDynamicEquipment
            // 
            this.btnUpdateDynamicEquipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDynamicEquipment.Location = new System.Drawing.Point(129, 280);
            this.btnUpdateDynamicEquipment.Name = "btnUpdateDynamicEquipment";
            this.btnUpdateDynamicEquipment.Size = new System.Drawing.Size(176, 54);
            this.btnUpdateDynamicEquipment.TabIndex = 2;
            this.btnUpdateDynamicEquipment.Text = "Update selected equipment";
            this.btnUpdateDynamicEquipment.UseVisualStyleBackColor = true;
            this.btnUpdateDynamicEquipment.Click += new System.EventHandler(this.btnUpdateDynamicEquipment_Click);
            // 
            // SetUsedDynamicEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 428);
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
    }
}