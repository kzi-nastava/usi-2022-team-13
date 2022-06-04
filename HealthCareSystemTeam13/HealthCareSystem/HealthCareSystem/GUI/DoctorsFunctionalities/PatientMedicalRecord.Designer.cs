
namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    partial class PatientMedicalRecord
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
            this.lbHeight = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeight.Location = new System.Drawing.Point(90, 54);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(114, 31);
            this.lbHeight.TabIndex = 0;
            this.lbHeight.Text = "lbHeight";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWeight.Location = new System.Drawing.Point(85, 142);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(119, 31);
            this.lbWeight.TabIndex = 1;
            this.lbWeight.Text = "lbWeight";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(63, 276);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(177, 59);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PatientMedicalRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 348);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.lbHeight);
            this.Name = "PatientMedicalRecord";
            this.Text = "PatientMedicalRecord";
            this.Load += new System.EventHandler(this.PatientMedicalRecord_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Button btnClose;
    }
}