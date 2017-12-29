using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;
using System.Data;
using System.Text;



public partial class ProfilePage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string login = Session["login"].ToString();
        string imie;

        HttpCookie cookie = Request.Cookies["style"];
        if (cookie == null)
        {
            cookie = new HttpCookie("style");
            cookie.Value = "IndexStyle.css";
            Response.Cookies.Add(cookie);
        }

        pagestyle.Href = "" + cookie.Value;

        string connectionString = ConfigurationManager.ConnectionStrings["Komunikator"].ToString();
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand("select * from Uzytkownicy where login_uzytkownika like @login", connection))
        {
            command.Parameters.AddWithValue("@login", login);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            imie = reader["imie_uzytkownika"].ToString();
        }

        Session["name"] = imie;
        this.Powitanie.Text = "Witaj " + imie.ToString() + "! To jest strona Twojego profilu.";

        //if (this.IsPostBack)
        //{
            
            DataTable dt = this.GetRecords(Session["login"].ToString());

            int i=0;
            //string rowLogin="";
            foreach (DataRow row in dt.Rows)
            {
                //ContactsTable.Controls.Add(new LiteralControl("<form ation=\"ChatPage.aspx\">)"));
                TableRow tRow = new TableRow();
                ContactsTable.Rows.Add(tRow);
                foreach (DataColumn column in dt.Columns)
                {
 
                    if (String.Equals(column.ColumnName.ToString(), "login_uzytkownika"))
                    {
                        
                        TableCell tCell1 = new TableCell();
                        tRow.Cells.Add(tCell1);
                        tCell1.Text = row[column.ColumnName].ToString();
                        tCell1.ID = "loginCell" + (i+1);

                    }

                else {
                        TableCell tCell2 = new TableCell();
                        tRow.Cells.Add(tCell2);
                        tCell2.Text = row[column.ColumnName].ToString();
                    }
                }

                TableCell writeCell = new TableCell();
                tRow.Cells.Add(writeCell);
                Button chatButton = new Button();
                chatButton.OnClientClick = "return interactWithContact();";
                chatButton.Text = "Napisz";
                chatButton.ID  = ""+(i+1);
                chatButton.Click += new EventHandler(button_Click);

            writeCell.Controls.Add(chatButton);
                TableCell deleteCell = new TableCell();
                tRow.Cells.Add(deleteCell);
                Button deleteButton = new Button();
                deleteButton.Text = "Usuń";
                deleteCell.Controls.Add(deleteButton);
               
                i++;

            //}
            //contDiv.Controls.Add(new LiteralControl("</form>"));
        }
    }

    private DataTable GetRecords(string login)
    {
        try { 
        string constr = ConfigurationManager.ConnectionStrings["Komunikator"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand("select Uzytkownicy.login_uzytkownika, Uzytkownicy.imie_uzytkownika, Uzytkownicy.nazwisko_uzytkownika, status_uzytkownika FROM(Kontakty INNER JOIN Uzytkownicy ON Uzytkownicy.login_uzytkownika = Kontakty.login_kontaktu) Where Kontakty.login_wlasciciela like \'" + login + "\'"))
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);
                return dt;
            }
        }
        }
        catch (Exception e)
        {
            return null;
        }
    }

    protected void button_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;

        int searched = Int32.Parse(button.ID);
        TableRow justRow = ContactsTable.Rows[searched];
        string login = justRow.Cells[0].Text;

        Session["interlocutor"] = login;

        Response.Redirect("ChatPage.aspx");
    }


    protected void logout_Click(object sender, EventArgs e)
    {
        QueryBox.Insert(
              "Update Uzytkownicy SET status_uzytkownika = 0 where login_uzytkownika LIKE @login",
             p =>
             {
                 p.Add("@login", SqlDbType.Text).Value = Session["login"];

             });
        Session["login"] = null;
        Session["logged"] = false;
        Response.Redirect("Default.aspx");
    }

    protected void findFriends_Click(object sender, EventArgs e)
    {
        Response.Redirect("FindFriends.aspx");
    }
}

