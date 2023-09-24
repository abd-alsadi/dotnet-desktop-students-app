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
    [Table("role")]

    public class clsRole
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
        public string fldDescription { set; get; }
        public string fldCreationDate { set; get; }

        public List<clsUser> users { set; get; }
        public List<clsFrmPermisionRole> formPermisionsRoles { set; get; }
        public clsRole()
        {
            users = new List<clsUser>();
            formPermisionsRoles = new List<clsFrmPermisionRole>();
        }

    }
}
