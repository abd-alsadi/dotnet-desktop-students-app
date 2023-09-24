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
    [Table("formfield")]
    public class clsFrmField
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
        public string fldOrder { set; get; }
        public string fldDescription { set; get; }
        public string fldCreationDate { set; get; }
        public string fldDataType { set; get; }
        public string fldIsRequired { set; get; }
        public string fldNamex { set; get; }

        public Guid formflduid { set; get; }
        public clsForm form { set; get; }
        public clsFrmField()
        {
        }


    }
}
