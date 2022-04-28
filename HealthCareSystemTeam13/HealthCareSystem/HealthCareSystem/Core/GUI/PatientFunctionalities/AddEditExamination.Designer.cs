
namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    partial class AddEditExamination
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbExaminationId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDoctors = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRoomId = new System.Windows.Forms.TextBox();
            this.tbDuration = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(194, 402);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(295, 402);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Examination ID";
            // 
            // tbExaminationId
            // 
            this.tbExaminationId.Enabled = false;
            this.tbExaminationId.Location = new System.Drawing.Point(150, 48);
            this.tbExaminationId.Name = "tbExaminationId";
            this.tbExaminationId.Size = new System.Drawing.Size(100, 20);
            this.tbExaminationId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Doctor";
            // 
            // cbDoctors
            // 
            this.cbDoctors.FormattingEnabled = true;
            this.cbDoctors.Location = new System.Drawing.Point(150, 80);
            this.cbDoctors.Name = "cbDoctors";
            this.cbDoctors.Size = new System.Drawing.Size(121, 21);
            this.cbDoctors.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Date";
            // 
            // dtDate
            // 
            this.dtDate.Location = new System.Drawing.Point(134, 121);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(200, 20);
            this.dtDate.TabIndex = 7;
            this.dtDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Room ID";
            // 
            // tbRoomId
            // 
            this.tbRoomId.Enabled = false;
            this.tbRoomId.Location = new System.Drawing.Point(150, 170);
            this.tbRoomId.Name = "tbRoomId";
            this.tbRoomId.Size = new System.Drawing.Size(100, 20);
            this.tbRoomId.TabIndex = 9;
            this.tbRoomId.TextChanged += new System.EventHandler(this.tbRoomId_TextChanged);
            // 
            // tbDuration
            // 
            this.tbDuration.Enabled = false;
            this.tbDuration.Location = new System.Drawing.Point(150, 220);
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new System.Drawing.Size(100, 20);
            this.tbDuration.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Duration (min)";
            // 
            // AddEditExamination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 452);
            this.Controls.Add(this.tbDuration);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbRoomId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDoctors);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbExaminationId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "AddEditExamination";
            this.Text = "AddEditExamination";
            this.Load += new System.EventHandler(this.AddEditExamination_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbExaminationId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDoctors;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRoomId;
        private System.Windows.Forms.TextBox tbDuration;
        private System.Windows.Forms.Label label5;
    }
}