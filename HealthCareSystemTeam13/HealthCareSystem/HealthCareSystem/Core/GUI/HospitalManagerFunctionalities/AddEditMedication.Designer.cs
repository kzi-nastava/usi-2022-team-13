
namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    partial class AddEditMedication
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblMedicationIngredients = new System.Windows.Forms.Label();
            this.tbMedication = new System.Windows.Forms.TextBox();
            this.clbIngredients = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 212);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(135, 212);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 28);
            this.btnAccept.TabIndex = 14;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(132, 70);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(115, 17);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "Medication name";
            // 
            // lblMedicationIngredients
            // 
            this.lblMedicationIngredients.AutoSize = true;
            this.lblMedicationIngredients.Location = new System.Drawing.Point(132, 113);
            this.lblMedicationIngredients.Name = "lblMedicationIngredients";
            this.lblMedicationIngredients.Size = new System.Drawing.Size(150, 17);
            this.lblMedicationIngredients.TabIndex = 17;
            this.lblMedicationIngredients.Text = "Medication ingredients";
            // 
            // tbMedication
            // 
            this.tbMedication.Location = new System.Drawing.Point(298, 70);
            this.tbMedication.Name = "tbMedication";
            this.tbMedication.Size = new System.Drawing.Size(129, 22);
            this.tbMedication.TabIndex = 18;
            // 
            // clbIngredients
            // 
            this.clbIngredients.FormattingEnabled = true;
            this.clbIngredients.Location = new System.Drawing.Point(307, 113);
            this.clbIngredients.Name = "clbIngredients";
            this.clbIngredients.Size = new System.Drawing.Size(120, 72);
            this.clbIngredients.TabIndex = 19;
            // 
            // AddEditMedication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 269);
            this.Controls.Add(this.clbIngredients);
            this.Controls.Add(this.tbMedication);
            this.Controls.Add(this.lblMedicationIngredients);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "AddEditMedication";
            this.Text = " AddEditMedications";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMedicationIngredients;
        private System.Windows.Forms.TextBox tbMedication;
        private System.Windows.Forms.CheckedListBox clbIngredients;
    }
}