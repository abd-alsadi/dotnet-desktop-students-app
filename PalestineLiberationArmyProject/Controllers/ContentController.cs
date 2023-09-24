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
    public class ContentController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public ContentController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldformuid)
        {
            Guid uid = new Guid(fldformuid);
            clsForm model = (from form in dbManager.Form
                             where form.fldUID == uid
                             select form).SingleOrDefault();
            ViewBag.fldformuid = fldformuid;

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(clsForm formx,string fldformuid)
        {
            ViewBag.fldformuid = fldformuid;

            Guid uid = new Guid(fldformuid);
            clsForm model = (from form in dbManager.Form
                             where form.fldUID == uid
                             select form).SingleOrDefault();
            model.fldContent = formx.fldContent;
            dbManager.Form.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldformuid = fldformuid });
        }

        public IActionResult ShowForm(string fldformuid,string flduid)
        {
            ViewBag.fldformuid = fldformuid;
            Guid uid = new Guid(fldformuid);
            Guid instanceuid = new Guid(flduid);
            clsForm formx = (from form in dbManager.Form
                             where form.fldUID == uid
                             select form).SingleOrDefault();

            var content = formx.fldContent;

            clsFrmInstance instancex = (from instance in dbManager.FormInstance
                             where instance.fldUID == uid
                             select instance).SingleOrDefault();

            List<clsFrmField> fields = (from field in dbManager.FormField
                                        where field.formflduid == uid
                                        select field).ToList();

            foreach(var field in fields)
            {
                var fldNamex = field.fldNamex;
                if (content.Contains(fldNamex))
                {
                    clsFrmFieldValue valuex = (from value in dbManager.FormFieldValue
                                                     where value.FormInstancefldUID == instanceuid && value.FormFieldfldUID==field.fldUID
                                             select value).FirstOrDefault();
                    if(valuex != null)
                    {
                        content = content.Replace(fldNamex, valuex.fldvalue);
                    }
                }
            }
            formx.fldContent = content;

            return View(formx);
        }
        
    }
}