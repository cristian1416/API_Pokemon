using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Domain.Entities
{
    public class Abilities
    {
        public DetailAbility ability { get; set; }
        public bool is_hidden { get; set; }
        public int slot { get; set; }
        
    }
}
