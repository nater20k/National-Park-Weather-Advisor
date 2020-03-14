using System;
using System.Collections.Generic;
using System.Web;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FOrC(string parkCode, string choice = "F")
        {
            HttpContext.Session.SetString("parkCode", parkCode);
            SetSession(choice);
            return RedirectToAction("DetailFC", "Park");
        }

        public void SetSession(string choice)
        {
            HttpContext.Session.SetString("temp", choice);
        }

        public string GetSession()
        {
            return HttpContext.Session.GetString("temp");
        }
    }
}
