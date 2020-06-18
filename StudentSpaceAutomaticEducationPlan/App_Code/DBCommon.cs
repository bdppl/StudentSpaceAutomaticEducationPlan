using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentSpaceAutomaticEducationPlan
{
   
    public class DBCommon
    {
        public string connStr = "";
        SqlConnection con;
        public DBCommon()
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        }

        public DataTable GetTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable() ;
            try
            {
                con = new SqlConnection(connStr);
                cmd.Connection = con;
                con.Open();

                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);

            }
            catch(Exception ex)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();

            }

            return dt;
        }

        public String GetValue(SqlCommand cmd)
        {
            string obj = "";
            try
            {
                con = new SqlConnection(connStr);
                cmd.Connection = con;
                con.Open();

                obj= Convert.ToString(cmd.ExecuteScalar());
                


            }
            catch (Exception ex)
            {
                obj = "";

            }
            finally
            {
                con.Close();
                con.Dispose();

            }

            return obj;
        }
    }
}