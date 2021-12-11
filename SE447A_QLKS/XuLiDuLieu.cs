using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE447A_QLKS
{
    class XuLiDuLieu
    {
        public static int CapNhatDuLieu(string query)
        {
            SqlCommand command = new SqlCommand(query, ketNoiCSDL());
            try
            {
                return command.ExecuteNonQuery();
            }
            catch (SqlException) { }
            return 0;
        }
        public static object docdulieuduynhat(string query)
        {
            SqlCommand com = new SqlCommand(query, ketNoiCSDL());
            return com.ExecuteScalar();
        }
        public static DataSet docDulieu(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, ketNoiCSDL());
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
        private static SqlConnection ketNoiCSDL()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-SGKVLKK\SQLBINH;Initial Catalog=QLKS_PM;Integrated Security=True");
            connect.Open();
            return connect;
        }
        public static int capNhatDuLieuStored(string storename, object[] dulieu, string[] tenthamso)
        {
            SqlCommand com = new SqlCommand(storename, ketNoiCSDL());
            com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dulieu.Length; i++)
                com.Parameters.Add(new SqlParameter(tenthamso[i], dulieu[i]));
            return com.ExecuteNonQuery();
        }
        public static DataTable docDuLieuStored(string storename, object[] dulieu, string[] tenthamso)
        {
            SqlCommand com = new SqlCommand(storename, ketNoiCSDL());
            com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dulieu.Length; i++)
                com.Parameters.Add(new SqlParameter(tenthamso[i], dulieu[i]));
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
