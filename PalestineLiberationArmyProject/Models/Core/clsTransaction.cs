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
    [Table("transaction")]

    public class clsTransaction
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldtype { set; get; }
        public string fldtext { set; get; }
        public string fldCreationDate { set; get; }
        public string fldDate { set; get; }

        public Guid personflduid { set; get; }
        public clsPerson person { set; get; }
        public clsTransaction()
        {
            
        }

    }
}
