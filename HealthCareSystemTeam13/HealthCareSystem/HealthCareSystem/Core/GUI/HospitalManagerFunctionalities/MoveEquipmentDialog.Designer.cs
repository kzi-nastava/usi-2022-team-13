
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
            this.txbAmount = new System.Windows.Forms.TextBox();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblMoving = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOrigin = new System.Windows.Forms.ComboBox();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAmountInRoom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpExecutionDate
            // 
            this.dtpExecutionDate.Location = new System.Drawing.Point(311, 263);
            this.dtpExecutionDate.Name = "dtpExecutionDate";
            this.dtpExecutionDate.Size = new System.Drawing.Size(200, 22);
            this.dtpExecutionDate.TabIndex = 0;
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(348, 327);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(136, 83);
            this.btnMove.TabIndex = 2;
            this.btnMove.Text = "MOVE";
            this.btnMove.UseVisualStyleBackColor = true;
            // 
            // txbAmount
            // 
            this.txbAmount.Location = new System.Drawing.Point(311, 201);
            this.txbAmount.Name = "txbAmount";
            this.txbAmount.Size = new System.Drawing.Size(200, 22);
            this.txbAmount.TabIndex = 3;
            // 
            // lblOrigin
            // 
            this.lblOrigin.AutoSize = true;
            this.lblOrigin.Location = new System.Drawing.Point(141, 58);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(82, 17);
            this.lblOrigin.TabIndex = 4;
            this.lblOrigin.Text = "Origin room";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(141, 206);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(142, 17);
            this.lblAmount.TabIndex = 5;
            this.lblAmount.Text = "Amount of equipment";
            // 
            // lblMoving
            // 
            this.lblMoving.AutoSize = true;
            this.lblMoving.Location = new System.Drawing.Point(141, 268);
            this.lblMoving.Name = "lblMoving";
            this.lblMoving.Size = new System.Drawing.Size(85, 17);
            this.lblMoving.TabIndex = 6;
            this.lblMoving.Text = "Moving date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Destination room";
            // 
            // cmbOrigin
            // 
            this.cmbOrigin.FormattingEnabled = true;
            this.cmbOrigin.Location = new System.Drawing.Point(311, 51);
            this.cmbOrigin.Name = "cmbOrigin";
            this.cmbOrigin.Size = new System.Drawing.Size(200, 24);
            this.cmbOrigin.TabIndex = 8;
            // 
            // cmbDestination
            // 
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(311, 99);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(200, 24);
            this.cmbDestination.TabIndex = 9;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(311, 151);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 24);
            this.cmbType.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Equipment name";
            // 
            // lblAmountInRoom
            // 
            this.lblAmountInRoom.AutoSize = true;
            this.lblAmountInRoom.Location = new System.Drawing.Point(537, 204);
            this.lblAmountInRoom.Name = "lblAmountInRoom";
            this.lblAmountInRoom.Size = new System.Drawing.Size(210, 17);
            this.lblAmountInRoom.TabIndex = 12;
            this.lblAmountInRoom.Text = "Available amount from this room";
            this.lblAmountInRoom.Visible = false;
            // 
            // MoveEquipmentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblAmountInRoom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbDestination);
            this.Controls.Add(this.cmbOrigin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMoving);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.txbAmount);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.dtpExecutionDate);
            this.Name = "MoveEquipmentDialog";
            this.Text = "Move Equipment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpExecutionDate;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.TextBox txbAmount;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblMoving;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbOrigin;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAmountInRoom;
    }
}