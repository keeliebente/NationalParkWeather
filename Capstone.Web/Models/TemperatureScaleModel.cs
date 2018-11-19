using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Capstone.Web.Models
{
    public class TemperatureScaleModel
    {
        public string TemperatureScale { get; set; }

        public IList<SelectListItem> TemperatureScales = new List<SelectListItem>()
        {
           new SelectListItem() {Text = "Fahrenheit"},
           new SelectListItem() {Text = "Celsius"}
        };
    }
}
