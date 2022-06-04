using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    public partial class SetUsedDynamicEquipment : Form
    {
        private readonly DoctorRepository DoctorRep;
        private int ExaminationId;
        private int RoomId;
        public SetUsedDynamicEquipment(int examinationId, string doctorUsername)
        {
            InitializeComponent();

            DoctorRep = new DoctorRepository(doctorUsername, true);
            ExaminationId = examinationId;
            RoomId = DoctorRep.GetRoomIdFromExaminationId(examinationId);
            PullEquipment();
        }

        public void PullEquipment()
        {
            
            lbDynamicEquipment.ValueMember = null;
            List<Equipment> equipment = DoctorRep.GetEquipmentFromRoomId(RoomId);

            lbDynamicEquipment.DisplayMember = "NameAndAmount";
            lbDynamicEquipment.DataSource = equipment;
        }

        private void btnUpdateDynamicEquipment_Click(object sender, EventArgs e)
        {
            var isNumeric = int.TryParse(tbNumber.Text, out _);

            if (tbNumber.Text == "" || tbNumber.Text == " " || !isNumeric)
            {
                MessageBox.Show("You need to insert a numeric value");
                return;
            }

            Equipment selectedEquipment = (Equipment)lbDynamicEquipment.SelectedValue;
            int amount = selectedEquipment.Amount - Convert.ToInt32(tbNumber.Text);
            DoctorRep.UpdateAmountOfEquipmentInTheRoom(amount, RoomId, selectedEquipment.ID);
            MessageBox.Show("Successfully thrown out equipment that was used in the examination.");
            PullEquipment();

        }
    }
}
