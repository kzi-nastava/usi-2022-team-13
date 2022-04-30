
namespace HealthCareSystem.Core.GUI
{
    partial class PatientView
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
            this.btnExaminations = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlPatient = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnExaminations
            // 
<<<<<<< HEAD
            this.btnExaminations.BackColor = System.Drawing.Color.Transparent;
            this.btnExaminations.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExaminations.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExaminations.FlatAppearance.BorderSize = 3;
            this.btnExaminations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExaminations.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
=======
>>>>>>> feature/Doctor
            this.btnExaminations.Location = new System.Drawing.Point(2, 26);
            this.btnExaminations.Name = "btnExaminations";
            this.btnExaminations.Size = new System.Drawing.Size(137, 88);
            this.btnExaminations.TabIndex = 0;
            this.btnExaminations.Text = "My Examinations";
<<<<<<< HEAD
            this.btnExaminations.UseVisualStyleBackColor = false;
=======
            this.btnExaminations.UseVisualStyleBackColor = true;
>>>>>>> feature/Doctor
            this.btnExaminations.Click += new System.EventHandler(this.btnExaminations_Click);
            // 
            // btnExit
            // 
<<<<<<< HEAD
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.FlatAppearance.BorderSize = 3;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(2, 483);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(137, 82);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Logout";
            this.btnExit.UseVisualStyleBackColor = false;
=======
            this.btnExit.Location = new System.Drawing.Point(2, 513);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(137, 52);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Logout";
            this.btnExit.UseVisualStyleBackColor = true;
>>>>>>> feature/Doctor
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlPatient
            // 
            this.pnlPatient.Location = new System.Drawing.Point(145, 1);
            this.pnlPatient.Name = "pnlPatient";
            this.pnlPatient.Size = new System.Drawing.Size(749, 573);
            this.pnlPatient.TabIndex = 3;
            // 
            // PatientView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
            this.BackColor = System.Drawing.Color.SkyBlue;
=======
>>>>>>> feature/Doctor
            this.ClientSize = new System.Drawing.Size(897, 577);
            this.Controls.Add(this.pnlPatient);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExaminations);
            this.Name = "PatientView";
            this.Text = "Patient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientView_FormClosing);
            this.Load += new System.EventHandler(this.PatientView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExaminations;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlPatient;
    }
}