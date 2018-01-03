using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

/// <summary>
/// Summary description for MyRouteConfig
/// </summary>
public class MyRouteConfig
{
    public MyRouteConfig()
    {
        
    }

    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.EnableFriendlyUrls();
        routes.Add(new System.Web.Routing.Route("{language}/{*page}", new LanguageRouteHandler()));
    }
}