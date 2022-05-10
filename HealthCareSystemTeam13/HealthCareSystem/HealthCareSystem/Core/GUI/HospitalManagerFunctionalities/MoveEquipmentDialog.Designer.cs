
namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    partial class MoveEquipmentDialog
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
            this.dtpExecutionDate = new System.Windows.Forms.DateTimePicker();
            this.btnMove = new System.Windows.Forms.Button();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblMoving = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.lblEquipmentName = new System.Windows.Forms.Label();
            this.lblTrueAmount = new System.Windows.Forms.Label();
            this.lblRoomId = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.lblEquipmentId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpExecutionDate
            // 
            this.dtpExecutionDate.Location = new System.Drawing.Point(201, 266);
            this.dtpExecutionDate.Name = "dtpExecutionDate";
            this.dtpExecutionDate.Size = new System.Drawing.Size(283, 22);
            this.dtpExecutionDate.TabIndex = 0;
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(330, 327);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(154, 53);
            this.btnMove.TabIndex = 2;
            this.btnMove.Text = "MOVE";
            this.btnMove.UseVisualStyleBackColor = true;
            // 
            // lblOrigin
            // 
            this.lblOrigin.AutoSize = true;
            this.lblOrigin.Location = new System.Drawing.Point(31, 61);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(82, 17);
            this.lblOrigin.TabIndex = 4;
            this.lblOrigin.Text = "Origin room";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(31, 209);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(142, 17);
            this.lblAmount.TabIndex = 5;
            this.lblAmount.Text = "Amount of equipment";
            // 
            // lblMoving
            // 
            this.lblMoving.AutoSize = true;
            this.lblMoving.Location = new System.Drawing.Point(31, 271);
            this.lblMoving.Name = "lblMoving";
            this.lblMoving.Size = new System.Drawing.Size(85, 17);
            this.lblMoving.TabIndex = 6;
            this.lblMoving.Text = "Moving date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Destination room";
            // 
            // cmbDestination
            // 
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(201, 102);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(283, 24);
            this.cmbDestination.TabIndex = 9;
            // 
            // lblEquipmentName
            // 
            this.lblEquipmentName.AutoSize = true;
            this.lblEquipmentName.Location = new System.Drawing.Point(31, 161);
            this.lblEquipmentName.Name = "lblEquipmentName";
            this.lblEquipmentName.Size = new System.Drawing.Size(114, 17);
            this.lblEquipmentName.TabIndex = 11;
            this.lblEquipmentName.Text = "Equipment name";
            // 
            // lblTrueAmount
            // 
            this.lblTrueAmount.AutoSize = true;
            this.lblTrueAmount.Location = new System.Drawing.Point(500, 161);
            this.lblTrueAmount.Name = "lblTrueAmount";
            this.lblTrueAmount.Size = new System.Drawing.Size(210, 17);
            this.lblTrueAmount.TabIndex = 12;
            this.lblTrueAmount.Text = "Available amount from this room";
            this.lblTrueAmount.Visible = false;
            // 
            // lblRoomId
            // 
            this.lblRoomId.AutoSize = true;
            this.lblRoomId.Location = new System.Drawing.Point(198, 61);
            this.lblRoomId.Name = "lblRoomId";
            this.lblRoomId.Size = new System.Drawing.Size(0, 17);
            this.lblRoomId.TabIndex = 13;
            // 
            // nudAmount
            // 
            this.nudAmount.Location = new System.Drawing.Point(201, 207);
            this.nudAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(283, 22);
            this.nudAmount.TabIndex = 14;
            this.nudAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblEquipmentId
            // 
            this.lblEquipmentId.AutoSize = true;
            this.lblEquipmentId.Location = new System.Drawing.Point(198, 161);
            this.lblEquipmentId.Name = "lblEquipmentId";
            this.lblEquipmentId.Size = new System.Drawing.Size(0, 17);
            this.lblEquipmentId.TabIndex = 15;
            // 
            // MoveEquipmentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblEquipmentId);
            this.Controls.Add(this.nudAmount);
            this.Controls.Add(this.lblRoomId);
            this.Controls.Add(this.lblTrueAmount);
            this.Controls.Add(this.lblEquipmentName);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMoving);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.dtpExecutionDate);
            this.Name = "MoveEquipmentDialog";
            this.Text = "Move Equipment";
            this.Load += new System.EventHandler(this.MoveEquipmentDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpExecutionDate;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblMoving;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.Label lblEquipmentName;
        private System.Windows.Forms.Label lblTrueAmount;
        private System.Windows.Forms.Label lblRoomId;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.Label lblEquipmentId;
    }
}