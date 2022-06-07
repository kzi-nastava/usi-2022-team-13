
namespace HealthCareSystem
{
    partial class DoctorView
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgwExaminations = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnShowDay = new System.Windows.Forms.Button();
            this.btnShowNextThreeDays = new System.Windows.Forms.Button();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.btnMedicalRecord = new System.Windows.Forms.Button();
            this.btnStartExamination = new System.Windows.Forms.Button();
            this.btnMedicine = new System.Windows.Forms.Button();
            this.btnRequestDaysOff = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExaminations)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(195, 17);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(177, 59);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(796, 17);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(143, 59);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(657, 17);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(131, 59);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgwExaminations
            // 
            this.dgwExaminations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwExaminations.Location = new System.Drawing.Point(217, 134);
            this.dgwExaminations.Margin = new System.Windows.Forms.Padding(4);
            this.dgwExaminations.Name = "dgwExaminations";
            this.dgwExaminations.Size = new System.Drawing.Size(769, 421);
            this.dgwExaminations.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(952, 17);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 59);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "REMOVE";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(12, 599);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(184, 47);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Log out";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnShowDay
            // 
            this.btnShowDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDay.Location = new System.Drawing.Point(12, 289);
            this.btnShowDay.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowDay.Name = "btnShowDay";
            this.btnShowDay.Size = new System.Drawing.Size(184, 84);
            this.btnShowDay.TabIndex = 12;
            this.btnShowDay.Text = "Show examinations for the specific day";
            this.btnShowDay.UseVisualStyleBackColor = true;
            this.btnShowDay.Click += new System.EventHandler(this.btnShowDay_Click);
            // 
            // btnShowNextThreeDays
            // 
            this.btnShowNextThreeDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowNextThreeDays.Location = new System.Drawing.Point(13, 395);
            this.btnShowNextThreeDays.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowNextThreeDays.Name = "btnShowNextThreeDays";
            this.btnShowNextThreeDays.Size = new System.Drawing.Size(184, 82);
            this.btnShowNextThreeDays.TabIndex = 13;
            this.btnShowNextThreeDays.Text = "Show examinations for the next 3 days";
            this.btnShowNextThreeDays.UseVisualStyleBackColor = true;
            this.btnShowNextThreeDays.Click += new System.EventHandler(this.btnShowNextThreeDays_Click);
            // 
            // dtDate
            // 
            this.dtDate.Location = new System.Drawing.Point(10, 134);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(200, 20);
            this.dtDate.TabIndex = 14;
            // 
            // btnMedicalRecord
            // 
            this.btnMedicalRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedicalRecord.Location = new System.Drawing.Point(13, 503);
            this.btnMedicalRecord.Margin = new System.Windows.Forms.Padding(4);
            this.btnMedicalRecord.Name = "btnMedicalRecord";
            this.btnMedicalRecord.Size = new System.Drawing.Size(184, 82);
            this.btnMedicalRecord.TabIndex = 15;
            this.btnMedicalRecord.Text = "Medical Record of the selected patient";
            this.btnMedicalRecord.UseVisualStyleBackColor = true;
            this.btnMedicalRecord.Click += new System.EventHandler(this.btnMedicalRecord_Click);
            // 
            // btnStartExamination
            // 
            this.btnStartExamination.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartExamination.Location = new System.Drawing.Point(10, 17);
            this.btnStartExamination.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartExamination.Name = "btnStartExamination";
            this.btnStartExamination.Size = new System.Drawing.Size(177, 59);
            this.btnStartExamination.TabIndex = 16;
            this.btnStartExamination.Text = "Start selected examination";
            this.btnStartExamination.UseVisualStyleBackColor = true;
            this.btnStartExamination.Click += new System.EventHandler(this.btnStartExamination_Click);
            // 
            // btnMedicine
            // 
            this.btnMedicine.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedicine.Location = new System.Drawing.Point(380, 17);
            this.btnMedicine.Margin = new System.Windows.Forms.Padding(4);
            this.btnMedicine.Name = "btnMedicine";
            this.btnMedicine.Size = new System.Drawing.Size(128, 59);
            this.btnMedicine.TabIndex = 17;
            this.btnMedicine.Text = "Medicine Managment";
            this.btnMedicine.UseVisualStyleBackColor = true;
            this.btnMedicine.Click += new System.EventHandler(this.btnMedicine_Click);
            // 
            // btnRequestDaysOff
            // 
            this.btnRequestDaysOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestDaysOff.Location = new System.Drawing.Point(534, 17);
            this.btnRequestDaysOff.Margin = new System.Windows.Forms.Padding(4);
            this.btnRequestDaysOff.Name = "btnRequestDaysOff";
            this.btnRequestDaysOff.Size = new System.Drawing.Size(115, 59);
            this.btnRequestDaysOff.TabIndex = 18;
            this.btnRequestDaysOff.Text = "Request Days Off";
            this.btnRequestDaysOff.UseVisualStyleBackColor = true;
            this.btnRequestDaysOff.Click += new System.EventHandler(this.btnRequestDaysOff_Click);
            // 
            // DoctorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 658);
            this.Controls.Add(this.btnRequestDaysOff);
            this.Controls.Add(this.btnMedicine);
            this.Controls.Add(this.btnStartExamination);
            this.Controls.Add(this.btnMedicalRecord);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.btnShowNextThreeDays);
            this.Controls.Add(this.btnShowDay);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgwExaminations);
            this.Name = "DoctorView";
            this.Text = "Doctor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DoctorView_FormClosing);
            this.Load += new System.EventHandler(this.DoctorView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwExaminations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgwExaminations;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnShowDay;
        private System.Windows.Forms.Button btnShowNextThreeDays;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Button btnMedicalRecord;
        private System.Windows.Forms.Button btnStartExamination;
        private System.Windows.Forms.Button btnMedicine;
        private System.Windows.Forms.Button btnRequestDaysOff;
    }
}