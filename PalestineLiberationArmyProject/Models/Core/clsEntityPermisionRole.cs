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
    [Table("entitypermisionrole")]

    public class clsEntityPermisionRole
    {
        [Key]
        public string fldUID { set; get; }
        public string fldRoleUID { set; get; }
        public string fldEntityuID { set; get; }
        public clsRole role { set; get; }
        public clsEntity entity { set; get; }
        public clsEntityPermisionRole()
        {
          
        }


    }
}
