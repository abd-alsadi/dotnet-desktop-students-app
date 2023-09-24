using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalestineLiberationArmyProject.Models.Core;
using PalestineLiberationArmyProject.Data;
using Microsoft.EntityFrameworkCore;

namespace PalestineLiberationArmyProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext manager = ApplicationDbContext.getInstance();

        public IActionResult Index()
        {
            //var x = manager.Role.Add(new clsRole() { fldName = "administration" ,fldDescription= "admin", fldCreationDate = "1/1/2010",fldUID=Guid.NewGuid().ToString() });
            //var xx= manager.SaveChanges();

 
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

    
        public IActionResult Error()
        {
            return View();
        }
    }
}
