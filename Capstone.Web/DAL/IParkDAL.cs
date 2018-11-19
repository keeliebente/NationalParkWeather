using Capstone.Web.Models;
using System.Collections.Generic;

namespace Capstone.Web.DAL
{
    public interface IParkDAL
    {
        ParkModel GetPark(string parkCode);
        IList<ParkModel> GetParks();
        IList<ParkModel> GetFavoriteParks();
    }
}
