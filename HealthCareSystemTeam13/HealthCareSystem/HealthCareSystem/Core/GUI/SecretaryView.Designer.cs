
namespace HealthCareSystem.Core.GUI
{
    partial class SecretaryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecretaryView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.requestsButton = new System.Windows.Forms.Button();
            this.blockedPatientsButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.patientsButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.patientsButton);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.requestsButton);
            this.panel1.Controls.Add(this.blockedPatientsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 554);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(74, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // requestsButton
            // 
            this.requestsButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.requestsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.requestsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.requestsButton.ForeColor = System.Drawing.Color.White;
            this.requestsButton.Location = new System.Drawing.Point(26, 267);
            this.requestsButton.Name = "requestsButton";
            this.requestsButton.Size = new System.Drawing.Size(209, 38);
            this.requestsButton.TabIndex = 2;
            this.requestsButton.Text = "Requests";
            this.requestsButton.UseVisualStyleBackColor = false;
            this.requestsButton.Click += new System.EventHandler(this.requestsButton_Click);
            // 
            // blockedPatientsButton
            // 
            this.blockedPatientsButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.blockedPatientsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blockedPatientsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.blockedPatientsButton.ForeColor = System.Drawing.Color.White;
            this.blockedPatientsButton.Location = new System.Drawing.Point(26, 223);
            this.blockedPatientsButton.Name = "blockedPatientsButton";
            this.blockedPatientsButton.Size = new System.Drawing.Size(209, 38);
            this.blockedPatientsButton.TabIndex = 1;
            this.blockedPatientsButton.Text = "Blocked patients";
            this.blockedPatientsButton.UseVisualStyleBackColor = false;
            this.blockedPatientsButton.Click += new System.EventHandler(this.blockedPatientsButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(266, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(801, 554);
            this.mainPanel.TabIndex = 1;
            // 
            // patientsButton
            // 
            this.patientsButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.patientsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.patientsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.patientsButton.ForeColor = System.Drawing.Color.White;
            this.patientsButton.Location = new System.Drawing.Point(26, 179);
            this.patientsButton.Name = "patientsButton";
            this.patientsButton.Size = new System.Drawing.Size(209, 38);
            this.patientsButton.TabIndex = 4;
            this.patientsButton.Text = "Patients";
            this.patientsButton.UseVisualStyleBackColor = false;
            this.patientsButton.Click += new System.EventHandler(this.patientsButton_Click);
            // 
            // SecretaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SecretaryView";
            this.Text = "SECRETARY";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecretaryView_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button requestsButton;
        private System.Windows.Forms.Button blockedPatientsButton;
        private System.Windows.Forms.Button patientsButton;
    }
}