
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
            this.dgwMedications = new System.Windows.Forms.DataGridView();
            this.btnPrescribe = new System.Windows.Forms.Button();
            this.rtbInstructions = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFinish = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwMedications)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPatientName
            // 
            this.lbPatientName.AutoSize = true;
            this.lbPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientName.Location = new System.Drawing.Point(260, 9);
            this.lbPatientName.Name = "lbPatientName";
            this.lbPatientName.Size = new System.Drawing.Size(229, 31);
            this.lbPatientName.TabIndex = 3;
            this.lbPatientName.Text = "Patient Full Name";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWeight.Location = new System.Drawing.Point(65, 156);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(96, 25);
            this.lbWeight.TabIndex = 5;
            this.lbWeight.Text = "lbWeight";
            this.lbWeight.Click += new System.EventHandler(this.lbWeight_Click);
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeight.Location = new System.Drawing.Point(65, 112);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(91, 25);
            this.lbHeight.TabIndex = 4;
            this.lbHeight.Text = "lbHeight";
            this.lbHeight.Click += new System.EventHandler(this.lbHeight_Click);
            // 
            // rbSpeciality
            // 
            this.rbSpeciality.AutoSize = true;
            this.rbSpeciality.Location = new System.Drawing.Point(26, 277);
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
            this.rbDoctor.Location = new System.Drawing.Point(149, 277);
            this.rbDoctor.Name = "rbDoctor";
            this.rbDoctor.Size = new System.Drawing.Size(109, 17);
            this.rbDoctor.TabIndex = 7;
            this.rbDoctor.TabStop = true;
            this.rbDoctor.Text = "By specific doctor";
            this.rbDoctor.UseVisualStyleBackColor = true;
            this.rbDoctor.CheckedChanged += new System.EventHandler(this.rbDoctor_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Refer to another doctor";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbSpeciality
            // 
            this.cbSpeciality.FormattingEnabled = true;
            this.cbSpeciality.Location = new System.Drawing.Point(11, 322);
            this.cbSpeciality.Name = "cbSpeciality";
            this.cbSpeciality.Size = new System.Drawing.Size(121, 21);
            this.cbSpeciality.TabIndex = 30;
            this.cbSpeciality.SelectedIndexChanged += new System.EventHandler(this.cbSpeciality_SelectedIndexChanged);
            // 
            // cbDoctor
            // 
            this.cbDoctor.FormattingEnabled = true;
            this.cbDoctor.Location = new System.Drawing.Point(149, 322);
            this.cbDoctor.Name = "cbDoctor";
            this.cbDoctor.Size = new System.Drawing.Size(121, 21);
            this.cbDoctor.TabIndex = 31;
            this.cbDoctor.SelectedIndexChanged += new System.EventHandler(this.cbDoctor_SelectedIndexChanged);
            // 
            // btnRefer
            // 
            this.btnRefer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefer.Location = new System.Drawing.Point(57, 375);
            this.btnRefer.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefer.Name = "btnRefer";
            this.btnRefer.Size = new System.Drawing.Size(158, 31);
            this.btnRefer.TabIndex = 32;
            this.btnRefer.Text = "Refer";
            this.btnRefer.UseVisualStyleBackColor = true;
            this.btnRefer.Click += new System.EventHandler(this.btnRefer_Click);
            // 
            // dgwMedications
            // 
            this.dgwMedications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwMedications.Location = new System.Drawing.Point(440, 166);
            this.dgwMedications.Margin = new System.Windows.Forms.Padding(4);
            this.dgwMedications.Name = "dgwMedications";
            this.dgwMedications.Size = new System.Drawing.Size(261, 195);
            this.dgwMedications.TabIndex = 33;
            // 
            // btnPrescribe
            // 
            this.btnPrescribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrescribe.Location = new System.Drawing.Point(477, 533);
            this.btnPrescribe.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrescribe.Name = "btnPrescribe";
            this.btnPrescribe.Size = new System.Drawing.Size(158, 31);
            this.btnPrescribe.TabIndex = 34;
            this.btnPrescribe.Text = "Prescribe";
            this.btnPrescribe.UseVisualStyleBackColor = true;
            this.btnPrescribe.Click += new System.EventHandler(this.btnPrescribe_Click);
            // 
            // rtbInstructions
            // 
            this.rtbInstructions.Location = new System.Drawing.Point(460, 409);
            this.rtbInstructions.Name = "rtbInstructions";
            this.rtbInstructions.Size = new System.Drawing.Size(197, 117);
            this.rtbInstructions.TabIndex = 35;
            this.rtbInstructions.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(472, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 25);
            this.label2.TabIndex = 36;
            this.label2.Text = "Medical prescription";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(384, 377);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(382, 16);
            this.label3.TabIndex = 37;
            this.label3.Text = "How frequently should the patient drink medicine, when to stop...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(506, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 16);
            this.label4.TabIndex = 38;
            this.label4.Text = "Active medication";
            // 
            // btnFinish
            // 
            this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinish.Location = new System.Drawing.Point(57, 495);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(201, 56);
            this.btnFinish.TabIndex = 39;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // StartedExamination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 585);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtbInstructions);
            this.Controls.Add(this.btnPrescribe);
            this.Controls.Add(this.dgwMedications);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgwMedications)).EndInit();
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
        private System.Windows.Forms.DataGridView dgwMedications;
        private System.Windows.Forms.Button btnPrescribe;
        private System.Windows.Forms.RichTextBox rtbInstructions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFinish;
    }
}