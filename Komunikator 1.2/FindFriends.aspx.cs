using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindFriends : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string login = Session["login"].ToString();

        DataTable dt = this.GetRecords(Session["login"].ToString());

        int i = 0;
        //string rowLogin = "";
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
                    tCell1.ID = "loginCell" + i;

                }
                else
                {
                    TableCell tCell2 = new TableCell();
                    tRow.Cells.Add(tCell2);
                    tCell2.Text = row[column.ColumnName].ToString();
                }
            }

            TableCell writeCell = new TableCell();
            tRow.Cells.Add(writeCell);
            Button chatButton = new Button();
            chatButton.Text = "Dodaj";
            chatButton.ID = "" + i + 1;
            chatButton.Click += new EventHandler(button_Click);

            i++;
        }
    }

    

    private DataTable GetRecords(string login)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["Komunikator"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("select * from Uzytkownicy where Uzytkownicy.login_uzytkownika NOT IN(select Kontakty.login_kontaktu from Kontakty where Kontakty.login_wlasciciela like \'" + login + "\'")) 
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                //cmd.Parameters.AddWithValue("@login", login);
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
        Response.Redirect("ProfilePage.aspx");
    }


    protected void logout_Click(object sender, EventArgs e)
    {
        Session["login"] = null;
        Session["logged"] = false;
        Response.Redirect("Default.aspx");

    }

    protected void findFriends_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfilePage.aspx");
    }
}