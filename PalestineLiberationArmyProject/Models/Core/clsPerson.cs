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
    [Table("person")]

    public class clsPerson
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
       public string fldNumber { set; get; }

        public string fldCreationDate { set; get; }
        public List<clsTransaction> transactions { set; get; }

        public clsPerson()
        {
            transactions = new List<clsTransaction>();
        }

    }
}
