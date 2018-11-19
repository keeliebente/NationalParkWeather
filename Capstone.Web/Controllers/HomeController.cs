using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Capstone.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParkDAL _parkDAL;
        private readonly ISurveyDAL _surveyDAL;
        private readonly IWeatherDAL _weatherDAL;

        public HomeController(IParkDAL parkDAL, ISurveyDAL surveyDAL, IWeatherDAL weatherDAL)
        {
            _parkDAL = parkDAL;
            _surveyDAL = surveyDAL;
            _weatherDAL = weatherDAL;
        }

        public IActionResult Index()
        {
            IList<ParkModel> parks = _parkDAL.GetParks();

            ParksViewModel model = new ParksViewModel
            {
                Parks = parks
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            ParkModel model = _parkDAL.GetPark(id);
            model.Weather = _weatherDAL.GetWeatherByPark(id);
            model.TemperatureScaleModel = new TemperatureScaleModel();

            if (HttpContext.Session.Get<TemperatureScaleModel>("TemperatureType") != null)
            {
                model.TemperatureScaleModel.TemperatureScale = HttpContext.Session.Get<TemperatureScaleModel>("TemperatureType").TemperatureScale.ToString();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(ParkModel model)
        {
            HttpContext.Session.Set("TemperatureType", model.TemperatureScaleModel);

            return RedirectToAction(nameof(Detail), model.ParkCode);
        }

        [HttpGet]
        public IActionResult Survey()
        {
            SurveyModel model = new SurveyModel();

            IList<ParkModel> parks = _parkDAL.GetParks();

            IList<SelectListItem> Parks = new List<SelectListItem>();

            foreach (ParkModel park in parks)
            {
                Parks.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
            }

            model.ParkNames = Parks;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Survey(SurveyModel model)
        {
            if (!ModelState.IsValid)
            {
                IList<ParkModel> parks = _parkDAL.GetParks();

                IList<SelectListItem> Parks = new List<SelectListItem>();

                foreach (ParkModel park in parks)
                {
                    Parks.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
                }

                model.ParkNames = Parks;

                return View(model);
            }

            else
            {
                _surveyDAL.SubmitSurvey(model);

                return RedirectToAction(nameof(FavoriteParks));
            }
        }

        public IActionResult FavoriteParks()
        {
            FavoriteParksViewModel model = new FavoriteParksViewModel();

            IList<ParkModel> FavoriteParks = _parkDAL.GetFavoriteParks();

            model.FavoriteParks = FavoriteParks;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
