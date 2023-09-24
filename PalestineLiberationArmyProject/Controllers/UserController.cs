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
    public class UserController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldroleuid ,string fldparameter)
        {
            ViewBag.fldroleuid = fldroleuid;
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                
                Guid id = new Guid(fldroleuid);
                var users = (from user in dbManager.User.ToList()
                                where user.rolefldUID==id
                                select user).ToList();
                return View(users);
            }
            else
            {
                fldparameter = fldparameter.Trim();
                Guid id = new Guid(fldroleuid);
                var users = (from user in dbManager.User.Where<clsUser>(e=>e.rolefldUID == id).ToList()
                                     where user.fldEmail.Contains(fldparameter) || user.fldName.Contains(fldparameter) || user.fldCreationDate.Contains(fldparameter) || user.fldPassword.Contains(fldparameter)
                                     select user).ToList();

                return View(users);
            }
   
        }
        [HttpPost]
        public IActionResult Add(string fldroleuid,string fldname,string fldemail,string fldpassword)
        {
            string date = DateTime.Now.ToShortDateString();
            
            Guid id = new Guid(fldroleuid);
            clsUser model=new clsUser{fldCreationDate=date,fldName=fldname,fldEmail=fldemail, fldPassword=fldpassword, rolefldUID = id };

            dbManager.User.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new {fldroleuid=fldroleuid });
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldroleuid,string fldname, string fldemail, string fldpassword,string fldCreationDate)
        {
            Guid uid = new Guid(flduid);
            
            clsUser model = (from user in dbManager.User
                                where user.fldUID == uid
                             select user).SingleOrDefault();
            model.fldName = fldname;
            model.fldEmail = fldemail;
            model.fldPassword = fldpassword;
           
            dbManager.User.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new {fldroleuid=fldroleuid });
        }
        public IActionResult Delete(string flduid,string fldroleuid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsUser> models = dbManager.User.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.User.Remove(models.First<clsUser>());
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldroleuid = fldroleuid });
        }
    }
}
