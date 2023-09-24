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
    public class TransactionController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public TransactionController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string fldpersonuid ,string fldparameter)
        {
            ViewBag.fldpersonuid = fldpersonuid;
            string email = _userManager.GetUserName(User);
            if (fldparameter == "" || fldparameter == null)
            {
                
                Guid id = new Guid(fldpersonuid);
                var transactios = (from transaction in dbManager.Transaction.ToList()
                                where transaction.personflduid==id
                                select transaction).ToList();
                return View(transactios);
            }
            else
            {
                fldparameter = fldparameter.Trim();
                Guid id = new Guid(fldpersonuid);
                var transactions = (from transaction in dbManager.Transaction.Where<clsTransaction>(e=>e.personflduid == id).ToList()
                                     where transaction.fldDate.Contains(fldparameter) || transaction.fldtext.Contains(fldparameter) || transaction.fldCreationDate.Contains(fldparameter) || transaction.fldtype.Contains(fldparameter)
                                     select transaction).ToList();

                return View(transactions);
            }
   
        }
        [HttpPost]
        public IActionResult Add(string fldpersonuid,string fldtext,string fldtype,string flddate)
        {
            string date = DateTime.Now.ToShortDateString();
            
            Guid id = new Guid(fldpersonuid);
            clsTransaction model=new clsTransaction{fldCreationDate=date,fldtext=fldtext,fldtype=fldtype, fldDate=flddate,personflduid=id };

            dbManager.Transaction.Add(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new {fldpersonuid=fldpersonuid });
        }
        [HttpPost]
        public IActionResult Update(string flduid,string fldpersonuid,string fldtext, string fldtype, string flddate,string fldCreationDate)
        {
            Guid uid = new Guid(flduid);
            
            clsTransaction model = (from transaction in dbManager.Transaction
                                where transaction.fldUID == uid
                             select transaction).SingleOrDefault();
            model.fldDate = flddate;
            model.fldtext = fldtext;
            model.fldtype = fldtype;
           
            dbManager.Transaction.Update(model);
            dbManager.SaveChanges();
            return RedirectToAction("Index",new {fldpersonuid=fldpersonuid });
        }
        public IActionResult Delete(string flduid,string fldpersonuid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsTransaction> models = dbManager.Transaction.Where(e => e.fldUID.ToString() == flduid).ToList();

            dbManager.Transaction.Remove(models.First<clsTransaction>());
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldpersonuid = fldpersonuid });
        }
    }
}
