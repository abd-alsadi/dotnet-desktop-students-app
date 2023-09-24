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
    public class FormInstanceController : Controller
    {
        private ApplicationDbContext dbManager = ApplicationDbContext.getInstance();
        private UserManager<ApplicationUser> _userManager;
        public FormInstanceController(UserManager<ApplicationUser> userManager)
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
                var form = dbManager.Form.Where<clsForm>(e => e.fldUID == id).FirstOrDefault();

                var instances = (from instance  in dbManager.FormInstance.ToList()
                                 where instance.formFldUID == id
                                select instance).ToList();


                
                foreach(var instance in instances)
                {
                     var fieldsvalues = (from fieldvalue in dbManager.FormFieldValue.ToList()
                                    where fieldvalue.FormInstancefldUID==instance.fldUID
                                    select fieldvalue).ToList();
                    instance.formFieldValue = fieldsvalues;
                    instance.form = form;
                    foreach(var fieldvalue in instance.formFieldValue)
                    {
                        fieldvalue.formField = dbManager.FormField.Where<clsFrmField>(e => e.fldUID == fieldvalue.FormFieldfldUID).FirstOrDefault();
                    }
                }
                
                return View(instances);
            }
            else
            {
                fldparameter = fldparameter.Trim();
                Guid id = new Guid(fldformuid);
                var fields = (from field in dbManager.FormField.Where<clsFrmField>(e=>e.formflduid==id).ToList()
                                     where field.fldName.Contains(fldparameter) || field.fldDescription.Contains(fldparameter) || field.fldCreationDate.Contains(fldparameter) || field.fldDataType.Contains(fldparameter)
                                     select field).ToList();

                return View(fields);
            }
   
        }
        
        public IActionResult Add(string fldformuid)
        {
            Guid id = new Guid(fldformuid);

            ViewBag.fldformuid = fldformuid;
            var fields = (from field in dbManager.FormField.ToList()
                                where field.formflduid == id
                                select field).ToList();
            return View(fields);
        }
        [HttpGet]
        public IActionResult Store(string fldformuid)
        {
            string date = DateTime.Now.ToShortDateString();
            ViewBag.fldformuid = fldformuid;
            var pars =Request.QueryString.Value.ToString().Substring(1);
            var resultAll = pars.Split('&');
            List<clsFrmFieldValue> values = new List<clsFrmFieldValue>();
            clsFrmInstance instance = new clsFrmInstance { fldCreationDate = date, formFldUID = new Guid(fldformuid), userFldUID = Guid.NewGuid() };

            dbManager.FormInstance.Add(instance);
            dbManager.SaveChanges();
            dbManager.Database.CloseConnection();
            var resultFirst = resultAll[0];
            
            foreach (var res in resultAll)
            {
                if (res == resultFirst)
                    continue;
                var res2= res.Split('=');
                res2[0] = res2[0].Substring(6);
                res2[1]= HttpUtility.UrlDecode(res2[1]);
                clsFrmFieldValue model = new clsFrmFieldValue{fldvalue=res2[1], fldCreationDate=date, FormFieldfldUID=new Guid(res2[0]),FormInstancefldUID=Guid.NewGuid()};
                model.FormInstancefldUID = instance.fldUID;
                values.Add(model);

            }
            if (values.Count > 0)
            {
                dbManager.FormFieldValue.AddRange(values);
                dbManager.SaveChanges();
            }

            return RedirectToAction("Index", new { fldformuid = fldformuid });
        }
       
        public IActionResult Delete(string flduid,string fldformuid)
        {
            string date = DateTime.Now.ToShortDateString();
            List<clsFrmFieldValue> models = dbManager.FormFieldValue.Where(e => e.FormInstancefldUID.ToString() == flduid).ToList();

            clsFrmInstance instance = dbManager.FormInstance.Where(e => e.fldUID.ToString() == flduid).FirstOrDefault();
            dbManager.FormFieldValue.RemoveRange(models);
            dbManager.FormInstance.Remove(instance);
            dbManager.SaveChanges();
            return RedirectToAction("Index", new { fldformuid = fldformuid });
        }
    }
}
