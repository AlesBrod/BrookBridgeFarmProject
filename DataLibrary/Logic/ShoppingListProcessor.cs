using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DataLibrary.Logic
{
    public static class ShoppingListProcessor
    {
        public static Random number = new Random();

        public static int RandomNumber (int min, int max)
        {
            return number.Next(min, max);
        }
      
        public static string dateTime = DateTime.Now.ToString("ddd_dd_MMM_yyy_hh_mm");
        public static string dbo = "_"+dateTime +"_"+RandomNumber(1,10000);
        public static int CreateShoppingList(int id, string name, string description, string code, int price, int qty, string veg, int qtyToBuy, int summary)
        {
            ProductModel data = new ProductModel
            {
                Id = id,
                Name = name,
                Description = description,
                Code = code,
                Price = price,
                Qty = qty,
                Veg = veg,
                QtyToBuy = qtyToBuy,
                Summary = summary
              
            };
                   
            string sql = @"INSERT INTO DBO.SHOPPINGLIST (Name, Description, Code, Price, Qty, Veg, QtyToBuy, Summary)
                           VALUES (@Name, @Description, @Code, @Price, @Qty, @Veg, @QtyToBuy, @Summary);";
                        
            return SqlDataAccess.SaveData(sql, data);
        }
       

        public static List<ShoppingListModel> LoadShoppingList()
        {
            EditAndDeleteModel slm = new EditAndDeleteModel();
                           
            string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg, QtyToBuy, Price * QtyToBuy AS [Summary]
                        FROM DBO.ShoppingList;
                        Update dbo.ShoppingList Set Summary = Price * QtyToBuy ;
                        ";

            SqlDataAccess.SaveData(sql, slm);
            return SqlDataAccess.LoadData<ShoppingListModel>(sql);

        }

        public static int Delete(int id, string name, string description, string code, int price, int qty, string veg, int qtyToBuy, int summary, int sumTotal)
        {
           EditAndDeleteModel data = new EditAndDeleteModel
           {
                Id = id,
                Name = name,
                Description = description,
                Code = code,
                Price = price,
                Qty = qty,
                Veg = veg,
                QtyToBuy = qtyToBuy,
                Summary = summary,
                SumTotal = sumTotal
            };

            string sql = @"DELETE FROM DBO.SHOPPINGLIST Where Id = @Id;";
            return SqlDataAccessForShoppingList.SaveData(sql, data);
        }
        public static int DeleteAll(int id, string name, string description, string code, int price, int qty, string veg, int qtyToBuy)
        {
            ShoppingListModel data = new ShoppingListModel
            {
                Id = id,
                Name = name,
                Description = description,
                Code = code,
                Price = price,
                Qty = qty,
                Veg = veg,
                QtyToBuy = qtyToBuy
            };

            string sql = @"DELETE DBO.SHOPPINGLIST;";
            return SqlDataAccessForShoppingList.SaveData(sql, data);
        }
        public static int UpdateMinus(int id, int qty, int qtyToBuy)
        {
            ShoppingListModel data = new ShoppingListModel
            {                
                Id = id,
                Qty = qty,
                QtyToBuy = qtyToBuy
            };
            string sql = @"update dbo.Product set Qty = Qty - QtyToBuy where Id = @Id AND Qty >= QtyToBuy;";
            return SqlDataAccessForShoppingList.SaveData(sql, data);
        }

        public static List<ShoppingListModel> CreateOrder(string userId)
        {
           
            ShoppingListModel slm = new ShoppingListModel();

            try
            {
                string sql =
    @"CREATE TABLE " + userId + dbo + @"(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Code] NVARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL, 
    [Qty] INT NOT NULL, 
    [Veg] NVARCHAR(50) NOT NULL, 
    [QtyToBuy] INT NULL, 
    [Summary] INT NULL   
);

INSERT INTO " + userId + dbo + @"(Id, Name, Description, Code, Price, Qty, Veg, QtyToBuy, Summary)
                           SELECT * FROM DBO.ShoppingList;";
                SqlDataAccess.SaveData(sql, slm);
            }
            catch(SqlException)
            {
               string sql = @"INSERT INTO " + userId + dbo + @"(Id, Name, Description, Code, Price, Qty, Veg, QtyToBuy, Summary)
                           SELECT* FROM DBO.ShoppingList; ";
                SqlDataAccess.SaveData(sql, slm);
            }
                
                return LoadOrder(userId);
            }


        public static List<ShoppingListModel> LoadOrder(string userId)
        {            
            

            string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg, QtyToBuy, Price * QtyToBuy AS [Summary]
                        FROM " + userId + dbo + "; DELETE dbo.ShoppingList;";
            return SqlDataAccess.LoadData<ShoppingListModel>(sql);
          

        }

        public static List<DatabaseList> DatabaseListing(string userId)
        {
            if (userId == "Order_Admin")
            {
                string sql2 = @"SELECT name, create_date FROM sys.tables WHERE Name LIKE '%order%' ORDER BY create_date desc;";
                return SqlDataAccess.LoadData<DatabaseList>(sql2);
            }
            string sql = @"SELECT name, create_date FROM sys.tables WHERE Name LIKE '%"+userId+"_"+"%' ORDER BY create_date desc;";
         return SqlDataAccess.LoadData<DatabaseList>(sql);
            
            
        }

      
    }     


    }
