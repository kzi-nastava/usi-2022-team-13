using HealthCareSystem.Core.Ingredients.Model;
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
            FillCheckBoxList();

            if (!isAddChosen)
            {
                LoadEditData();
            }
        }

        private void FillCheckBoxList()
        {
            List<Ingredient> ingredients = RoomRep.GetIngredients("select * from ingredients");
            Console.WriteLine(ingredients.Count);
            foreach(Ingredient ingredient in ingredients)
            {
                clbIngredients.Items.Add(ingredient);
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

                string medicationQuery = "select * from medications where nameOfMedication ='" + MedicationName + "'";
                Medication newMedication = RoomRep.GetSelectedMedication(medicationQuery);

                foreach(Ingredient ingredient in clbIngredients.CheckedItems)
                {
                    RoomRep.InsertMedicationContainsIngredient(newMedication.ID, ingredient.Id);
                }
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

                string updateNameAndStatusQuery = "Update medications set nameOfMedication = '" + MedicationName + "'" +
                    " and set status = '" + MedicationStatus.InProgress.ToString() + "' where id = " + MedicationId;
                RoomRep.UpdateContent(updateNameAndStatusQuery);

                //updatovanje rejeted medications
                //updatovanje medicationcontains ingredient
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
