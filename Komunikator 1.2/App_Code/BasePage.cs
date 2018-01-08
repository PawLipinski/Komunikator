using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage: System.Web.UI.Page
{
    public BasePage()
    { }

        protected override void InitializeCulture()
        {
        //if (!string.IsNullOrEmpty(Request.Cookies["language"].Value))
        //{
        //    HttpCookie cookie = Request.Cookies["language"];
        //    Session["lang"] = Request["lang"];
        //}
        //string lang = Convert.ToString(Request.Cookies["language"].Value);
        string lang = Convert.ToString(Request["language"]);

        if (lang==null)
        {
            HttpCookie cookie = new HttpCookie("language");
            
            if (Request.Cookies["language"] == null)
            {
                cookie.Value = "pl";
                Response.Cookies.Add(cookie);
                if (lang==null)
                {
                    lang = cookie.Value;
                }
            }
            else
            {
                lang = cookie.Value;
            }
        }

        
        string culture = string.Empty;
            
            if(lang.ToLower().CompareTo("pl") == 0 ||string.IsNullOrEmpty(culture))
            {               
                culture = "pl";
            }
            
            if (lang.ToLower().CompareTo("en") == 0 || string.IsNullOrEmpty(culture))
            {
                culture = "en-US";
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            base.InitializeCulture();
        }
}
