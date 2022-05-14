
namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    partial class Renovations
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
            this.dgwRenovations = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewRenovation = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRenovations)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwRenovations
            // 
            this.dgwRenovations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwRenovations.Location = new System.Drawing.Point(4, 13);
            this.dgwRenovations.Margin = new System.Windows.Forms.Padding(4);
            this.dgwRenovations.Name = "dgwRenovations";
            this.dgwRenovations.RowHeadersWidth = 51;
            this.dgwRenovations.RowTemplate.Height = 24;
            this.dgwRenovations.Size = new System.Drawing.Size(737, 419);
            this.dgwRenovations.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(14, 462);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(137, 47);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnNewRenovation
            // 
            this.btnNewRenovation.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Bold);
            this.btnNewRenovation.Location = new System.Drawing.Point(184, 462);
            this.btnNewRenovation.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewRenovation.Name = "btnNewRenovation";
            this.btnNewRenovation.Size = new System.Drawing.Size(179, 47);
            this.btnNewRenovation.TabIndex = 2;
            this.btnNewRenovation.Text = "Add renovation";
            this.btnNewRenovation.UseVisualStyleBackColor = true;
            this.btnNewRenovation.Click += new System.EventHandler(this.btnNewRenovation_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(402, 462);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 47);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Renovations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 556);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNewRenovation);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgwRenovations);
            this.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Renovations";
            this.Text = "Renovations";
            ((System.ComponentModel.ISupportInitialize)(this.dgwRenovations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwRenovations;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNewRenovation;
        private System.Windows.Forms.Button button1;
    }
}