using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalestineLiberationArmyProject.Models.Core;
using PalestineLiberationArmyProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PalestineLiberationArmyProject.Models;

namespace PalestineLiberationArmyProject.Controllers
{
    public class FormFieldController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public FormFieldController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldformuid ,string fldparameter)
        {
            ViewBag.fldformuid = fldformuid;
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                
                Guid id = new Guid(fldformuid);
                var fields = (from field in dbManager.FormField.ToList()
                                where field.formflduid==id
                                select field).ToList();
                return View(fields);
            }
            else
            {
                fldparameter = fldparameter.Trim();
                Guid id = new Guid(fldformuid);
                var fields = (from field in dbManager.FormField.Where<clsFrmField>(e=>e.formflduid == id).ToList()
                                     where field.fldName.Contains(fldparameter) || field.fldDescription.Contains(fldparameter) || field.fldCreationDate.Contains(fldparameter) || field.fldDataType.Contains(fldparameter) || field.fldNamex.Contains(fldparameter)
                              select field).ToList();

                return View(fields);
            }
   
        }
        [HttpPost]
        public IActionResult Add(string fldformuid, string fldname,string flddescription,string fldorder,string fldisrequired,string flddatatype,string fldnamex)
        {
            string date = DateTime.Now.ToShortDateString();

            string isreq = "0";
            if (fldisrequired == "on")
            {
                isreq = "1";
            }
            Guid id = new Guid(fldformuid);
            clsFrmField model=new clsFrmField{fldCreationDate=date,fldName=fldname,fldIsRequired= isreq, fldDescription=flddescription,fldOrder=fldorder,fldDataType=flddatatype, formflduid = id,fldNamex=fldnamex };

            dbManager.FormField.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new { fldformuid = fldformuid });
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldformuid, string fldname, string flddescription, string fldorder, string flddatatype,string fldCreationDate,string fldisrequired,string fldnamex)
        {
            Guid uid = new Guid(flduid);
            
            clsFrmField model = (from field in dbManager.FormField
                                where field.fldUID == uid
                             select field).SingleOrDefault();
            model.fldOrder = fldorder;
            model.fldNamex = fldnamex;
            model.fldDataType = flddatatype;
            model.fldDescription = flddescription;
            model.fldName = fldname;
            model.fldIsRequired = "0";
            if (fldisrequired == "on")
            {
                model.fldIsRequired = "1";
            }
            
            dbManager.FormField.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new { fldformuid = fldformuid });
        }
        public IActionResult Delete(string flduid,string fldformuid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsFrmField> models = dbManager.FormField.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.FormField.Remove(models.First<clsFrmField>());
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldformuid = fldformuid });
        }
    }
}
