using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChatPage : System.Web.UI.Page
{
    private string intLogin;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindDummyRow();
        }

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
        Session["lastMessageDate"] = chatTable.Rows[chatTable.Rows.Count - 1].Cells[2].Text;

        //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert(\'"+Session["interlocutor"]+"\');", true);

    }

    public void LoadConversation()
    {

        DataSet dt = this.GetRecordsStart(Session["login"].ToString(), Session["interlocutor"].ToString());

        chatTable.DataSource = dt;
        chatTable.DataBind();
    }

    private DataSet GetRecordsStart(string login, string interlocutor)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["Komunikator"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("select Komunikaty.ID, Komunikaty.nadawca, Komunikaty.odbiorca, Komunikaty.data_dodania, Komunikaty.komunikat FROM Komunikaty Where Komunikaty.nadawca LIKE \'" + login + "\' AND Komunikaty.odbiorca LIKE \'" + interlocutor + "\' OR Komunikaty.nadawca LIKE \'" + interlocutor + "\' AND Komunikaty.odbiorca LIKE \'" + login + "\'"))
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet dt = new DataSet())
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

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static void SendMessage(string text)
    {
        //string communicate = textField.Text.ToString();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Komunikator"].ToString();
            //conn.ConnectionString = "Server=PAWEL-STAC\\SQLEXPRESS01;Database=KomunikatorDB;Trusted_Connection=true";
            conn.Open();
            var sqlcommand = new SqlCommand("INSERT INTO Komunikaty VALUES (@nadawca, @odbiorca, @data, @komunikat)", conn);

            sqlcommand.Parameters.AddWithValue("@nadawca", HttpContext.Current.Session["login"].ToString());
            sqlcommand.Parameters.AddWithValue("@odbiorca", HttpContext.Current.Session["interlocutor"].ToString());
            DateTime myDateTime = DateTime.Now;

            sqlcommand.Parameters.AddWithValue("@data", myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sqlcommand.Parameters.AddWithValue("@komunikat", text);


            try
            {
                sqlcommand.ExecuteNonQuery();
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Pomyślnie zarejestrowano!');", true);
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Error","alert('hi');", true);
                //Response.Redirect(Request.RawUrl);
            }
            catch
            {
                return;
            }

            conn.Close();
        }
    }

    //private void UpdateTable()
    //{
    //    DateTime tempLastMessage = new DateTime();

    //    #region old
    //    //SqlDataRecord[] = QueryBox.Retrieve(
    //    //      "select Komunikaty.nadawca, Komunikaty.odbiorca, Komunikaty.data_dodania, Komunikaty.komunikat FROM Komunikaty Where Komunikaty.nadawca LIKE @login AND Komunikaty.odbiorca LIKE @interlocutor OR Komunikaty.nadawca LIKE @interlocutor AND Komunikaty.odbiorca LIKE @login AND Komunikaty.data_dodania >= @data",
    //    //     p =>
    //    //     {
    //    //         p.Add("@login", SqlDbType.Text).Value = Session["login"];
    //    //         p.Add("@intrelocutor", SqlDbType.Text).Value = Session["interlocutor"];
    //    //         p.Add("@data", SqlDbType.DateTime).Value = lastMessageDate.ToString("yyy-MM-dd HH:mm:ss.fff");

    //    //     });
    //    #endregion

    //    DataTable myDataTable =null;

    //    try
    //    {
    //        string constr = ConfigurationManager.ConnectionStrings["Komunikator"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(constr))
    //        using (SqlCommand cmd = new SqlCommand("select Komunikaty.nadawca, Komunikaty.odbiorca, Komunikaty.data_dodania, Komunikaty.komunikat FROM Komunikaty Where Komunikaty.nadawca LIKE \'" + Session["login"] + "\' AND Komunikaty.odbiorca LIKE \'" + Session["interlocutor"] + "\' OR Komunikaty.nadawca LIKE\'" + Session["interlocutor"] + "\' AND Komunikaty.odbiorca LIKE \'" + Session["login"] + "\'AND Komunikaty.data_dodania >= "+ lastMessageDate.ToString("yyy-MM-dd HH:mm:ss.fff")))
    //        using (SqlDataAdapter sda = new SqlDataAdapter())
    //        {
    //            cmd.Connection = con;
    //            sda.SelectCommand = cmd;
    //            using (DataTable dt = new DataTable())
    //            {
    //                sda.Fill(dt);
    //                myDataTable=dt;
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        myDataTable=null;
    //    }


    //    if (myDataTable != null)
    //    {
    //        int i = 1;
    //        //string rowLogin = "";
    //        foreach (DataRow row in myDataTable.Rows)
    //        {
    //            //ContactsTable.Controls.Add(new LiteralControl("<form ation=\"ChatPage.aspx\">)"));
    //            TableRow tRow = new TableRow();
    //            chatTable.Rows.Add(tRow);
    //            foreach (DataColumn column in myDataTable.Columns)
    //            {
    //                if (String.Equals(column.ColumnName.ToString(), "odbiorca"))
    //                {
    //                    continue;
    //                }
    //                else
    //                {
    //                    TableCell tCell2 = new TableCell();
    //                    tRow.Cells.Add(tCell2);
    //                    tCell2.Text = row[column.ColumnName].ToString();
    //                    if (String.Equals(column.ColumnName.ToString(), "data_dodania"))
    //                    { 
    //                        tempLastMessage = (DateTime)row[column.ColumnName];
    //                    }
    //                }
    //            }

    //            i++;

    //            //}
    //            //contDiv.Controls.Add(new LiteralControl("</form>"));
    //        }
    //    }

    //    lastMessageDate = tempLastMessage;
    //}

    protected void backToProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfilePage.aspx");
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string LoadMessages()
    {

        string myLogin = HttpContext.Current.Session["login"].ToString();
        string interlocutor = HttpContext.Current.Session["interlocutor"].ToString();
        string lastMessageDate = HttpContext.Current.Session["lastMessageDate"].ToString();

        SqlDateTime sqlTime = SqlDateTime.Parse(lastMessageDate);

        SqlCommand cmd = new SqlCommand("select Komunikaty.ID, Komunikaty.nadawca, Komunikaty.data_dodania, Komunikaty.komunikat FROM Komunikaty Where Komunikaty.ID IN (select Komunikaty.ID WHERE Komunikaty.nadawca LIKE \'" + myLogin + "\' AND Komunikaty.odbiorca LIKE \'" + interlocutor + "\'AND Komunikaty.data_dodania > \'" + sqlTime + "\' OR Komunikaty.nadawca LIKE\'" + interlocutor + "\' AND Komunikaty.odbiorca LIKE \'" + myLogin + "\' AND Komunikaty.data_dodania > \'" + sqlTime + "\')");

        DataSet ds = GetData(cmd);
        if (!(ds.Tables[0].Rows.Count == 0))
        {
            HttpContext.Current.Session["lastMessageDate"] = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["data_dodania"].ToString();
        }

        return ds.GetXml();
    }

    private static DataSet GetData(SqlCommand cmd)
    {

        string constr = ConfigurationManager.ConnectionStrings["Komunikator"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        //using (SqlCommand cmd = new SqlCommand("select Komunikaty.nadawca, Komunikaty.odbiorca, Komunikaty.data_dodania, Komunikaty.komunikat FROM Komunikaty Where Komunikaty.nadawca LIKE \'" + login + "\' AND Komunikaty.odbiorca LIKE \'" + interlocutor + "\' OR Komunikaty.nadawca LIKE\'" + interlocutor + "\' AND Komunikaty.odbiorca LIKE \'" + login + "\'"))
        using (SqlDataAdapter sda = new SqlDataAdapter())
        {
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            using (DataSet ds = new DataSet())
            {
                sda.Fill(ds, "Komunikaty");
                return ds;
            }
        }

    }

    protected void loadButton_Click(object sender, EventArgs e)
    {
        //System.IO.File.WriteAllLines(@"D:\text.txt", LoadMessages().Split('\n'));
    }

    private void BindDummyRow()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("ID");
        dummy.Columns.Add("nadawca");
        dummy.Columns.Add("data_dodania");
        dummy.Columns.Add("komunikat");
        dummy.Rows.Add();
        chatTable.DataSource = dummy;
        chatTable.DataBind();
    }
}