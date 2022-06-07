
namespace HealthCareSystem.GUI.DoctorsFunctionalities
{
    partial class RequestDaysOff
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
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.rtbReasonForRequest = new System.Windows.Forms.RichTextBox();
            this.cbUrgent = new System.Windows.Forms.CheckBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lbFirstDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvPastRequests = new System.Windows.Forms.ListView();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbPastRequests = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(18, 143);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 0;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(302, 143);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpEnd.TabIndex = 1;
            // 
            // rtbReasonForRequest
            // 
            this.rtbReasonForRequest.Location = new System.Drawing.Point(179, 199);
            this.rtbReasonForRequest.Name = "rtbReasonForRequest";
            this.rtbReasonForRequest.Size = new System.Drawing.Size(176, 96);
            this.rtbReasonForRequest.TabIndex = 2;
            this.rtbReasonForRequest.Text = "";
            // 
            // cbUrgent
            // 
            this.cbUrgent.AutoSize = true;
            this.cbUrgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUrgent.Location = new System.Drawing.Point(169, 322);
            this.cbUrgent.Name = "cbUrgent";
            this.cbUrgent.Size = new System.Drawing.Size(251, 24);
            this.cbUrgent.TabIndex = 3;
            this.cbUrgent.Text = "Is taking these days off urgent?";
            this.cbUrgent.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(219, 370);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(96, 39);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // lbFirstDate
            // 
            this.lbFirstDate.AutoSize = true;
            this.lbFirstDate.Location = new System.Drawing.Point(96, 115);
            this.lbFirstDate.Name = "lbFirstDate";
            this.lbFirstDate.Size = new System.Drawing.Size(53, 13);
            this.lbFirstDate.TabIndex = 5;
            this.lbFirstDate.Text = "Start date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(382, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ending time";
            // 
            // lvPastRequests
            // 
            this.lvPastRequests.HideSelection = false;
            this.lvPastRequests.Location = new System.Drawing.Point(623, 143);
            this.lvPastRequests.Name = "lvPastRequests";
            this.lvPastRequests.Size = new System.Drawing.Size(253, 117);
            this.lvPastRequests.TabIndex = 8;
            this.lvPastRequests.UseCompatibleStateImageBehavior = false;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(247, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(255, 37);
            this.lbTitle.TabIndex = 51;
            this.lbTitle.Text = "Request days off";
            // 
            // lbPastRequests
            // 
            this.lbPastRequests.AutoSize = true;
            this.lbPastRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPastRequests.Location = new System.Drawing.Point(617, 88);
            this.lbPastRequests.Name = "lbPastRequests";
            this.lbPastRequests.Size = new System.Drawing.Size(276, 31);
            this.lbPastRequests.TabIndex = 52;
            this.lbPastRequests.Text = "See my past requests";
            // 
            // RequestDaysOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 430);
            this.Controls.Add(this.lbPastRequests);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lvPastRequests);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbFirstDate);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbUrgent);
            this.Controls.Add(this.rtbReasonForRequest);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Name = "RequestDaysOff";
            this.Text = "RequestDaysOff";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.RichTextBox rtbReasonForRequest;
        private System.Windows.Forms.CheckBox cbUrgent;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lbFirstDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvPastRequests;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbPastRequests;
    }
}