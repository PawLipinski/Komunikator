using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Globalization;

public partial class logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HttpCookie cookie = Request.Cookies["style"];
        if (cookie == null)
        {
            cookie = new HttpCookie("style");
            cookie.Value = "IndexStyle.css";
            Response.Cookies.Add(cookie);
        }

        pagestyle.Href = "" + cookie.Value;
        
    }

    protected void loginButton_Click(object sender, EventArgs e)
    {
        #region excluded
        //using (SqlConnection conn = new SqlConnection())
        //{
        //    byte[] salt = null, key=null;
        //    // load salt and key from database


        //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["Komunikator"].ToString();
        //    //conn.ConnectionString = "Server=PAWEL-STAC\\SQLEXPRESS01;Database=KomunikatorDB;Trusted_Connection=true"; 
        //    conn.Open();
        //    var sqlcommand = new SqlCommand("SELECT login_uzytkownika, haslo_uzytkownika, sol_uzytkownika FROM Uzytkownicy WHERE login_uzytkownika LIKE @log", conn);

        //    sqlcommand.Parameters.AddWithValue("@log", this.login.Text);
        //    //sqlcommand.Parameters.AddWithValue("@pass", this.haslo.Text);

        //    //SqlDataReader dr = sqlcommand.ExecuteReader();
        //    SqlDataReader myReader = sqlcommand.ExecuteReader();
        //    if (myReader.Read())
        //    {
        //    key = (byte[])myReader["haslo_uzytkownika"];
        //    salt = (byte[])myReader["sol_uzytkownika"];


        //    using (var deriveBytes = new Rfc2898DeriveBytes(this.haslo.Text, salt))
        //    {
        //        byte[] newKey = deriveBytes.GetBytes(20);  // derive a 20-byte key

        //        if (!newKey.SequenceEqual(key))
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Nieprawidłowe dane! :(');", true);
        //        }
        //        else
        //        {
        //            Session["login"] = this.login.Text;
        //            Session["logged"] = true;
        //            Response.Redirect("ProfilePage.aspx");
        //        }
        //    }

        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Brak użytkowników w bazie! :(');", true);
        //    }
        //    conn.Close();
        //}

        //using (var deriveBytes = new Rfc2898DeriveBytes(this.haslo.Text, 20))
        //{
        #endregion

        UTF8Encoding utf8 = new UTF8Encoding(true, true);

            //byte[] key = deriveBytes.GetBytes(20);  // derive a 20-byte key

            IEnumerable<IDataRecord> rows = QueryBox.Retrieve(
            "SELECT login_uzytkownika, haslo_uzytkownika, sol_uzytkownika FROM Uzytkownicy WHERE login_uzytkownika LIKE @login",
           p =>
           {
               p.Add("@login", SqlDbType.Text).Value = this.login.Text;
           }
         );

            byte[] compareBytes = null;
            byte[] salt = null;

            foreach (var row in rows)
            {
                compareBytes = (byte[])row["haslo_uzytkownika"];
                salt = (byte[])row["sol_uzytkownika"];
            }


        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        try
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(this.haslo.Text, salt);
            tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);
        }
        catch (Exception d)
        {
            Console.WriteLine(d.Message);
        }

        if (StructuralComparisons.StructuralEqualityComparer.Equals(tdes.Key, compareBytes))
            {
                Session["login"] = this.login.Text;
                Session["logged"] = true;
                QueryBox.Insert(
               "Update Uzytkownicy SET status_uzytkownika = 1 where login_uzytkownika LIKE @login",
              p =>
              {
                  p.Add("@login", SqlDbType.Text).Value = Session["login"];

              });
            Response.Redirect("ProfilePage.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Nieprawidłowe dane! :(');", true);
            }
        //}
    }

    static byte[] GenerateSaltedHash(String passwordString, byte[] salt)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(passwordString + salt);
        System.Security.Cryptography.SHA256Managed bytesToHash = new System.Security.Cryptography.SHA256Managed();
        byte[] hash = bytesToHash.ComputeHash(bytes);

        return hash;
    }

    //branch master

    protected void StyleChange_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["style"];
        if (cookie == null) return;
        else if (cookie.Value == "IndexStyle.css")
        {
            cookie = new HttpCookie("style");
            cookie.Value = "AlternateStyle.css";
        }
        else if (cookie.Value == "AlternateStyle.css")
        {
            cookie = new HttpCookie("style");
            cookie.Value = "IndexStyle.css";
        }
        Response.Cookies.Add(cookie);
        Response.Redirect("Logon.aspx");
    }

    protected void LanguageChange_Click(object sender, EventArgs e)
    {
        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Equals("pl"))
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
        }
        else if (System.Threading.Thread.CurrentThread.CurrentUICulture.Equals("en"))
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
        }

    }
}