using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace AdminLibrary
{
   public class AdminDAL
    {
        public bool AddAdmin(Admin admin)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertAdmin", cn);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_AdminName", admin.AdminName);
                cmd.Parameters.AddWithValue("@p_Email", admin.Email);
                cmd.Parameters.AddWithValue("@p_Password", admin.Password);

                cn.Open();
                cmd.ExecuteNonQuery();
                operationStatus = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            return operationStatus;

        }
        public List<Admin> GetAdminList()
        {
            List<Admin> Adminlist = new List<Admin>();
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from Admin", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Admin a= new Admin();
                a.AdminId = Convert.ToInt32(dr["AdminId"]);
                a.AdminName= dr["AdminName"].ToString();
                a.Email= dr["Email"].ToString();
                a.Password = dr["Password"].ToString();
                Adminlist.Add(a);


            }
            cn.Close();
            cn.Dispose();


            return Adminlist;
        }
        public bool ValidateAdminLogin(string email, string password)
        {
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE Email = @Email AND Password = @Password", cn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cn.Open();
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool ValidateAdminMail(string email)
        {
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE Email = @Email", cn);
            cmd.Parameters.AddWithValue("@Email", email);
            cn.Open();
            int count = (int)cmd.ExecuteScalar();
            return count > 0;

        }
    }
}
