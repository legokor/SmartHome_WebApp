using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartHomeWebApp.Models.ViewModels;

namespace SmartHomeWebApp.Controllers
{
    [Authorize]
    public class DesignerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DesignerViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            return View();
        }

    }
}