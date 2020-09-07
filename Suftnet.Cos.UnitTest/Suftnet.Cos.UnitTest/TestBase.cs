namespace Suftnet.Cos.UnitTest
{
    using Moq;
    using NUnit.Framework;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;
    using Suftnet.Cos.Web;

    [TestFixture]
    public abstract class TestBase
    {
        private Mock<HttpContextBase> m_MockHttpContext;
        protected ILogger Logger { get; private set; }

        public virtual void Initialize()
        {
            var httpContext = GetHttpContext();                      
              
            var container = Boostrapper.Start();
            container.Configure(x =>
            {
                x.For<ILogger>().Use<LogAdapter>();                  
            });
            
            this.Logger = container.GetInstance<ILogger>();
        }

        protected void TeardownHttpContext()
        {
            m_MockHttpContext = null;
        }

        public HttpContextBase GetHttpContext()
        {
            if (m_MockHttpContext == null)
            {
                m_MockHttpContext = CreateMockHttpContext();
            }
            return m_MockHttpContext.Object;
        }

        protected T CreateController<T>()
            where T : System.Web.Mvc.Controller
        {
            var httpContext = GetHttpContext();
            var result = CreateController<T>(httpContext);
            return result;
        }

        protected T CreateController<T>(System.Web.HttpContextBase httpContext)
            where T : System.Web.Mvc.Controller
        {
            var controller = GeneralConfiguration.Configuration.DependencyResolver.GetService<T>();
            controller.SetupControllerContext(httpContext);

            return controller;
        }
        public T CreateApiController<T>()
            where T : System.Web.Http.ApiController
        {
            var controller = GeneralConfiguration.Configuration.DependencyResolver.GetService<T>();
            controller.Request = new HttpRequestMessage();
            controller.Request.Headers.Add("apikey", "test");
            controller.ControllerContext = new System.Web.Http.Controllers.HttpControllerContext();
            return controller;
        }      
      
        public Mock<HttpContextBase> CreateMockHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var cookies = new HttpCookieCollection();

            // Response
            var response = new Mock<HttpResponseBase>();
            var cachePolicy = new Mock<HttpCachePolicyBase>();

            response.SetupProperty(r => r.StatusCode, 200);
            response.Setup(r => r.Cache).Returns(cachePolicy.Object);
            response.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => r);
            response.Setup(r => r.Cookies).Returns(cookies);
            context.Setup(ctx => ctx.Response).Returns(response.Object);

            // Request
            var request = new Mock<HttpRequestBase>();
            var userId = Guid.NewGuid().ToString();
            var principal = new Suftnet.Cos.Web.UserPrincipal(userId);

            request.Setup(r => r.AnonymousID).Returns(principal.UserId);
            request.Setup(r => r.AnonymousID).Returns(principal.UserId);
            request.Setup(r => r.Cookies).Returns(cookies);
            request.Setup(r => r.Url).Returns(new Uri("http://www.tester.com"));
            request.Setup(r => r.Headers).Returns(new System.Collections.Specialized.NameValueCollection());
            request.Setup(r => r.RequestContext).Returns(new System.Web.Routing.RequestContext(context.Object, new System.Web.Routing.RouteData()));
            request.SetupGet(x => x.PhysicalApplicationPath).Returns("/");
            request.Setup(r => r.UserHostAddress).Returns("127.0.0.1");
           

            request.Object.Cookies.Add(new HttpCookie("userId")
            {
                Value = principal.UserId,
            });

            request.SetupGet(r => r.QueryString).Returns(new System.Collections.Specialized.NameValueCollection());
            request.SetupGet(r => r.Form).Returns(new System.Collections.Specialized.NameValueCollection());
            request.SetupGet(r => r.PathInfo).Returns(string.Empty);
            request.SetupGet(r => r.AppRelativeCurrentExecutionFilePath).Returns("~/");
            context.Setup(ctx => ctx.Request).Returns(request.Object);

            // Sessions
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(ctx => ctx.Session).Returns(session.Object);

            // Server
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(r =>
            {
                if (r.Equals("/bin", StringComparison.InvariantCultureIgnoreCase))
                {
                    r = string.Empty;
                }
                var path = typeof(Suftnet.Cos.Web.MvcApplication).Assembly.Location;
                var fileName = System.IO.Path.GetFileName(path);
                path = path.Replace(fileName, string.Empty);
                r = r.Trim('/').Trim('\\');
                path = System.IO.Path.Combine(path, r);
                return path;
            });
            context.Setup(ctx => ctx.Server).Returns(server.Object);            

            // Items
            context.Setup(ctx => ctx.Items).Returns(new Dictionary<string, object>());

            context.Setup(c => c.User).Returns(principal);
            context.Setup(c => c.User.Identity.IsAuthenticated).Returns(true); 
            context.Setup(c => c.User.Identity.Name).Returns("1001");
            context.Setup(c => c.User.IsInRole("Admin")).Returns(true);
            context.Setup(c => c.User.IsInRole("Admin")).Returns(true); 

            return context;
        }

        //protected TestControllerActionInvoker<Result> ControllerActionInvoker<Result>()
        //    where Result : ActionResult
        //{
        //    var container = Boostrapper.Start();
        //    return new TestControllerActionInvoker<Result>(container);
        //}


        protected bool IsOnlyPostAllowed<T>(Expression<Action<T>> action)
        {
            var body = action.Body as MethodCallExpression;

            var attribute = body.Method.GetCustomAttributes(typeof(AcceptVerbsAttribute), false)
                                     .Cast<AcceptVerbsAttribute>()
                                     .SingleOrDefault();

            return (attribute != null && attribute.Verbs.Contains(HttpVerbs.Post.ToString().ToUpper()));
        }

        protected void AuthenticateRequest(System.Web.HttpContextBase ctx)
        {
            var visitorId = ((Suftnet.Cos.Web.UserPrincipal)ctx.User).UserId;
            var authCookie = ctx.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return;
            }

            var authenticationService = GeneralConfiguration.Configuration.DependencyResolver.GetService<IUserAccount>();

            Logger.Log($"RequestAuthenticated {ctx.Request.Url}", EventLogSeverity.Information);
            // Extraction et decryptage du ticket d'autentification
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            if (authTicket == null)
            {
                return;
            }

            // Recuperation du userId
            int userId = 0;

            int.TryParse(authTicket.Name, out userId);

            if (userId > 0)
            {
                // Creation d'une nouvelle identité
                var id = new FormsIdentity(authTicket);

                // On passe l'identité et les roles a l'objet Principal
                var principal = new Suftnet.Cos.Web.UserPrincipal(id, visitorId);
                var user = authenticationService.Get(userId);
                if (user != null)
                {
                    user.LastLoginDate = DateTime.UtcNow;
                    principal.CurrentUser.User.UserId = user.UserId;
                    Logger.Log($"User : {principal.CurrentUser.User.UserName} Logged", EventLogSeverity.Information );
                }

                var httpContext = GetHttpContext();
                var mock = Moq.Mock.Get(httpContext);
                mock.Setup(c => c.User).Returns(principal);
            }
        }        

        public UrlHelper GetUrlHelper(string appPath = "/", RouteCollection routes = null)
        {
            if (routes == null)
            {
                routes = new RouteCollection();
            }

            var httpContext = new StubHttpContextForRouting(appPath);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "home");
            routeData.Values.Add("action", "index");
            var requestContext = new RequestContext(httpContext, routeData);
            var helper = new UrlHelper(requestContext, routes);
            return helper;
        }

        public RouteData GetRouteDataByUrl(string url)
        {
            var ctx = new StubHttpContextForRouting(requestUrl: url);
            var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(ctx);
            return routeData;
        }
    }
}
