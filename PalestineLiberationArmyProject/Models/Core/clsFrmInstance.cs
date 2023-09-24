using PalestineLiberationArmyProject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PalestineLiberationArmyProject.Models.Core
{
    [Table("forminstance")]

    public class clsFrmInstance
    {
        [Key]
        public Guid fldUID { set; get; }
        public Guid formFldUID { set; get; }
        public string fldCreationDate { set; get; }
        public Guid userFldUID { set; get; }
        public clsUser user { set; get; }
        public clsForm form { set; get; }
        public List<clsFrmFieldValue> formFieldValue { set; get; }
        public clsFrmInstance()
        {
            formFieldValue = new List<clsFrmFieldValue>();
        }


    }
}
