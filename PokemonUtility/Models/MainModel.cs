using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonUtility.Models
{
    class MainModel
    {
        public String CHangeTitle()
        {
            return "test";
        }

        public bool ShowMyPartyWindow(bool isChecked)
        {
            if (isChecked) { return true; }
            return false; 
        }
    }


}
