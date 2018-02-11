using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLun.SuitOfArmor.Common.CacheControle;
using BLun.SuitOfArmor.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [XssProtection(XssProtection.EnablesForOldIe8)]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [CacheControl(CacheResponseDirective.NoCache)]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}