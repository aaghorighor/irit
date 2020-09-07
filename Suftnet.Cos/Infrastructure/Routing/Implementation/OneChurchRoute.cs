namespace Suftnet.Cos.Web
{
    using System.Web.Routing;

    public class OneChurchRoute : Route
    {
        public OneChurchRoute(string url, IRouteHandler routeHandler)
			: base(url, routeHandler)
		{

		}

		public OneChurchRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: base(url, defaults, routeHandler)
		{

		}

		public OneChurchRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: base(url, defaults, constraints, routeHandler)
		{

		}

        public OneChurchRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
			: base(url, defaults, constraints, dataTokens, routeHandler)
		{

		}

		public string Name { get; set; }
    }
}