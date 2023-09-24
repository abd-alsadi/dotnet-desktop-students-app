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
    [Table("entityfieldvalue")]

    public class clsEntityFieldValue
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldCreationDate { set; get; }

        public Guid EntityInstancefldUID { set; get; }
        public Guid EntityFieldfldUID { set; get; }
        public string fldvalue { set; get; }

        public clsEntityInstance entityInstance { set; get; }
        public clsEntityField entityField { set; get; }
        public clsEntityFieldValue()
        {
            
        }


    }
}
