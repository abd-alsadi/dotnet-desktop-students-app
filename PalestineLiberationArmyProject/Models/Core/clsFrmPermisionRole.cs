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
    [Table("frompermisionrole")]

    public class clsFrmPermisionRole
    {
        [Key]
        public Guid fldUID { set; get; }
        public Guid RolefldUID { set; get; }
        public Guid FormfldUID { set; get; }
        public clsRole role { set; get; }
        public clsForm form { set; get; }
        public clsFrmPermisionRole()
        {
           
        }


    }
}
