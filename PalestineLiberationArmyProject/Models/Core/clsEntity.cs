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
    [Table("entity")]
    public class clsEntity 
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
        public string fldIcon { set; get; }
        public string fldOrder { set; get; }
        public string fldDescription { set; get; }
        public string fldCreationDate { set; get; }
        public List<clsEntityField> entityFields { set; get; }
        public clsEntity()
        {
            entityFields = new List<clsEntityField>();
        }

    }
}
