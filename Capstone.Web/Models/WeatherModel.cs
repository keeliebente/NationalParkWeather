using System;
using System.Collections.Generic;

namespace Capstone.Web.Models
{
    public class WeatherModel
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public string GetDayOfWeek()
        {
            string DayOfWeek = "";

            switch (FiveDayForecastValue)
            {
                case 1:
                    DayOfWeek = "Today";
                    break;
                case 2:
                    DayOfWeek = DateTime.Today.AddDays(1).DayOfWeek.ToString();
                    break;
                case 3:
                    DayOfWeek = DateTime.Today.AddDays(2).DayOfWeek.ToString();
                    break;
                case 4:
                    DayOfWeek = DateTime.Today.AddDays(3).DayOfWeek.ToString();
                    break;
                case 5:
                    DayOfWeek = DateTime.Today.AddDays(4).DayOfWeek.ToString();
                    break;
            }

            return DayOfWeek;
        }

        public int CelsiusLow()
        {
            return (Low - 32) * 5 / 9;
        }

        public int CelsiusHigh()
        {
            return (High - 32) * 5 / 9;
        }

        public List<string> Recommendation()
        {
            List<string> Recommendation = new List<string>();

            if (High > 75)
            {
                Recommendation.Add("Bring an extra gallon of water");
            }
            else if (Low < 20)
            {
                Recommendation.Add("Exposure to frigid temperatures is dangerous!");
            }
            if ((High - Low) > 20)
            {
                Recommendation.Add("Wear breathable layers");
            }

            switch (Forecast)
            {
                case "snow":
                    Recommendation.Add("Pack snowshoes");
                    break;
                case "sunny":
                    Recommendation.Add("Pack sunblock");
                    break;
                case "rain":
                    Recommendation.Add("Pack rain gear and wear waterproof shoes");
                    break;
                case "thunderstorms":
                    Recommendation.Add("Seek shelter and avoid hiking on exposed ridges");
                    break;
            }

            return Recommendation;
        }
    }
}
