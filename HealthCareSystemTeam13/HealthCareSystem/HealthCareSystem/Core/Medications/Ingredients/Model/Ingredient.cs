using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Ingredients.Model
{
    class Ingredient
    {
        public string Name { get; set; }
    
        public Ingredient()
        {

        }
        public Ingredient(string name)
        {
            this.Name = name;
        }
    }
}
