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
    public class RoleController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public RoleController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldparameter)
        {
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                var roles = dbManager.Role.ToList();
                return View(roles);
            }
            else
            {
                fldparameter = fldparameter.Trim();

                var roles = (from role in dbManager.Role.ToList()
                                     where role.fldName.Contains(fldparameter) || role.fldDescription.Contains(fldparameter) || role.fldCreationDate.Contains(fldparameter)
                             select role).ToList();

                return View(roles);
            }


            
        }
        [HttpPost]
        public IActionResult Add(string fldname,string flddescription)
        {
            string date = DateTime.Now.ToShortDateString();
            clsRole model=new clsRole{fldCreationDate=date,fldName=fldname,fldDescription=flddescription};

            dbManager.Role.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldname, string flddescription,string fldCreationDate)
        {
            Guid uid = new Guid(flduid);
            
            clsRole model = (from role in dbManager.Role
                                where role.fldUID == uid
                             select role).SingleOrDefault();
            
            model.fldDescription = flddescription;
            model.fldName = fldname;
            dbManager.Role.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string flduid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsRole> models = dbManager.Role.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.Role.Remove(models.First<clsRole>());
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
