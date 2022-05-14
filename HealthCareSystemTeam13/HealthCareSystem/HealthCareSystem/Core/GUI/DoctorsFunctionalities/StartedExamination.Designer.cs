
namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    partial class StartedExamination
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
            this.lbPatientName = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.lbHeight = new System.Windows.Forms.Label();
            this.rbSpeciality = new System.Windows.Forms.RadioButton();
            this.rbDoctor = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSpeciality = new System.Windows.Forms.ComboBox();
            this.cbDoctor = new System.Windows.Forms.ComboBox();
            this.btnRefer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbPatientName
            // 
            this.lbPatientName.AutoSize = true;
            this.lbPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientName.Location = new System.Drawing.Point(229, 29);
            this.lbPatientName.Name = "lbPatientName";
            this.lbPatientName.Size = new System.Drawing.Size(229, 31);
            this.lbPatientName.TabIndex = 3;
            this.lbPatientName.Text = "Patient Full Name";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWeight.Location = new System.Drawing.Point(511, 207);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(96, 25);
            this.lbWeight.TabIndex = 5;
            this.lbWeight.Text = "lbWeight";
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeight.Location = new System.Drawing.Point(516, 119);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(91, 25);
            this.lbHeight.TabIndex = 4;
            this.lbHeight.Text = "lbHeight";
            this.lbHeight.Click += new System.EventHandler(this.lbHeight_Click);
            // 
            // rbSpeciality
            // 
            this.rbSpeciality.AutoSize = true;
            this.rbSpeciality.Location = new System.Drawing.Point(27, 249);
            this.rbSpeciality.Name = "rbSpeciality";
            this.rbSpeciality.Size = new System.Drawing.Size(85, 17);
            this.rbSpeciality.TabIndex = 6;
            this.rbSpeciality.TabStop = true;
            this.rbSpeciality.Text = "By Speciality";
            this.rbSpeciality.UseVisualStyleBackColor = true;
            this.rbSpeciality.CheckedChanged += new System.EventHandler(this.rbSpeciality_CheckedChanged);
            // 
            // rbDoctor
            // 
            this.rbDoctor.AutoSize = true;
            this.rbDoctor.Location = new System.Drawing.Point(150, 249);
            this.rbDoctor.Name = "rbDoctor";
            this.rbDoctor.Size = new System.Drawing.Size(109, 17);
            this.rbDoctor.TabIndex = 7;
            this.rbDoctor.TabStop = true;
            this.rbDoctor.Text = "By specific doctor";
            this.rbDoctor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Refer to another doctor";
            // 
            // cbSpeciality
            // 
            this.cbSpeciality.FormattingEnabled = true;
            this.cbSpeciality.Location = new System.Drawing.Point(12, 294);
            this.cbSpeciality.Name = "cbSpeciality";
            this.cbSpeciality.Size = new System.Drawing.Size(121, 21);
            this.cbSpeciality.TabIndex = 30;
            // 
            // cbDoctor
            // 
            this.cbDoctor.FormattingEnabled = true;
            this.cbDoctor.Location = new System.Drawing.Point(150, 294);
            this.cbDoctor.Name = "cbDoctor";
            this.cbDoctor.Size = new System.Drawing.Size(121, 21);
            this.cbDoctor.TabIndex = 31;
            // 
            // btnRefer
            // 
            this.btnRefer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefer.Location = new System.Drawing.Point(58, 347);
            this.btnRefer.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefer.Name = "btnRefer";
            this.btnRefer.Size = new System.Drawing.Size(158, 31);
            this.btnRefer.TabIndex = 32;
            this.btnRefer.Text = "Refer";
            this.btnRefer.UseVisualStyleBackColor = true;
            this.btnRefer.Click += new System.EventHandler(this.btnRefer_Click);
            // 
            // StartedExamination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 480);
            this.Controls.Add(this.btnRefer);
            this.Controls.Add(this.cbDoctor);
            this.Controls.Add(this.cbSpeciality);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbDoctor);
            this.Controls.Add(this.rbSpeciality);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.lbHeight);
            this.Controls.Add(this.lbPatientName);
            this.Name = "StartedExamination";
            this.Text = "StartedExamination";
            this.Load += new System.EventHandler(this.StartedExamination_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbPatientName;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.RadioButton rbSpeciality;
        private System.Windows.Forms.RadioButton rbDoctor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSpeciality;
        private System.Windows.Forms.ComboBox cbDoctor;
        private System.Windows.Forms.Button btnRefer;
    }
}