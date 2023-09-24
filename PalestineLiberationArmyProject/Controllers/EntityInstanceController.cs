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
using System.Web;

namespace PalestineLiberationArmyProject.Controllers
{
    public class EntityInstanceController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public EntityInstanceController(UserManager<ApplicationUser> userManager)
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
                var entity = dbManager.Entity.Where<clsEntity>(e => e.fldUID == id).FirstOrDefault();

                var instances = (from instance  in dbManager.EntityInstance.ToList()
                                 where instance.entityFldUID == id
                                select instance).ToList();


                
                foreach(var instance in instances)
                {
                     var fieldsvalues = (from fieldvalue in dbManager.EntityFieldValue.ToList()
                                    where fieldvalue.EntityInstancefldUID==instance.fldUID
                                    select fieldvalue).ToList();
                    instance.entityFieldValue = fieldsvalues;
                    instance.entity = entity;
                    foreach(var fieldvalue in instance.entityFieldValue)
                    {
                        fieldvalue.entityField = dbManager.EntityField.Where<clsEntityField>(e => e.fldUID == fieldvalue.EntityFieldfldUID).FirstOrDefault();
                    }
                }
                
                return View(instances);
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
        
        public IActionResult Add(string fldentityuid)
        {
            Guid id = new Guid(fldentityuid);

            ViewBag.fldentityuid = fldentityuid;
            var fields = (from field in dbManager.EntityField.ToList()
                                where field.entityflduid == id
                                select field).ToList();
            return View(fields);
        }
        [HttpGet]
        public IActionResult Store(string fldentityuid)
        {
            string date = DateTime.Now.ToShortDateString();
            ViewBag.fldentityuid = fldentityuid;
            var pars =Request.QueryString.Value.ToString().Substring(1);
            var resultAll = pars.Split('&');
            List<clsEntityFieldValue> values = new List<clsEntityFieldValue>();
            clsEntityInstance instance = new clsEntityInstance { fldCreationDate = date, entityFldUID = new Guid(fldentityuid), userFldUID = Guid.NewGuid() };

            dbManager.EntityInstance.Add(instance);
            dbManager.SaveChanges();
            var resultFirst = resultAll[0];
            
            foreach (var res in resultAll)
            {
                if (res == resultFirst)
                    continue;
                var res2= res.Split('=');
                res2[0] = res2[0].Substring(6);
                res2[1]= HttpUtility.UrlDecode(res2[1]);
                clsEntityFieldValue model = new clsEntityFieldValue{fldvalue=res2[1], fldCreationDate=date, EntityFieldfldUID=new Guid(res2[0]),EntityInstancefldUID=Guid.NewGuid()};
                model.EntityInstancefldUID = instance.fldUID;
                values.Add(model);

            }
            if (values.Count > 0)
            {
                dbManager.EntityFieldValue.AddRange(values);
                dbManager.SaveChanges();
            }

            return RedirectToAction("Index", new { fldentityuid = fldentityuid });
        }
       
        public IActionResult Delete(string flduid,string fldentityuid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsEntityFieldValue> models = dbManager.EntityFieldValue.Where(e => e.EntityInstancefldUID.ToString() == flduid).ToList();

            clsEntityInstance instance = dbManager.EntityInstance.Where(e => e.fldUID.ToString() == flduid).FirstOrDefault();
            dbManager.EntityFieldValue.RemoveRange(models);
            dbManager.EntityInstance.Remove(instance);
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldentityuid = fldentityuid });
        }
    }
}
