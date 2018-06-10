using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace HotelManagement
{
    public class DataConnectSQL
    {
        private static SqlConnection conn;

        public static void Connect()
        {

            try
            {
                string strConn = "Data Source=NGUYENDINHP79CD\\PHAT;Initial Catalog=QuanLyKhachSan;Persist Security Info=True;User ID=sa;Password=123";
                conn = new SqlConnection(strConn);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString());
            }
        }

        public static void DisConnect()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            else
            {
                conn.Dispose();
            }
        }

        public static int ExcuteQuery(string query)
        {
            int result = -1;
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString());
            }
            finally
            {
                DisConnect();
            }
            return result;
        }
        public static int ExcuteProcedure(string query, string user, int numRoom, DateTime checkIn, DateTime checkOut)
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@num_Room", numRoom);
                cmd.Parameters.AddWithValue("@check_In", checkIn);
                cmd.Parameters.AddWithValue("@check_Out", checkOut);
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@result"].Value = "";
                cmd.ExecuteNonQuery();
                int result = (int)cmd.Parameters["@result"].Value;
                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.ToString());
            }
            finally
            {
                DisConnect();
            }
            return 0;
        }
        public static DataTable LoadData(string strLenh)
        {
            DataTable ds = new DataTable();
            SqlCommand cmd = new SqlCommand(strLenh, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DisConnect();
            return ds;
        }
    }
}
