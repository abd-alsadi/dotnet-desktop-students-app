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
    public class FormController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public FormController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldparameter)
        {
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                var forms = dbManager.Form.ToList();
                return View(forms);
            }
            else
            {
                fldparameter = fldparameter.Trim();

                var forms = (from form in dbManager.Form.ToList()
                                     where form.fldName.Contains(fldparameter) || form.fldDescription.Contains(fldparameter) || form.fldCreationDate.Contains(fldparameter)
                                     select form).ToList();

                return View(forms);
            }


            
        }
        [HttpPost]
        public IActionResult Add(string fldname,string flddescription,string fldorder,string fldicon,string fldcontent)
        {
            string date = DateTime.Now.ToShortDateString();
            clsForm model=new clsForm{fldCreationDate=date,fldName=fldname,fldIcon=fldicon,fldDescription=flddescription,fldOrder=fldorder,fldContent=fldcontent };

            dbManager.Form.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldname, string flddescription, string fldorder, string fldicon,string fldCreationDate,string fldContent)
        {
            Guid uid = new Guid(flduid);
            
            clsForm model = (from form in dbManager.Form
                                where form.fldUID == uid
                             select form).SingleOrDefault();
            model.fldOrder = fldorder;
            model.fldIcon = fldicon;
            model.fldDescription = flddescription;
            model.fldName = fldname;
            model.fldContent = fldContent;
            dbManager.Form.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string flduid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsForm> models = dbManager.Form.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.Form.Remove(models.First<clsForm>());
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
