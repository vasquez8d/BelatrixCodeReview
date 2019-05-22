using System;
using System.Web.Mvc;
using proyCodeReview.Logic;

namespace proyCodeReview.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobLoggerLogic _oJobLogger = new JobLoggerLogic();

        public ActionResult Index()
        {
            try
            {
                _oJobLogger.LogMessage(GetType().Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, JobLoggerLogic.LogType.Message);
                _oJobLogger.LogMessage(GetType().Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, JobLoggerLogic.LogType.Warning);
            }
            catch (Exception ex)
            {
                _oJobLogger.LogMessage(ex.Message, JobLoggerLogic.LogType.Error);
            }

            return View();
        }

        public ActionResult About()
        {
            try
            {
                _oJobLogger.LogMessage(GetType().Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, JobLoggerLogic.LogType.Message);
                _oJobLogger.LogMessage(GetType().Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, JobLoggerLogic.LogType.Warning);
                ViewBag.Message = "Your application description page.";
            }
            catch (Exception ex)
            {
                _oJobLogger.LogMessage(ex.Message, JobLoggerLogic.LogType.Error);
            }

            return View();
        }

        public ActionResult Contact()
        {
            try
            {
                var data = "/".Split('-')[12].ToCharArray();
                _oJobLogger.LogMessage(GetType().Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, JobLoggerLogic.LogType.Message);
                _oJobLogger.LogMessage(GetType().Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name, JobLoggerLogic.LogType.Warning);
                ViewBag.Message = "Your contact page.";
            }
            catch (Exception ex)
            {
                _oJobLogger.LogMessage(ex.Message, JobLoggerLogic.LogType.Error);
            }

            return View();
        }
    }
}