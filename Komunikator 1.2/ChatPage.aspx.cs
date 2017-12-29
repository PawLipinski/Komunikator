using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChatPage : System.Web.UI.Page
{
    private string intLogin;

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

        intLogin = Session["interlocutor"].ToString();
        //etykieta.Text = intLogin;
        LoadConversation();

    }

    private void LoadConversation()
    {
        
        DataTable dt = this.GetRecords(Session["login"].ToString(), Session["interlocutor"].ToString());

        if (dt != null)
        {
            int i = 1;
            //string rowLogin = "";
            foreach (DataRow row in dt.Rows)
            {
                //ContactsTable.Controls.Add(new LiteralControl("<form ation=\"ChatPage.aspx\">)"));
                TableRow tRow = new TableRow();
                chatTable.Rows.Add(tRow);
                foreach (DataColumn column in dt.Columns)
                {
                    if(String.Equals(column.ColumnName.ToString(), "odbiorca"))
                    {
                        continue;
                    }
                    else
                    { 
                        TableCell tCell2 = new TableCell();
                        tRow.Cells.Add(tCell2);
                        tCell2.Text = row[column.ColumnName].ToString();
                    }
                }

                i++;

                //}
                //contDiv.Controls.Add(new LiteralControl("</form>"));
            }
        }
        else
        {
            TableRow tRow = new TableRow();
            chatTable.Rows.Add(tRow);
            TableCell tCell2 = new TableCell();
            tRow.Cells.Add(tCell2);
            Label noLabel = new Label();
            tCell2.Text = "Nic do wyświetlenia!";
        }
        //ClientScript.RegisterStartupScript(GetType(), "act1", "$('#chatDiv').scrollTop($('#ChatDiv')[0].scrollHeight);", true);
    }

    private DataTable GetRecords(string login, string interlocutor)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["Komunikator"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("select Komunikaty.nadawca, Komunikaty.odbiorca, Komunikaty.data_dodania, Komunikaty.komunikat FROM Komunikaty Where Komunikaty.nadawca LIKE \'" + login + "\' AND Komunikaty.odbiorca LIKE \'" + interlocutor + "\' OR Komunikaty.nadawca LIKE\'" + interlocutor + "\' AND Komunikaty.odbiorca LIKE \'" +login + "\'"))
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

    protected void sendButton_Click(object sender, EventArgs e)
    {
        string communicate = textField.Text.ToString();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Komunikator"].ToString();
            //conn.ConnectionString = "Server=PAWEL-STAC\\SQLEXPRESS01;Database=KomunikatorDB;Trusted_Connection=true";
            conn.Open();
            var sqlcommand = new SqlCommand("INSERT INTO Komunikaty VALUES (@nadawca, @odbiorca, @data, @komunikat)", conn);

            sqlcommand.Parameters.AddWithValue("@nadawca", this.Session["login"].ToString());
            sqlcommand.Parameters.AddWithValue("@odbiorca", this.Session["interlocutor"].ToString());
            DateTime myDateTime = DateTime.Now;
            
            sqlcommand.Parameters.AddWithValue("@data", myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sqlcommand.Parameters.AddWithValue("@komunikat", communicate);


            try
            {
                sqlcommand.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Pomyślnie zarejestrowano!');", true);
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Error","alert('hi');", true);
                Response.Redirect(Request.RawUrl);
            }
            catch
            {
                return;
            }

            conn.Close();
        }
    }

    protected void backToProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfilePage.aspx");
    }
}