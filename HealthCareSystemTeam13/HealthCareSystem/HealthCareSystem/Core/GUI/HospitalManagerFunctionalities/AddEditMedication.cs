using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Rooms.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    public partial class AddEditMedication : Form
    {
        public int MedicationId { get; set; }
        private RoomRepository RoomRep;
        private string MedicationName;

        private bool IsAddChosen { get; set; }

        public AddEditMedication(int medicationId, bool isAddChosen)
        {
            MedicationId = medicationId;
            IsAddChosen = isAddChosen;
            RoomRep = new RoomRepository();
            InitializeComponent();

            if (!isAddChosen)
            {
                LoadEditData();
            }
        }

        private void LoadEditData()
        {
            string query = "select * from medications where id=" + MedicationId;
            Medication medication = RoomRep.GetSelectedMedication(query);
            //get ingredients from medication
            tbMedication.Text = medication.Name;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsAddChosen)
            {
                MedicationName = tbMedication.Text;
                
                if (!RoomRep.DoesMedicationExists(MedicationName))
                {
                    MessageBox.Show("There already exists medication with this name!");
                    this.Close();
                    return;

                }


                RoomRep.InsertMedication(MedicationName);
                //insert new medicationhasingredients
                MessageBox.Show("Successfully added a medication!");

            }
            else
            {
                MedicationName = tbMedication.Text;
                if (!RoomRep.DoesMedicationExists(MedicationName))
                {
                    MessageBox.Show("There already exists medication with this name!");
                    this.Close();
                    return;
                }

                string updateQuery = "Update medications set nameOfIngredient = '" + MedicationName + "' where id = " + MedicationId;
                //updatovanje rejeted medications
                //updatovanje medicationcontains ingredient
                RoomRep.UpdateContent(updateQuery);
                MessageBox.Show("Successfully edited a medication!");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
