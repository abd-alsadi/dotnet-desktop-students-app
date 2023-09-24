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
    [Table("form")]

    public class clsForm 
    {
        [Key]
        public Guid fldUID { set; get; }
        public string fldName { set; get; }
        public string fldIcon { set; get; }
        public string fldOrder { set; get; }
        public string fldDescription { set; get; }
        public string fldCreationDate { set; get; }
        public List<clsFrmField> formFields { set; get; }
        public string fldContent { set; get; }
        //public clsContent content { set; get; }
        public clsForm()
        {
            formFields = new List<clsFrmField>();
           // content = new clsContent();
        }

    }
}
