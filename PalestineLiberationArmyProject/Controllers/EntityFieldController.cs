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
    public class EntityFieldController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public EntityFieldController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldentityuid ,string fldparameter)
        {
            ViewBag.fldentityuid = fldentityuid;
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                
                Guid id = new Guid(fldentityuid);
                var fields = (from field in dbManager.EntityField.ToList()
                                where field.entityflduid==id
                                select field).ToList();
                return View(fields);
            }
            else
            {
                fldparameter = fldparameter.Trim();
                Guid id = new Guid(fldentityuid);
                var fields = (from field in dbManager.EntityField.Where<clsEntityField>(e=>e.entityflduid==id).ToList()
                                     where field.fldName.Contains(fldparameter) || field.fldDescription.Contains(fldparameter) || field.fldCreationDate.Contains(fldparameter) || field.fldDataType.Contains(fldparameter)
                                     select field).ToList();

                return View(fields);
            }
   
        }
        [HttpPost]
        public IActionResult Add(string fldentityuid,string fldname,string flddescription,string fldorder,string fldisrequired,string flddatatype)
        {
            string date = DateTime.Now.ToShortDateString();

            string isreq = "0";
            if (fldisrequired == "on")
            {
                isreq = "1";
            }
            Guid id = new Guid(fldentityuid);
            clsEntityField model=new clsEntityField{fldCreationDate=date,fldName=fldname,fldIsRequired= isreq, fldDescription=flddescription,fldOrder=fldorder,fldDataType=flddatatype,entityflduid=id };

            dbManager.EntityField.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new {fldentityuid=fldentityuid });
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldentityuid,string fldname, string flddescription, string fldorder, string flddatatype,string fldCreationDate,string fldisrequired)
        {
            Guid uid = new Guid(flduid);
            
            clsEntityField model = (from field in dbManager.EntityField
                                where field.fldUID == uid
                             select field).SingleOrDefault();
            model.fldOrder = fldorder;
            model.fldDataType = flddatatype;
            model.fldDescription = flddescription;
            model.fldName = fldname;
            model.fldIsRequired = "0";
            if (fldisrequired == "on")
            {
                model.fldIsRequired = "1";
            }
            
            dbManager.EntityField.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new {fldentityuid=fldentityuid });
        }
        public IActionResult Delete(string flduid,string fldentityuid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsEntityField> models = dbManager.EntityField.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.EntityField.Remove(models.First<clsEntityField>());
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldentityuid = fldentityuid });
        }
    }
}
