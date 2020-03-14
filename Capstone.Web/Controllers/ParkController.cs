using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class ParkController : HomeController
    {
        private IParkDao parkDao;
        private IForecastDao forecastDao;

        public ParkController(IParkDao parkDao, IForecastDao forecastDao)
        {
            this.parkDao = parkDao;
            this.forecastDao = forecastDao;
        }

        public IActionResult Index()
        {
            IList<Park> parks = new List<Park>();
            parks = parkDao.GetParks();
            return View("Index", parks);
        }

        public IActionResult Detail(string parkCode)
        {
            Park park = new Park();
            park = parkDao.GetParkDetails(parkCode);

            IList<Forecast> forecasts = new List<Forecast>();
            forecasts = forecastDao.GetForecast(parkCode);

            string tempChoice = GetSession();
            ViewBag.Temp = tempChoice;

            ViewBag.ParkForecast = forecasts;

            return View(park);
        }
        public IActionResult DetailFC()
        {
            Park park = new Park();
            string sessionParkCode = HttpContext.Session.GetString("parkCode");

            string parkCode = sessionParkCode;

            park = parkDao.GetParkDetails(parkCode);

            IList<Forecast> forecasts = new List<Forecast>();
            forecasts = forecastDao.GetForecast(parkCode);

            string tempChoice = GetSession();
            ViewBag.Temp = tempChoice;

            ViewBag.ParkForecast = forecasts;

            return View("Detail", park);
        }
    }
}