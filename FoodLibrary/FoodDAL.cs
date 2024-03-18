using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace FoodLibrary
{
  public  class FoodDAL
    {
        public bool AddFood(Food food)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertFood", cn);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_FoodName", food.FoodName);
                cmd.Parameters.AddWithValue("@p_FoodPrice",food.FoodPrice);
                

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
        public bool EditFood(Food food, int FoodId)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_UpdateFood]", cn);
            try
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_FoodId",FoodId);
                cmd.Parameters.AddWithValue("@p_FoodName",food.FoodName);
                cmd.Parameters.AddWithValue("@p_FoodPrice",food.FoodPrice);
               
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
        public bool RemoveFood(int FoodId)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("delete  from Food where FoodId= " + FoodId, cn);
            cn.Open();
            cmd.ExecuteNonQuery();

            operationStatus = true;
            cn.Close();
            cn.Dispose();

            return operationStatus;

        }
        public List<Food> GetFoodList()
        {
            List<Food> Foodlist = new List<Food>();
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from Food", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Food food = new Food();
                food.FoodId= Convert.ToInt32(dr["FoodId"]);
                food.FoodName = dr["FoodName"].ToString();
                food.FoodPrice = Convert.ToSingle( dr["FoodPrice"]);
               
                Foodlist.Add(food);


            }
            cn.Close();
            cn.Dispose();


            return Foodlist;
        }
        public Food FindFood(int FoodId)
        {

            Food food= new Food();
            string str = ConfigurationManager.ConnectionStrings["KitchenConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("select * from Food where FoodId= " + FoodId, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                food.FoodId = Convert.ToInt32(dr["FoodId"]);
                food.FoodName = dr["FoodName"].ToString();
                food.FoodPrice = Convert.ToSingle(dr["FoodPrice"]);

            }
            cn.Close();
            cn.Dispose();
            return food;
        }
    }
}
