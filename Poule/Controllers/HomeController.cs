﻿using Microsoft.AspNetCore.Mvc;

namespace Poule.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello From HomeController");
        }
    }
}
