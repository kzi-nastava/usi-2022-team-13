
namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    partial class PatientRecommendation
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
            this.cbDoctors = new System.Windows.Forms.ComboBox();
            this.dtLatestDate = new System.Windows.Forms.DateTimePicker();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.rbDoctor = new System.Windows.Forms.RadioButton();
            this.rbTime = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgwRecommendations = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAppoint = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRecommendations)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDoctors
            // 
            this.cbDoctors.FormattingEnabled = true;
            this.cbDoctors.Location = new System.Drawing.Point(244, 41);
            this.cbDoctors.Name = "cbDoctors";
            this.cbDoctors.Size = new System.Drawing.Size(178, 28);
            this.cbDoctors.TabIndex = 0;
            // 
            // dtLatestDate
            // 
            this.dtLatestDate.Location = new System.Drawing.Point(244, 148);
            this.dtLatestDate.Name = "dtLatestDate";
            this.dtLatestDate.Size = new System.Drawing.Size(200, 26);
            this.dtLatestDate.TabIndex = 1;
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(333, 96);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(100, 26);
            this.tbStartTime.TabIndex = 2;
            // 
            // rbDoctor
            // 
            this.rbDoctor.AutoSize = true;
            this.rbDoctor.Location = new System.Drawing.Point(50, 31);
            this.rbDoctor.Name = "rbDoctor";
            this.rbDoctor.Size = new System.Drawing.Size(81, 24);
            this.rbDoctor.TabIndex = 3;
            this.rbDoctor.TabStop = true;
            this.rbDoctor.Text = "Doctor";
            this.rbDoctor.UseVisualStyleBackColor = true;
            this.rbDoctor.CheckedChanged += new System.EventHandler(this.rbDoctor_CheckedChanged);
            // 
            // rbTime
            // 
            this.rbTime.AutoSize = true;
            this.rbTime.Location = new System.Drawing.Point(189, 31);
            this.rbTime.Name = "rbTime";
            this.rbTime.Size = new System.Drawing.Size(150, 24);
            this.rbTime.TabIndex = 4;
            this.rbTime.TabStop = true;
            this.rbTime.Text = "Time Amplitude";
            this.rbTime.UseVisualStyleBackColor = true;
            this.rbTime.CheckedChanged += new System.EventHandler(this.rbTime_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDoctor);
            this.groupBox1.Controls.Add(this.rbTime);
            this.groupBox1.Location = new System.Drawing.Point(224, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 74);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Priority";
            // 
            // btnAccept
            // 
            this.btnAccept.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAccept.FlatAppearance.BorderSize = 3;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Lucida Bright", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Location = new System.Drawing.Point(496, 273);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(178, 44);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "FIND";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(514, 96);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(100, 26);
            this.tbEndTime.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Choose a Doctor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Choose Latest Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Time Span ( HH:MM )";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Choose Priority";
            // 
            // dgwRecommendations
            // 
            this.dgwRecommendations.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgwRecommendations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwRecommendations.Location = new System.Drawing.Point(43, 362);
            this.dgwRecommendations.Name = "dgwRecommendations";
            this.dgwRecommendations.Size = new System.Drawing.Size(694, 146);
            this.dgwRecommendations.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Recommendations";
            // 
            // btnAppoint
            // 
            this.btnAppoint.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAppoint.FlatAppearance.BorderSize = 3;
            this.btnAppoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppoint.Font = new System.Drawing.Font("Lucida Bright", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppoint.Location = new System.Drawing.Point(514, 514);
            this.btnAppoint.Name = "btnAppoint";
            this.btnAppoint.Size = new System.Drawing.Size(223, 47);
            this.btnAppoint.TabIndex = 16;
            this.btnAppoint.Text = "Appoint";
            this.btnAppoint.UseVisualStyleBackColor = true;
            this.btnAppoint.Click += new System.EventHandler(this.btnAppoint_Click);
            // 
            // PatientRecommendation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(749, 573);
            this.ControlBox = false;
            this.Controls.Add(this.btnAppoint);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgwRecommendations);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEndTime);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbStartTime);
            this.Controls.Add(this.dtLatestDate);
            this.Controls.Add(this.cbDoctors);
            this.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PatientRecommendation";
            this.Text = "PatientRecommendation";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRecommendations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDoctors;
        private System.Windows.Forms.DateTimePicker dtLatestDate;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.RadioButton rbDoctor;
        private System.Windows.Forms.RadioButton rbTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgwRecommendations;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAppoint;
    }
}