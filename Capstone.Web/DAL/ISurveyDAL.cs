using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        bool SubmitSurvey(SurveyModel survey);
    }
}
