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
    public class PersonController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public PersonController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldparameter)
        {
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                var persons = dbManager.Person.ToList();
                return View(persons);
            }
            else
            {
                fldparameter = fldparameter.Trim();

                var persons = (from person in dbManager.Person.ToList()
                                     where person.fldName.Contains(fldparameter) || person.fldNumber.Contains(fldparameter) 
                                     select person).ToList();

                return View(persons);
            }


            
        }
        [HttpPost]
        public IActionResult Add(string fldname,string fldnumber)
        {
            string date = DateTime.Now.ToShortDateString();
            clsPerson model=new clsPerson{fldCreationDate=date,fldName=fldname,fldNumber=fldnumber};

            dbManager.Person.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldname, string fldnumber,string fldCreationDate)
        {
            Guid uid = new Guid(flduid);
            
            clsPerson model = (from person in dbManager.Person
                                where person.fldUID == uid
                             select person).SingleOrDefault();
            
            model.fldNumber = fldnumber;
            model.fldName = fldname;
            dbManager.Person.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string flduid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsPerson> models = dbManager.Person.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.Person.Remove(models.First<clsPerson>());
            dbManager.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
