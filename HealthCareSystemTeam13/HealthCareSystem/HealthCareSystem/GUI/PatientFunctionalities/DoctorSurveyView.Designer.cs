
namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    partial class DoctorSurveyView
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
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.cbRating = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbReccomendNo = new System.Windows.Forms.RadioButton();
            this.rbReccomendYes = new System.Windows.Forms.RadioButton();
            this.rtbComment = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbQuality
            // 
            this.cbQuality.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cbQuality.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQuality.FormattingEnabled = true;
            this.cbQuality.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbQuality.Location = new System.Drawing.Point(181, 190);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(121, 28);
            this.cbQuality.TabIndex = 34;
            // 
            // cbRating
            // 
            this.cbRating.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cbRating.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRating.FormattingEnabled = true;
            this.cbRating.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbRating.Location = new System.Drawing.Point(181, 147);
            this.cbRating.Name = "cbRating";
            this.cbRating.Size = new System.Drawing.Size(121, 28);
            this.cbRating.TabIndex = 33;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbReccomendNo);
            this.groupBox1.Controls.Add(this.rbReccomendYes);
            this.groupBox1.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(57, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 77);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Would you reccomend?";
            // 
            // rbReccomendNo
            // 
            this.rbReccomendNo.AutoSize = true;
            this.rbReccomendNo.Location = new System.Drawing.Point(147, 29);
            this.rbReccomendNo.Name = "rbReccomendNo";
            this.rbReccomendNo.Size = new System.Drawing.Size(49, 24);
            this.rbReccomendNo.TabIndex = 20;
            this.rbReccomendNo.TabStop = true;
            this.rbReccomendNo.Text = "No";
            this.rbReccomendNo.UseVisualStyleBackColor = true;
            // 
            // rbReccomendYes
            // 
            this.rbReccomendYes.AutoSize = true;
            this.rbReccomendYes.Location = new System.Drawing.Point(16, 29);
            this.rbReccomendYes.Name = "rbReccomendYes";
            this.rbReccomendYes.Size = new System.Drawing.Size(54, 24);
            this.rbReccomendYes.TabIndex = 19;
            this.rbReccomendYes.TabStop = true;
            this.rbReccomendYes.Text = "Yes";
            this.rbReccomendYes.UseVisualStyleBackColor = true;
            // 
            // rtbComment
            // 
            this.rtbComment.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rtbComment.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbComment.Location = new System.Drawing.Point(57, 361);
            this.rtbComment.Name = "rtbComment";
            this.rtbComment.Size = new System.Drawing.Size(262, 111);
            this.rtbComment.TabIndex = 30;
            this.rtbComment.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(53, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "Leave a comment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Quality";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Doctor Rating";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Bright", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(138, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 31);
            this.label1.TabIndex = 26;
            this.label1.Text = "Doctor Survey";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancel.FlatAppearance.BorderSize = 3;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(141, 500);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 40);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSend.FlatAppearance.BorderSize = 3;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(291, 480);
            this.btnSend.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(174, 60);
            this.btnSend.TabIndex = 24;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // DoctorSurveyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(480, 562);
            this.Controls.Add(this.cbQuality);
            this.Controls.Add(this.cbRating);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbComment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Name = "DoctorSurveyView";
            this.Text = "DoctorSurveyView";
            this.Load += new System.EventHandler(this.DoctorSurveyView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbQuality;
        private System.Windows.Forms.ComboBox cbRating;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbReccomendNo;
        private System.Windows.Forms.RadioButton rbReccomendYes;
        private System.Windows.Forms.RichTextBox rtbComment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSend;
    }
}