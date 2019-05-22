using System.Web.Mvc;
using proyCodeReview.Logic;

namespace PruebaBelatrix.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobLoggerLogic _oJobLogger = new JobLoggerLogic();

        public ActionResult Index()
        {
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Message);
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Error);
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Warning);
            return View();
        }

        public ActionResult About()
        {
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Message);
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Error);
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Warning);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Message);
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Error);
            _oJobLogger.LogMessage(GetType().Name, JobLoggerLogic.LogType.Warning);
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}