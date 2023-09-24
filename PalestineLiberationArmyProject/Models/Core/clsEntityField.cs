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
    [Table("entityfield")]
    public class clsEntityField
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
        public string fldOrder { set; get; }
        public string fldDescription { set; get; }
        public string fldCreationDate { set; get; }
        public string fldDataType { set; get; }
        public string fldIsRequired { set; get; }

        public Guid entityflduid { set; get; }
        public clsEntity entity { set; get; }
        public clsEntityField()
        {
        }


    }
}
