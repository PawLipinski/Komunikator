using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryBox
/// </summary>
public class QueryBox
{
    public QueryBox()
    {
    }
    public static IEnumerable<IDataRecord> Retrieve(string sql, Action<SqlParameterCollection> addParameters)
    {

        using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Komunikator"].ToString()))
        using (var cmd = new SqlCommand(sql, cn))
        {
            addParameters(cmd.Parameters);

            cn.Open();
            using (var rdr = cmd.ExecuteReader())
            {
                try
                {
                    while (rdr.Read()) yield return rdr;
                    rdr.Close();
                }
                finally
                {
                    rdr.Close();
                }
            }
        }
    }

    public static void Insert(string sql, Action<SqlParameterCollection> addParameters)
    {

        using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Komunikator"].ToString()))
        using (var cmd = new SqlCommand(sql, cn))
        {
            addParameters(cmd.Parameters);

            cn.Open();
            cmd.ExecuteNonQuery();

        }
    }
    

    public static int Count(IEnumerable<IDataRecord> x)
    {
        int amount = 0;
        foreach (var item in x)
        {
            amount++;
        }
        return amount;
    }


}