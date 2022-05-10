
namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    partial class MedicalRecordView
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
            this.lbName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.lbHeight = new System.Windows.Forms.Label();
            this.dgwExaminations = new System.Windows.Forms.DataGridView();
            this.lbDiseases = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowAnamnesis = new System.Windows.Forms.Button();
            this.tbAnamnesis = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSortByDate = new System.Windows.Forms.Button();
            this.btnShowAnamnesisInSearch = new System.Windows.Forms.Button();
            this.btnSortBySpeciality = new System.Windows.Forms.Button();
            this.btnSortByDoctor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchAnamnesis = new System.Windows.Forms.Button();
            this.dgwAnamnesis = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExaminations)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwAnamnesis)).BeginInit();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Lucida Bright", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(319, 39);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(124, 42);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Weight";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWeight.Location = new System.Drawing.Point(187, 115);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(32, 22);
            this.lbWeight.TabIndex = 7;
            this.lbWeight.Text = "kg";
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeight.Location = new System.Drawing.Point(187, 72);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(38, 22);
            this.lbHeight.TabIndex = 6;
            this.lbHeight.Text = "cm";
            // 
            // dgwExaminations
            // 
            this.dgwExaminations.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgwExaminations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwExaminations.Location = new System.Drawing.Point(15, 42);
            this.dgwExaminations.Name = "dgwExaminations";
            this.dgwExaminations.Size = new System.Drawing.Size(674, 172);
            this.dgwExaminations.TabIndex = 8;
            // 
            // lbDiseases
            // 
            this.lbDiseases.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lbDiseases.FormattingEnabled = true;
            this.lbDiseases.ItemHeight = 18;
            this.lbDiseases.Location = new System.Drawing.Point(36, 25);
            this.lbDiseases.Name = "lbDiseases";
            this.lbDiseases.Size = new System.Drawing.Size(283, 112);
            this.lbDiseases.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbDiseases);
            this.groupBox1.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 462);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 160);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Disease History";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnShowAnamnesis);
            this.groupBox2.Controls.Add(this.dgwExaminations);
            this.groupBox2.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(17, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(695, 303);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Examination/Operation History";
            // 
            // btnShowAnamnesis
            // 
            this.btnShowAnamnesis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAnamnesis.FlatAppearance.BorderSize = 3;
            this.btnShowAnamnesis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAnamnesis.Location = new System.Drawing.Point(456, 246);
            this.btnShowAnamnesis.Name = "btnShowAnamnesis";
            this.btnShowAnamnesis.Size = new System.Drawing.Size(233, 40);
            this.btnShowAnamnesis.TabIndex = 9;
            this.btnShowAnamnesis.Text = "Show Anamnesis";
            this.btnShowAnamnesis.UseVisualStyleBackColor = true;
            this.btnShowAnamnesis.Click += new System.EventHandler(this.btnShowAnamnesis_Click);
            // 
            // tbAnamnesis
            // 
            this.tbAnamnesis.Location = new System.Drawing.Point(206, 49);
            this.tbAnamnesis.Name = "tbAnamnesis";
            this.tbAnamnesis.Size = new System.Drawing.Size(180, 30);
            this.tbAnamnesis.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 22);
            this.label2.TabIndex = 17;
            this.label2.Text = "Enter Keyword";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSortByDate);
            this.groupBox3.Controls.Add(this.btnShowAnamnesisInSearch);
            this.groupBox3.Controls.Add(this.btnSortBySpeciality);
            this.groupBox3.Controls.Add(this.btnSortByDoctor);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnSearchAnamnesis);
            this.groupBox3.Controls.Add(this.dgwAnamnesis);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tbAnamnesis);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(17, 643);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(702, 411);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Anamnesis";
            // 
            // btnSortByDate
            // 
            this.btnSortByDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortByDate.FlatAppearance.BorderSize = 3;
            this.btnSortByDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByDate.Location = new System.Drawing.Point(151, 107);
            this.btnSortByDate.Name = "btnSortByDate";
            this.btnSortByDate.Size = new System.Drawing.Size(149, 35);
            this.btnSortByDate.TabIndex = 22;
            this.btnSortByDate.Text = "Date";
            this.btnSortByDate.UseVisualStyleBackColor = true;
            this.btnSortByDate.Click += new System.EventHandler(this.btnSortByDate_Click);
            // 
            // btnShowAnamnesisInSearch
            // 
            this.btnShowAnamnesisInSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAnamnesisInSearch.FlatAppearance.BorderSize = 3;
            this.btnShowAnamnesisInSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAnamnesisInSearch.Location = new System.Drawing.Point(425, 359);
            this.btnShowAnamnesisInSearch.Name = "btnShowAnamnesisInSearch";
            this.btnShowAnamnesisInSearch.Size = new System.Drawing.Size(233, 40);
            this.btnShowAnamnesisInSearch.TabIndex = 10;
            this.btnShowAnamnesisInSearch.Text = "Show Anamnesis";
            this.btnShowAnamnesisInSearch.UseVisualStyleBackColor = true;
            this.btnShowAnamnesisInSearch.Click += new System.EventHandler(this.btnShowAnamnesisInSearch_Click);
            // 
            // btnSortBySpeciality
            // 
            this.btnSortBySpeciality.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortBySpeciality.FlatAppearance.BorderSize = 3;
            this.btnSortBySpeciality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortBySpeciality.Location = new System.Drawing.Point(509, 107);
            this.btnSortBySpeciality.Name = "btnSortBySpeciality";
            this.btnSortBySpeciality.Size = new System.Drawing.Size(149, 35);
            this.btnSortBySpeciality.TabIndex = 21;
            this.btnSortBySpeciality.Text = "Speciality";
            this.btnSortBySpeciality.UseVisualStyleBackColor = true;
            this.btnSortBySpeciality.Click += new System.EventHandler(this.btnSortBySpeciality_Click);
            // 
            // btnSortByDoctor
            // 
            this.btnSortByDoctor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortByDoctor.FlatAppearance.BorderSize = 3;
            this.btnSortByDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByDoctor.Location = new System.Drawing.Point(331, 107);
            this.btnSortByDoctor.Name = "btnSortByDoctor";
            this.btnSortByDoctor.Size = new System.Drawing.Size(149, 35);
            this.btnSortByDoctor.TabIndex = 20;
            this.btnSortByDoctor.Text = "Doctor";
            this.btnSortByDoctor.UseVisualStyleBackColor = true;
            this.btnSortByDoctor.Click += new System.EventHandler(this.btnSortByDoctor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 22);
            this.label3.TabIndex = 19;
            this.label3.Text = "Sort By";
            // 
            // btnSearchAnamnesis
            // 
            this.btnSearchAnamnesis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchAnamnesis.FlatAppearance.BorderSize = 3;
            this.btnSearchAnamnesis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchAnamnesis.Location = new System.Drawing.Point(434, 46);
            this.btnSearchAnamnesis.Name = "btnSearchAnamnesis";
            this.btnSearchAnamnesis.Size = new System.Drawing.Size(149, 35);
            this.btnSearchAnamnesis.TabIndex = 10;
            this.btnSearchAnamnesis.Text = "Search";
            this.btnSearchAnamnesis.UseVisualStyleBackColor = true;
            this.btnSearchAnamnesis.Click += new System.EventHandler(this.btnSearchAnamnesis_Click);
            // 
            // dgwAnamnesis
            // 
            this.dgwAnamnesis.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgwAnamnesis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwAnamnesis.Location = new System.Drawing.Point(15, 156);
            this.dgwAnamnesis.Name = "dgwAnamnesis";
            this.dgwAnamnesis.Size = new System.Drawing.Size(643, 180);
            this.dgwAnamnesis.TabIndex = 18;
            // 
            // MedicalRecordView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(765, 571);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.lbHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbName);
            this.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MedicalRecordView";
            this.Text = "MedicalRecordView";
            this.Load += new System.EventHandler(this.MedicalRecordView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwExaminations)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwAnamnesis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.DataGridView dgwExaminations;
        private System.Windows.Forms.ListBox lbDiseases;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbAnamnesis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgwAnamnesis;
        private System.Windows.Forms.Button btnShowAnamnesis;
        private System.Windows.Forms.Button btnSearchAnamnesis;
        private System.Windows.Forms.Button btnSortBySpeciality;
        private System.Windows.Forms.Button btnSortByDoctor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShowAnamnesisInSearch;
        private System.Windows.Forms.Button btnSortByDate;
    }
}