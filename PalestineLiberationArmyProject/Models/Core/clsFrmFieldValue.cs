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
    [Table("formfieldvalue")]

    public class clsFrmFieldValue
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldCreationDate { set; get; }

        public Guid FormInstancefldUID { set; get; }
        public Guid FormFieldfldUID { set; get; }
        public string fldvalue { set; get; }

        public clsFrmInstance formInstance { set; get; }
        public clsFrmField formField { set; get; }
        public clsFrmFieldValue()
        {
            
        }


    }
}
