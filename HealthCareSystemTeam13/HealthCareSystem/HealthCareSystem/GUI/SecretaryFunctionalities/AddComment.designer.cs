
namespace HealthCareSystem.GUI.SecretaryFunctionalities
{
    partial class AddComment
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
            this.denyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // denyButton
            // 
            this.denyButton.Location = new System.Drawing.Point(89, 90);
            this.denyButton.Margin = new System.Windows.Forms.Padding(2);
            this.denyButton.Name = "denyButton";
            this.denyButton.Size = new System.Drawing.Size(75, 19);
            this.denyButton.TabIndex = 14;
            this.denyButton.Text = "DENY";
            this.denyButton.UseVisualStyleBackColor = true;
            this.denyButton.Click += new System.EventHandler(this.denyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Add a reason:";
            // 
            // commentTextBox
            // 
            this.commentTextBox.Location = new System.Drawing.Point(25, 53);
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(209, 20);
            this.commentTextBox.TabIndex = 16;
            // 
            // AddComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 138);
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.denyButton);
            this.Name = "AddComment";
            this.Text = "AddComment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button denyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox commentTextBox;
    }
}