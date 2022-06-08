using HealthCareSystem.Core.Ingredients.Model;
using HealthCareSystem.Core.Medications.Repository;
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
    public partial class AddEditIngredients : Form
    {
        public int IngredientId { get; set; }
        public bool IsAddChosen { get; set; }
        private IngredientsRepository IngredientRep;
        private string IngredientName;

        public AddEditIngredients(int ingredientId, bool isAddChoosen)
        {
            IngredientRep = new IngredientsRepository();
            IngredientId = ingredientId;
            IsAddChosen = isAddChoosen;
            InitializeComponent();

            if (!IsAddChosen)
            {
                LoadEditData();
            }
        }

        private void LoadEditData()
        {
            string query = "select * from ingredients where id=" + IngredientId;
            Ingredient ingredient = IngredientRep.GetSelectedIngredient(query);
            tbIngredient.Text = ingredient.Name;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsAddChosen)
            {
                IngredientName = tbIngredient.Text;
                if (!IngredientRep.DoesIngredientExists(IngredientName))
                {
                    MessageBox.Show("There already exists ingredient with this name!");
                    this.Close();
                    return;
                    
                }


                IngredientRep.InsertIngredient(IngredientName);
                MessageBox.Show("Successfully added an ingredient!");

            }
            else
            {
                IngredientName = tbIngredient.Text;
                if (!IngredientRep.DoesIngredientExists(IngredientName))
                {
                    MessageBox.Show("There already exists ingredient with this name!");
                    this.Close();
                    return;
                }

                string updateQuery = "Update Ingredients set nameOfIngredient = '" + IngredientName + "' where id = " + IngredientId;
                DatabaseCommander.ExecuteNonQueries(updateQuery, IngredientRep.Connection);
                MessageBox.Show("Successfully edited an ingredient!");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
