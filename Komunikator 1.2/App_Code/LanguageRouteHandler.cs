using System;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

public class LanguageRouteHandler : IRouteHandler
{
    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        string page = CheckForNullValue(requestContext.RouteData.Values["page"]);
        string virtualPath = "~/" + page;

        try
        {
            return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page)) as IHttpHandler;
        }
        catch
        {
            return null;
        }
    }

    public static string CheckForNullValue(object x)
    {
        if (x == null) return "";
        else return x.ToString();
    }
}