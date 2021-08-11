using Dapper;
using DataLibrary.Models;
using DataLibrary.Logic;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccessForProduct
    {
        public static string GetConnectionString(string connectionName = "Product")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }

        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }

        }
       
        public static void SaveForEdit(EditAndDeleteModel product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Product"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SaveEdit", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = product.Id
                };
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = product.Name
                };
                cmd.Parameters.Add(paramName);

                SqlParameter paramDescription = new SqlParameter
                {
                    ParameterName = "@Description",
                    Value = product.Description
                };
                cmd.Parameters.Add(paramDescription);

                SqlParameter paramCode = new SqlParameter
                {
                    ParameterName = "@Code",
                    Value = product.Code
                };
                cmd.Parameters.Add(paramCode);

                SqlParameter paramPrice = new SqlParameter
                {
                    ParameterName = "@Price",
                    Value = product.Price
                };
                cmd.Parameters.Add(paramPrice);

                SqlParameter paramQty = new SqlParameter
                {
                    ParameterName = "@Qty",
                    Value = product.Qty
                };
                cmd.Parameters.Add(paramQty);

                SqlParameter paramVeg = new SqlParameter
                {
                    ParameterName = "@Veg",
                    Value = product.Veg
                };
                cmd.Parameters.Add(paramVeg);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public static void Minus(ShoppingListModel product)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Product"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Minus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = product.Id
                };
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = product.Name
                };
                cmd.Parameters.Add(paramName);

                SqlParameter paramDescription = new SqlParameter
                {
                    ParameterName = "@Description",
                    Value = product.Description
                };
                cmd.Parameters.Add(paramDescription);

                SqlParameter paramCode = new SqlParameter
                {
                    ParameterName = "@Code",
                    Value = product.Code
                };
                cmd.Parameters.Add(paramCode);

                SqlParameter paramPrice = new SqlParameter
                {
                    ParameterName = "@Price",
                    Value = product.Price
                };
                cmd.Parameters.Add(paramPrice);

                SqlParameter paramQty = new SqlParameter
                {
                    ParameterName = "@Qty",
                    Value = product.Qty
                };
                cmd.Parameters.Add(paramQty);

                SqlParameter paramVeg = new SqlParameter
                {
                    ParameterName = "@Veg",
                    Value = product.Veg
                };
                cmd.Parameters.Add(paramVeg);

                SqlParameter paramQtyToBuy = new SqlParameter
                {
                    ParameterName = "@QtyToBuy",
                    Value = product.QtyToBuy
                };
                cmd.Parameters.Add(paramQtyToBuy);
                con.Open();
                cmd.ExecuteNonQuery();
                ShoppingListProcessor.UpdateMinus(
                       product.Id,
                       product.Qty,
                       product.QtyToBuy);
                con.Close();
            }
        }

       

        public static int DeleteData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }

        }



    }
}
