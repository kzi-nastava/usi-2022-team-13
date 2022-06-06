
namespace HealthCareSystem.GUI.HospitalManagerFunctionalities
{
    partial class HospitalSurveys
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
            this.dgwHospitalSurveys = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwHospitalSurveys)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwHospitalSurveys
            // 
            this.dgwHospitalSurveys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwHospitalSurveys.Location = new System.Drawing.Point(4, 13);
            this.dgwHospitalSurveys.Margin = new System.Windows.Forms.Padding(4);
            this.dgwHospitalSurveys.Name = "dgwHospitalSurveys";
            this.dgwHospitalSurveys.RowHeadersWidth = 51;
            this.dgwHospitalSurveys.RowTemplate.Height = 24;
            this.dgwHospitalSurveys.Size = new System.Drawing.Size(737, 419);
            this.dgwHospitalSurveys.TabIndex = 0;
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
            // HospitalSurveys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(795, 556);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgwHospitalSurveys);
            this.Font = new System.Drawing.Font("Lucida Bright", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "HospitalSurveys";
            this.Text = "HospitalSurveysView";
            ((System.ComponentModel.ISupportInitialize)(this.dgwHospitalSurveys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwHospitalSurveys;
        private System.Windows.Forms.Button btnRefresh;
    }
}