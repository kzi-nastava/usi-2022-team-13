
namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    partial class SearchDoctorView
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
            this.btnAppoint = new System.Windows.Forms.Button();
            this.btnSortByAverageRating = new System.Windows.Forms.Button();
            this.btnSortByLastName = new System.Windows.Forms.Button();
            this.btnSortByName = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSearchDoctor = new System.Windows.Forms.TextBox();
            this.dgwDoctors = new System.Windows.Forms.DataGridView();
            this.btnSortBySpeciality = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDoctors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAppoint
            // 
            this.btnAppoint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAppoint.FlatAppearance.BorderSize = 3;
            this.btnAppoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppoint.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppoint.Location = new System.Drawing.Point(440, 363);
            this.btnAppoint.Name = "btnAppoint";
            this.btnAppoint.Size = new System.Drawing.Size(275, 40);
            this.btnAppoint.TabIndex = 10;
            this.btnAppoint.Text = "Add Appointment";
            this.btnAppoint.UseVisualStyleBackColor = true;
            // 
            // btnSortByAverageRating
            // 
            this.btnSortByAverageRating.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortByAverageRating.FlatAppearance.BorderSize = 3;
            this.btnSortByAverageRating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByAverageRating.Location = new System.Drawing.Point(149, 107);
            this.btnSortByAverageRating.Name = "btnSortByAverageRating";
            this.btnSortByAverageRating.Size = new System.Drawing.Size(132, 35);
            this.btnSortByAverageRating.TabIndex = 28;
            this.btnSortByAverageRating.Text = "Rating";
            this.btnSortByAverageRating.UseVisualStyleBackColor = true;
            // 
            // btnSortByLastName
            // 
            this.btnSortByLastName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortByLastName.FlatAppearance.BorderSize = 3;
            this.btnSortByLastName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByLastName.Location = new System.Drawing.Point(440, 107);
            this.btnSortByLastName.Name = "btnSortByLastName";
            this.btnSortByLastName.Size = new System.Drawing.Size(130, 36);
            this.btnSortByLastName.TabIndex = 27;
            this.btnSortByLastName.Text = "Last Name";
            this.btnSortByLastName.UseVisualStyleBackColor = true;
            // 
            // btnSortByName
            // 
            this.btnSortByName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortByName.FlatAppearance.BorderSize = 3;
            this.btnSortByName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByName.Location = new System.Drawing.Point(293, 107);
            this.btnSortByName.Name = "btnSortByName";
            this.btnSortByName.Size = new System.Drawing.Size(130, 35);
            this.btnSortByName.TabIndex = 26;
            this.btnSortByName.Text = "First Name";
            this.btnSortByName.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 22);
            this.label3.TabIndex = 25;
            this.label3.Text = "Sort By";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 22);
            this.label2.TabIndex = 24;
            this.label2.Text = "Enter Keyword";
            // 
            // tbSearchDoctor
            // 
            this.tbSearchDoctor.Location = new System.Drawing.Point(219, 49);
            this.tbSearchDoctor.Name = "tbSearchDoctor";
            this.tbSearchDoctor.Size = new System.Drawing.Size(274, 26);
            this.tbSearchDoctor.TabIndex = 23;
            this.tbSearchDoctor.TextChanged += new System.EventHandler(this.tbSearchDoctor_TextChanged);
            // 
            // dgwDoctors
            // 
            this.dgwDoctors.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgwDoctors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDoctors.Location = new System.Drawing.Point(28, 161);
            this.dgwDoctors.Name = "dgwDoctors";
            this.dgwDoctors.Size = new System.Drawing.Size(687, 180);
            this.dgwDoctors.TabIndex = 29;
            // 
            // btnSortBySpeciality
            // 
            this.btnSortBySpeciality.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSortBySpeciality.FlatAppearance.BorderSize = 3;
            this.btnSortBySpeciality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortBySpeciality.Location = new System.Drawing.Point(585, 107);
            this.btnSortBySpeciality.Name = "btnSortBySpeciality";
            this.btnSortBySpeciality.Size = new System.Drawing.Size(130, 35);
            this.btnSortBySpeciality.TabIndex = 30;
            this.btnSortBySpeciality.Text = "Speciality";
            this.btnSortBySpeciality.UseVisualStyleBackColor = true;
            // 
            // SearchDoctorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(785, 571);
            this.ControlBox = false;
            this.Controls.Add(this.btnSortBySpeciality);
            this.Controls.Add(this.dgwDoctors);
            this.Controls.Add(this.btnSortByAverageRating);
            this.Controls.Add(this.btnSortByLastName);
            this.Controls.Add(this.btnSortByName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSearchDoctor);
            this.Controls.Add(this.btnAppoint);
            this.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SearchDoctorView";
            this.Text = "SearchDoctorView";
            this.Load += new System.EventHandler(this.SearchDoctorView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwDoctors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAppoint;
        private System.Windows.Forms.Button btnSortByAverageRating;
        private System.Windows.Forms.Button btnSortByLastName;
        private System.Windows.Forms.Button btnSortByName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSearchDoctor;
        private System.Windows.Forms.DataGridView dgwDoctors;
        private System.Windows.Forms.Button btnSortBySpeciality;
    }
}