using Dapper;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "Users")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static string LoadData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
                return cnn.Query(sql).ToString();
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

        public static int SaveUserPermission<T>(string sql, T data)
        {
                using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
                {
                    return cnn.Execute(sql, data);              
                }
            
        }


    }
}
