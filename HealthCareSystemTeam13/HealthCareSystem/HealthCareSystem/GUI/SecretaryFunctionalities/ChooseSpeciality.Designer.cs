
namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class ChooseSpeciality
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
            this.label1 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.specialityComboBox = new System.Windows.Forms.ComboBox();
            this.durationBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose a speciality:";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(94, 174);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(4);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(100, 28);
            this.acceptButton.TabIndex = 32;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // specialityComboBox
            // 
            this.specialityComboBox.FormattingEnabled = true;
            this.specialityComboBox.Location = new System.Drawing.Point(44, 49);
            this.specialityComboBox.Name = "specialityComboBox";
            this.specialityComboBox.Size = new System.Drawing.Size(192, 24);
            this.specialityComboBox.TabIndex = 33;
            // 
            // durationBox
            // 
            this.durationBox.Location = new System.Drawing.Point(44, 124);
            this.durationBox.Name = "durationBox";
            this.durationBox.Size = new System.Drawing.Size(192, 22);
            this.durationBox.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "Duration:";
            // 
            // ChooseSpeciality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 215);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.durationBox);
            this.Controls.Add(this.specialityComboBox);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label1);
            this.Name = "ChooseSpeciality";
            this.Text = "ChooseSpeciality";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.ComboBox specialityComboBox;
        private System.Windows.Forms.TextBox durationBox;
        private System.Windows.Forms.Label label2;
    }
}