using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Rooms.Repository;
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
        private readonly DoctorRepository _doctorRep;
        private readonly RoomRepository _roomRep;
        private readonly ExaminationRepository _examinationRep;

        private int _examinationId;
        private int _roomId;
        public SetUsedDynamicEquipment(int examinationId, string doctorUsername)
        {
            InitializeComponent();

            _doctorRep = new DoctorRepository(doctorUsername, true);
            _examinationRep = new ExaminationRepository();
            _roomRep = new RoomRepository();
            _examinationId = examinationId;
            _roomId = _examinationRep.GetRoomIdFromExaminationId(examinationId);
            PullEquipment();
        }

        public void PullEquipment()
        {
            
            lbDynamicEquipment.ValueMember = null;
            List<Equipment> equipment = _roomRep.GetEquipmentFromRoomId(_roomId);

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

            int amountToReduceBy = Convert.ToInt32(tbNumber.Text);
            if(amountToReduceBy <= 0)
            {
                MessageBox.Show("Number has to be greater then zero!");
                return;
            }
            Equipment selectedEquipment = (Equipment)lbDynamicEquipment.SelectedValue;
            int amount = selectedEquipment.Amount - amountToReduceBy;

            if(amount < 0)
            {
                MessageBox.Show("You can't throw out more equipment then what the room already has!");
                return;
            }

            _roomRep.UpdateAmountOfEquipmentInTheRoom(amount, _roomId, selectedEquipment.ID);
            PullEquipment();
            MessageBox.Show("Successfully thrown out equipment that was used in the examination.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
