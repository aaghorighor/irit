namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;

    public static class ControllerExtensions
    {
        public static string GetActionOutput<T>(this System.Web.Mvc.Controller ctrl, Expression<Action<T>> action)
        {
            var body = action.Body as MethodCallExpression;

            var originalFilter = ctrl.HttpContext.Response.Filter;
            var memoryStreamFilter = new MemoryStream();
            ctrl.HttpContext.Response.Filter = memoryStreamFilter;

            var routeData = ctrl.ControllerContext.RequestContext.RouteData;
            string controllerName = body.Object.Type.Name.Replace("Controller", "");
            string actionName = body.Method.Name;
            var orgAction = routeData.Values["action"];
            var orgController = routeData.Values["controller"];
            routeData.Values["action"] = actionName;
            routeData.Values["controller"] = controllerName;
            var routeKeyParameters = new List<string>();

            var parameters = body.Method.GetParameters();
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var expression = body.Arguments[i];
                    object value = null;
                    var constantExpression = expression as ConstantExpression;
                    if (constantExpression != null)
                    {
                        value = constantExpression.Value;
                    }
                    else
                    {
                        var lambda = Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object)), new ParameterExpression[0]);
                        value = lambda.Compile()();
                    }

                    routeData.Values.Add(parameters[i].Name, value);
                    routeKeyParameters.Add(parameters[i].Name);
                }
            }

            var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            var c = controllerFactory.CreateController(ctrl.ControllerContext.RequestContext, controllerName);
            string result = null;
            try
            {
                c.Execute(ctrl.ControllerContext.RequestContext);

                ctrl.HttpContext.Response.Flush();
                memoryStreamFilter.Position = 0;
                using (var r = new StreamReader(memoryStreamFilter, ctrl.Response.ContentEncoding))
                {
                    result = r.ReadToEnd();
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                memoryStreamFilter.Close();
                memoryStreamFilter.Dispose();
                foreach (var item in routeKeyParameters)
                {
                    routeData.Values.Remove(item);
                }

                ctrl.HttpContext.Response.Filter = originalFilter;
                routeData.Values["action"] = orgAction;
                routeData.Values["controller"] = orgController;
            }
            return result;
        }

        public static string GetActionOutput(this System.Web.Mvc.Controller ctrl, string controllerName, string action, object parameters)
        {
            // hijack the response stream  
            var orgResponseFilter = ctrl.HttpContext.Response.Filter;
            var memoryStreamFilter = new MemoryStream();
            ctrl.HttpContext.Response.Filter = memoryStreamFilter;

            // hijack routeData  
            var routeData = ctrl.ControllerContext.RequestContext.RouteData;
            var orgAction = routeData.Values["action"];
            var orgController = routeData.Values["controller"];
            routeData.Values["action"] = action;
            routeData.Values["controller"] = controllerName;

            if (parameters != null)
            {
                var propertyInfoList = parameters.GetType().GetProperties();
                foreach (var pi in propertyInfoList)
                {
                    routeData.Values.Add(pi.Name, pi.GetValue(parameters, null));
                }
            }

            var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            var c = controllerFactory.CreateController(ctrl.ControllerContext.RequestContext, controllerName);

            string result = null;
            try
            {
                c.Execute(ctrl.ControllerContext.RequestContext);
                ctrl.HttpContext.Response.Flush();
                memoryStreamFilter.Position = 0;
                using (var r = new StreamReader(memoryStreamFilter))
                {
                    result = r.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                logger.LogError(ex);
            }

            // restore  
            ctrl.HttpContext.Response.Filter = orgResponseFilter;
            routeData.Values["action"] = orgAction;
            routeData.Values["controller"] = orgController;

            return result;
        }

        /// <summary>
        /// Captures the HTML output by a controller action that returns a ViewResult
        /// </summary>
        /// <typeparam name="TController">The type of controller to execute the action on</typeparam>
        /// <param name="controller">The controller</param>
        /// <param name="action">The action to execute</param>
        /// <returns>The HTML output from the view</returns>
        public static string CaptureActionHtml<TController>(
            this TController controller,
            Func<TController, ViewResult> action)
            where TController : Controller
        {
            return controller.CaptureActionHtml(controller, null, action);
        }

        /// <summary>
        /// Captures the HTML output by a controller action that returns a ViewResult
        /// </summary>
        /// <typeparam name="TController">The type of controller to execute the action on</typeparam>
        /// <param name="controller">The controller</param>
        /// <param name="masterPageName">The master page to use for the view</param>
        /// <param name="action">The action to execute</param>
        /// <returns>The HTML output from the view</returns>
        public static string CaptureActionHtml<TController>(
            this TController controller,
            string masterPageName,
            Func<TController, ViewResult> action)
            where TController : Controller
        {
            return controller.CaptureActionHtml(controller, masterPageName, action);
        }

        /// <summary>
        /// Captures the HTML output by a controller action that returns a ViewResult
        /// </summary>
        /// <typeparam name="TController">The type of controller to execute the action on</typeparam>
        /// <param name="controller">The current controller</param>
        /// <param name="targetController">The controller which has the action to execute</param>
        /// <param name="action">The action to execute</param>
        /// <returns>The HTML output from the view</returns>
        public static string CaptureActionHtml<TController>(
            this Controller controller,
            TController targetController,
            Func<TController, ViewResult> action)
            where TController : Controller
        {
            return controller.CaptureActionHtml(targetController, null, action);
        }

        /// <summary>
        /// Captures the HTML output by a controller action that returns a ViewResult
        /// </summary>
        /// <typeparam name="TController">The type of controller to execute the action on</typeparam>
        /// <param name="controller">The current controller</param>
        /// <param name="targetController">The controller which has the action to execute</param>
        /// <param name="masterPageName">The name of the master page for the view</param>
        /// <param name="action">The action to execute</param>
        /// <returns>The HTML output from the view</returns>
        public static string CaptureActionHtml<TController>(
            this Controller controller,
            TController targetController,
            string masterPageName,
            Func<TController, ViewResult> action)
            where TController : Controller
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }
            if (targetController == null)
            {
                throw new ArgumentNullException("targetController");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            // pass the current controller context to orderController
            var controllerContext = controller.ControllerContext;
            targetController.ControllerContext = controllerContext;

            // replace the current context with a new context that writes to a string writer
            var existingContext = System.Web.HttpContext.Current;
            var writer = new StringWriter();
            var response = new HttpResponse(writer);
            var context = new HttpContext(existingContext.Request, response)
            {
                User = existingContext.User
            };
            System.Web.HttpContext.Current = context;

            // execute the action
            var viewResult = action(targetController);

            // change the master page name
            if (masterPageName != null)
            {
                viewResult.MasterName = masterPageName;
            }

            // we have to set the controller route value to the name of the controller we want to execute
            // because the ViewLocator class uses this to find the correct view
            var oldController = controllerContext.RouteData.Values["controller"];
            controllerContext.RouteData.Values["controller"] = typeof(TController).Name.Replace("Controller", "");

            // execute the result
            viewResult.ExecuteResult(controllerContext);

            // restore the old route data
            controllerContext.RouteData.Values["controller"] = oldController;

            // restore the old context
            System.Web.HttpContext.Current = existingContext;

            return writer.ToString();
        }


        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            string result = null;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                result = sw.GetStringBuilder().ToString();
            }
            return result;
        }
       
    }
}