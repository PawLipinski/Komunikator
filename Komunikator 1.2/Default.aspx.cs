using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["logged"] == null) || (Session["login"] == null)) {
            Response.Redirect("logon.aspx");
        }
        else
        {
            QueryBox.Insert(
           "Update Uzytkownicy SET status_uzytkownika = 1 where login_uzytkownika = @login",
          p =>
          {
                p.Add("@login", SqlDbType.Text).Value = Session["login"];

           });
            Response.Redirect("ProfilePage.aspx");
            //QueryBox.Insert("Update Uzytkownicy SET status_uzytkownika = 1 where login_uzytkownika = ", null);
        } 
    }
}