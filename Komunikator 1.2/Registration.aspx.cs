using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Data;

public partial class registration : System.Web.UI.Page
{
    private static int MAX_LENGTH = 32;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void loginButton_Click(object sender, EventArgs e)
   {
       
            UTF8Encoding utf8 = new UTF8Encoding(true, true);

            byte[] salt = CreateRandomSalt(7);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();


            try
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(this.Haslo.Text, salt);
                tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message);
            }
           

        if (checkLogin(this.Login.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "act1", "dbanswer('Login','Podaj inny login', 'Istnieje użytkownik z takim loginem.');", true);
                return;
            };

            if (checkEmail(this.Email.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "act2", "dbanswer('Email','Podaj inny mail','Istnieje użytkownik zarejestrowany na taki mail.');", true);

                return;
            };

            //try { 
            QueryBox.Insert(
            "INSERT INTO Uzytkownicy VALUES (@login, @haslo, @imie, @nazwisko, @email, @status, @salt)",
           p =>
           {
               p.Add("@login", SqlDbType.Text).Value = this.Login.Text;
               p.Add("@haslo", SqlDbType.VarBinary).Value = tdes.Key;
               //p.Add("@haslo", SqlDbType.VarChar).Value = System.Text.Encoding.UTF8.GetString(key);
               p.Add("@imie", SqlDbType.Text).Value = this.Imie.Text;
               p.Add("@nazwisko", SqlDbType.Text).Value = this.Nazwisko.Text;
               p.Add("@email", SqlDbType.Text).Value = this.Email.Text;
               p.Add("@status", SqlDbType.TinyInt).Value = 0;
               p.Add("@salt", SqlDbType.VarBinary).Value = salt; ;
               //p.Add("@salt", SqlDbType.VarChar).Value = System.Text.Encoding.UTF8.GetString(salt); ;
           });

                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Pomyślnie zarejestrowano!');", true);
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Error","alert('hi');", true);
                Response.Redirect("Default.aspx");
            //}
            //catch
            //{
            //    return;
            //}

            #region ugly
            //using (SqlConnection conn = new SqlConnection())
            //    {

            //    if (checkLogin(this.Login.Text))
            //    {
            //        ClientScript.RegisterStartupScript(GetType(), "act1", "dbanswer('Login','podaj inny login', 'Istnieje użytkownik z takim loginem.');", true);
            //        //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Istnieje użytkownik z takim loginem.');", true);
            //        return;
            //    };

            //    if (checkEmail(this.Email.Text))
            //    {
            //        ClientScript.RegisterStartupScript(GetType(), "act2", "dbanswer('Email','Podaj inny mail','Istnieje użytkownik zarejestrowany na taki mail.');", true);

            //        return;
            //    };

            //    using (var deriveBytes = new Rfc2898DeriveBytes(this.Haslo.Text, 20))
            //    {
            //        byte[] salt = deriveBytes.Salt;
            //        byte[] key = deriveBytes.GetBytes(20);  // derive a 20-byte key

            //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["Komunikator"].ToString();
            //        //conn.ConnectionString = "Server=PAWEL-STAC\\SQLEXPRESS01;Database=KomunikatorDB;Trusted_Connection=true";
            //        conn.Open();
            //        var sqlcommand = new SqlCommand("INSERT INTO Uzytkownicy VALUES (@login, @haslo, @imie, @nazwisko, @email, @salt)", conn);

            //        sqlcommand.Parameters.AddWithValue("@login", this.Login.Text);
            //        sqlcommand.Parameters.AddWithValue("@haslo", key);
            //        sqlcommand.Parameters.AddWithValue("@imie", this.Imie.Text);
            //        sqlcommand.Parameters.AddWithValue("@nazwisko", this.Nazwisko.Text);
            //        sqlcommand.Parameters.AddWithValue("@email", this.Email.Text);
            //        sqlcommand.Parameters.AddWithValue("@email", salt);

            //        try
            //        {
            //            sqlcommand.ExecuteNonQuery();
            //            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Pomyślnie zarejestrowano!');", true);
            //            //ScriptManager.RegisterStartupScript(this, typeof(string), "Error","alert('hi');", true);
            //            Response.Redirect("Default.aspx");
            //        }
            //        catch
            //        {
            //            return;
            //        }

            //        conn.Close();
            //    }
            //}
            #endregion
        
    }

    public bool checkLogin(string name) 
    {
        
        IEnumerable<IDataRecord> rows = QueryBox.Retrieve(
            "SELECT login_uzytkownika FROM Uzytkownicy WHERE login_uzytkownika LIKE @login",
           p =>
           {
               p.Add("@login", SqlDbType.Text).Value = name;
           }
         );

        #region badstyle
        //using (SqlConnection conn = new SqlConnection())
        //{
        //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["Komunikator"].ToString();
        //    //conn.ConnectionString = "Server=PAWEŁ-LAPTOP\\SQLEXPRESS;Database=KomunikatorDB;Trusted_Connection=true";
        //    conn.Open();
        //    var sqlcommand = new SqlCommand("SELECT COUNT(login_uzytkownika) FROM Uzytkownicy WHERE login_uzytkownika LIKE @login", conn);

        //    sqlcommand.Parameters.AddWithValue("@login", name);
        //    Int32 count = (Int32)sqlcommand.ExecuteScalar();
        //    if (count >0) return true;
        //    else return false;         
        //}
        #endregion

        if (QueryBox.Count(rows) > 0)
        {
            return true;
        }
        else return false;
    }

    public bool checkEmail(string name) 
    {
        IEnumerable<IDataRecord> rows = QueryBox.Retrieve(
            "SELECT email_uzytkownika FROM Uzytkownicy WHERE email_uzytkownika LIKE @name",
           p =>
           {
               p.Add("@name", SqlDbType.Text).Value = name;
           }
         );

        if (QueryBox.Count(rows) > 0)
        {
            return true;
        }
        else return false;
    }

    public static byte[] CreateRandomSalt(int length)
    {
        // Create a buffer
        byte[] randBytes;

        if (length >= 1)
        {
            randBytes = new byte[length];
        }
        else
        {
            randBytes = new byte[1];
        }

        // Create a new RNGCryptoServiceProvider.
        RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        // Fill the buffer with random bytes.
        rand.GetBytes(randBytes);

        // return the bytes.
        return randBytes;
    }

    public static void ClearBytes(byte[] buffer)
    {
        // Check arguments.
        if (buffer == null)
        {
            throw new ArgumentException("buffer");
        }

        // Set each byte in the buffer to 0.
        for (int x = 0; x < buffer.Length; x++)
        {
            buffer[x] = 0;
        }
    }

}