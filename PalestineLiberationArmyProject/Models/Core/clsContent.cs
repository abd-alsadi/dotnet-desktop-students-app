using PalestineLiberationArmyProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PalestineLiberationArmyProject.Models.Core
{
    public class clsContent
    {
        public string fldID { set; get; }
        public string fldText { set; get; }

        public string fldFormID { set; get; }
        public clsForm form { set; get; }
        public clsContent()
        {
        
        }


    }
}
