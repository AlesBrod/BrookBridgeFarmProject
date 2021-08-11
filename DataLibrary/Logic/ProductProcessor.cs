using DataLibrary.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;

namespace DataLibrary.Logic
{
    public static class ProductProcessor
    {
        public static int CreateProduct(string name, string description, string code, int price, int qty, string veg/*, int qtyToBuy, int summary*/)
        {
            ProductModel data = new ProductModel
            {
                Name = name,
                Description = description,
                Code = code,
                Price = price,
                Qty = qty,
                Veg = veg/*, 
                QtyToBuy = qtyToBuy,
                Summary = summary    */
            };
            string sql = @"INSERT INTO DBO.PRODUCT (Name, Description, Code, Price, Qty, Veg)
                            VALUES (@Name, @Description, @Code, @Price, @Qty, @Veg);";
            return SqlDataAccessForProduct.SaveData(sql, data);
        }


        public static int DeleteProduct(int id, string name, string description, string code, int price, int qty, string veg)
        {
            EditAndDeleteModel data = new EditAndDeleteModel
            {
                Id = id,
                Name = name,
                Description = description,
                Code = code,
                Price = price,
                Qty = qty,
                Veg = veg
            };

            string sql = @"DELETE FROM DBO.PRODUCT Where Id = @Id;";
            return SqlDataAccessForProduct.SaveData(sql, data);
        }

        public static List<ShoppingListModel> LoadProduct()
        {
            string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg, QtyToBuy, Summary
                        FROM DBO.PRODUCT;";

            return SqlDataAccessForProduct.LoadData<ShoppingListModel>(sql);

        }


    }


}
