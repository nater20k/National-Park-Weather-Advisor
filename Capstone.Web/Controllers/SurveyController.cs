using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : HomeController
    {
        private ISurveyDao surveyDao;
        private IParkDao parkDao;

        public SurveyController(ISurveyDao surveyDao, IParkDao parkDao)
        {
            this.surveyDao = surveyDao;
            this.parkDao = parkDao;
        }

        [HttpGet]
        public IActionResult GetFeedback()
        {
            IList<Park> parks = new List<Park>();
            parks = parkDao.GetParks();
            ViewBag.ParksList = parks;
            Survey survey = new Survey();
           
            return View();
        }

        [HttpPost]
        public IActionResult GetFeedback(Survey survey)
        {
            surveyDao.SubmitSurvey(survey);
            return RedirectToAction("BestParks");
        }

        public IActionResult BestParks()
        {
            IList<Park> bestParks = new List<Park>();
            bestParks = parkDao.GetBestParks();
            return View(bestParks);
        }
    }
}