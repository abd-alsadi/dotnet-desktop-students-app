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
    public class AdminController : Controller
    {
        private ApplicationDbContext manager = ApplicationDbContext.getInstance();

       
        public IActionResult Admin()
        {
            return View();
        }
       
    }
}
