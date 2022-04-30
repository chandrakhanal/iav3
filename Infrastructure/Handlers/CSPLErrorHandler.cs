using NTierIA.Common;
using System;
using System.Text;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Handlers
{
    public class CSPLErrorHandler : HandleErrorAttribute, IDisposable
    {
        private LoggingHandler _loggingHandler;
        public CSPLErrorHandler()
        {
            _loggingHandler = new LoggingHandler();
        }
        public void Dispose()
        {
            if (_loggingHandler != null)
            {
                _loggingHandler.Dispose();
                _loggingHandler = null;
            }

            //base.Dispose(disposing);
        }
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            //Log Exception ex
            //GetExceptionMessageFormatted(ex);
            _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

            filterContext.ExceptionHandled = true;

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
                ,
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}