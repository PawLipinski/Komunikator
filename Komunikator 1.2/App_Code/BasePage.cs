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
        if (Request.Cookies["language"] == null)
        {
            HttpCookie cookie = new HttpCookie("language");
            cookie.Value = "pl";
            Response.Cookies.Add(cookie);
        }
        string lang = Convert.ToString(Request.Cookies["language"].Value);
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert(\'"+Request.Cookies["language"].Value+"\');", true);
            string culture = string.Empty;
            
            if(lang.ToLower().CompareTo("pl") == 0 ||string.IsNullOrEmpty(culture))
            {               
                culture = "pl";
            }
            
            if (lang.ToLower().CompareTo("en") == 0 || string.IsNullOrEmpty(culture))
            {
                culture = "en-US";
            }
            //if (lang.ToLower().CompareTo("pl") == 0)
            //{
            //    culture = "pl";
            //}
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            base.InitializeCulture();
        }
}
