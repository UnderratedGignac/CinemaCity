using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace CinemaCity
{
    internal class Connection
    {  
        public SqlConnection conn = new SqlConnection("Data Source=LAPTOP-F4493UV9\\SQLEXPRESS;Initial Catalog=CinemaCIty;Integrated Security=True;TrustServerCertificate=True");
        public void openCon()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void closeCon()
        {
            conn.Close();
        }
        public SqlDataReader selectQ(String sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                return r;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public void modifyQ(String sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("bye");
            }
        }
    }
}
