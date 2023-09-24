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
    [Table("entityinstance")]

    public class clsEntityInstance
    {
        [Key]
        public Guid fldUID { set; get; }
        public Guid entityFldUID { set; get; }
        public string fldCreationDate { set; get; }
        public Guid userFldUID { set; get; }
        public clsUser user { set; get; }
        public clsEntity entity { set; get; }
        public List<clsEntityFieldValue> entityFieldValue { set; get; }
        public clsEntityInstance()
        {
            entityFieldValue = new List<clsEntityFieldValue>();
        }


    }
}
