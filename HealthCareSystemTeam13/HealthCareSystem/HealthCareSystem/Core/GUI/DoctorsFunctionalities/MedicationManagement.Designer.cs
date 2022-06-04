
namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    partial class MedicationManagement
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
            this.cbDrug = new System.Windows.Forms.ComboBox();
            this.rbAllowDrug = new System.Windows.Forms.RadioButton();
            this.lbDrug = new System.Windows.Forms.Label();
            this.rbReturnDrug = new System.Windows.Forms.RadioButton();
            this.btnDrugUsage = new System.Windows.Forms.Button();
            this.rtbDrug = new System.Windows.Forms.RichTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDrug
            // 
            this.cbDrug.FormattingEnabled = true;
            this.cbDrug.Location = new System.Drawing.Point(154, 90);
            this.cbDrug.Name = "cbDrug";
            this.cbDrug.Size = new System.Drawing.Size(121, 21);
            this.cbDrug.TabIndex = 48;
            // 
            // rbAllowDrug
            // 
            this.rbAllowDrug.AutoSize = true;
            this.rbAllowDrug.Location = new System.Drawing.Point(145, 158);
            this.rbAllowDrug.Name = "rbAllowDrug";
            this.rbAllowDrug.Size = new System.Drawing.Size(50, 17);
            this.rbAllowDrug.TabIndex = 45;
            this.rbAllowDrug.TabStop = true;
            this.rbAllowDrug.Text = "Allow";
            this.rbAllowDrug.UseVisualStyleBackColor = true;
            // 
            // lbDrug
            // 
            this.lbDrug.AutoSize = true;
            this.lbDrug.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDrug.Location = new System.Drawing.Point(65, 24);
            this.lbDrug.Name = "lbDrug";
            this.lbDrug.Size = new System.Drawing.Size(322, 25);
            this.lbDrug.TabIndex = 50;
            this.lbDrug.Text = "Allow or return the selected drug";
            // 
            // rbReturnDrug
            // 
            this.rbReturnDrug.AutoSize = true;
            this.rbReturnDrug.Location = new System.Drawing.Point(233, 158);
            this.rbReturnDrug.Name = "rbReturnDrug";
            this.rbReturnDrug.Size = new System.Drawing.Size(57, 17);
            this.rbReturnDrug.TabIndex = 46;
            this.rbReturnDrug.TabStop = true;
            this.rbReturnDrug.Text = "Return";
            this.rbReturnDrug.UseVisualStyleBackColor = true;
            // 
            // btnDrugUsage
            // 
            this.btnDrugUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrugUsage.Location = new System.Drawing.Point(25, 391);
            this.btnDrugUsage.Margin = new System.Windows.Forms.Padding(4);
            this.btnDrugUsage.Name = "btnDrugUsage";
            this.btnDrugUsage.Size = new System.Drawing.Size(221, 31);
            this.btnDrugUsage.TabIndex = 49;
            this.btnDrugUsage.Text = "Allow/Return the drug";
            this.btnDrugUsage.UseVisualStyleBackColor = true;
            // 
            // rtbDrug
            // 
            this.rtbDrug.Location = new System.Drawing.Point(119, 209);
            this.rtbDrug.Name = "rtbDrug";
            this.rtbDrug.Size = new System.Drawing.Size(197, 117);
            this.rtbDrug.TabIndex = 47;
            this.rtbDrug.Text = "";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(287, 391);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 31);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DrugManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 447);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbDrug);
            this.Controls.Add(this.rbAllowDrug);
            this.Controls.Add(this.lbDrug);
            this.Controls.Add(this.rbReturnDrug);
            this.Controls.Add(this.btnDrugUsage);
            this.Controls.Add(this.rtbDrug);
            this.Name = "DrugManagment";
            this.Text = "DrugManagment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDrug;
        private System.Windows.Forms.RadioButton rbAllowDrug;
        private System.Windows.Forms.Label lbDrug;
        private System.Windows.Forms.RadioButton rbReturnDrug;
        private System.Windows.Forms.Button btnDrugUsage;
        private System.Windows.Forms.RichTextBox rtbDrug;
        private System.Windows.Forms.Button btnCancel;
    }
}