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
    [Table("user")]

    public class clsUser
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
        public string fldEmail { set; get; }
        public string fldPassword { set; get; }

        public string fldCreationDate { set; get; }
        public clsRole role { set; get; }
        public Guid rolefldUID { set; get; }
        public List<clsFrmInstance> instances { set; get; }

        public clsUser()
        {
            role = new clsRole();
            instances = new List<clsFrmInstance>();
        }

    }
}
