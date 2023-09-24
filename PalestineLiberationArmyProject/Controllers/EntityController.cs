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
    public class EntityController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public EntityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldparameter)
        {
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                var entities = dbManager.Entity.ToList();
                return View(entities);
            }
            else
            {
                fldparameter = fldparameter.Trim();

                var entities = (from entity in dbManager.Entity.ToList()
                                     where entity.fldName.Contains(fldparameter) || entity.fldDescription.Contains(fldparameter) || entity.fldCreationDate.Contains(fldparameter)
                                     select entity).ToList();

                return View(entities);
            }


            
        }
        [HttpPost]
        public IActionResult Add(string fldname,string flddescription,string fldorder,string fldicon)
        {
            string date = DateTime.Now.ToShortDateString();
            clsEntity model=new clsEntity{fldCreationDate=date,fldName=fldname,fldIcon=fldicon,fldDescription=flddescription,fldOrder=fldorder };

            dbManager.Entity.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldname, string flddescription, string fldorder, string fldicon,string fldCreationDate)
        {
            Guid uid = new Guid(flduid);
            
            clsEntity model = (from entity in dbManager.Entity
                                where entity.fldUID == uid
                             select entity).SingleOrDefault();
            model.fldOrder = fldorder;
            model.fldIcon = fldicon;
            model.fldDescription = flddescription;
            model.fldName = fldname;
            dbManager.Entity.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string flduid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsEntity> models = dbManager.Entity.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.Entity.Remove(models.First<clsEntity>());
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
