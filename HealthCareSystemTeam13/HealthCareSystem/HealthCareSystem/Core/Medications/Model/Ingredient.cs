using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Ingredients.Model
{
    class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
        public Ingredient()
        {

        }

        public Ingredient(string name)
        {
            this.Id = 0;
            this.Name = name;
        }
        public Ingredient(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
