
namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    partial class AddEditRooms
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
            this.cbRoomTypes = new System.Windows.Forms.ComboBox();
            this.labelRoomType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 212);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(180, 212);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 28);
            this.btnAccept.TabIndex = 12;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // cbRoomTypes
            // 
            this.cbRoomTypes.FormattingEnabled = true;
            this.cbRoomTypes.Location = new System.Drawing.Point(267, 87);
            this.cbRoomTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cbRoomTypes.Name = "cbRoomTypes";
            this.cbRoomTypes.Size = new System.Drawing.Size(160, 24);
            this.cbRoomTypes.TabIndex = 15;
            this.cbRoomTypes.SelectedIndexChanged += new System.EventHandler(this.cbRoomTypes_SelectedIndexChanged);
            // 
            // labelRoomType
            // 
            this.labelRoomType.AutoSize = true;
            this.labelRoomType.Location = new System.Drawing.Point(177, 94);
            this.labelRoomType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRoomType.Name = "labelRoomType";
            this.labelRoomType.Size = new System.Drawing.Size(81, 17);
            this.labelRoomType.TabIndex = 14;
            this.labelRoomType.Text = "Room Type";
            // 
            // AddEditRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 269);
            this.Controls.Add(this.cbRoomTypes);
            this.Controls.Add(this.labelRoomType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "AddEditRooms";
            this.Text = "AddEditRooms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.ComboBox cbRoomTypes;
        private System.Windows.Forms.Label labelRoomType;
    }
}