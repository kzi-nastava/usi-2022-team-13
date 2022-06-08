
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class BlockPatient
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
            this.patientIdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // patientIdBox
            // 
            this.patientIdBox.Location = new System.Drawing.Point(111, 49);
            this.patientIdBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.patientIdBox.Name = "patientIdBox";
            this.patientIdBox.Size = new System.Drawing.Size(100, 20);
            this.patientIdBox.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Patient ID";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(88, 105);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 35;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // BlockPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 176);
            this.Controls.Add(this.patientIdBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.acceptButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BlockPatient";
            this.Text = "BlockPatient";
            this.Load += new System.EventHandler(this.BlockPatient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox patientIdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button acceptButton;
    }
}