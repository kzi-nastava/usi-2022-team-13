using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Model
{
    class MedicationsIngredient
    {
       
        public int MedicationID { get; set; }
        public int IngredientID { get; set; }
       
        public MedicationsIngredient()
        {

        }
        public MedicationsIngredient(int medicationID, int ingredientID)
        {
            this.MedicationID = medicationID;
            this.IngredientID = ingredientID;
        }
    }
}
