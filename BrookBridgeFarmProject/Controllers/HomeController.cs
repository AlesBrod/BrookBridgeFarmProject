using System;
using DataLibrary.Models;
using DataLibrary.DataAccess;
using DataLibrary.Logic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace BrookBridgeFarmProject.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Contact()
        {
            return View("Contact");
        }

        public ActionResult Index()
        {
            /*if (Session["user"]==null || Session ["admin"] == null)
            {
                return RedirectToAction("login", "Account");
            }*/
            return View("Index");
        }

        [Authorize]
        public ActionResult ProductDatabase()

        {            
            return View(ProductProcessor.LoadProduct());
        }
        
        public ActionResult About()
        {
            return View("About");
        }

        [Authorize(Roles = "Admin")]
        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                ProductProcessor.CreateProduct(

                    product.Name,
                    product.Description,
                    product.Code,
                    product.Price,                    
                    product.Qty,
                    product.Veg
                    );
                return RedirectToAction("ProductDatabase");
            }

            return View();
        }

        // GET: Home/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {

                string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg
                        FROM DBO.PRODUCT;";
                var productToEdit = (from m in SqlDataAccessForProduct.LoadData<EditAndDeleteModel>(sql)
                                     where m.Id == id
                                     select m).Single();
                return View(productToEdit);

            }
            return View();
        }


            // POST: Home/Edit/5
            [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EditAndDeleteModel product)
            {
                if (ModelState.IsValid)
                {
                    SqlDataAccessForProduct.SaveForEdit(product);
                    return RedirectToAction("ProductDatabase");
                }
                return View(product);
            }
            

        // GET: Home/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg
                        FROM DBO.PRODUCT;";
            var productToDelete = (from m in SqlDataAccessForProduct.LoadData<DataLibrary.Models.EditAndDeleteModel>(sql)
                                   where m.Id == id
                                   select m).Single();
            return View(productToDelete);

        }

        // POST: Home/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(EditAndDeleteModel product)
        {
            if (ModelState.IsValid)
            {
                ProductProcessor.DeleteProduct(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Code,
                    product.Price,
                    product.Qty,
                    product.Veg

                    );
                return RedirectToAction("ProductDatabase");
            }

            return View();
        }
        [Authorize]
        public ActionResult DeleteFromBasket(int id)
        {
            string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg, QtyToBuy, Summary, SumTotal
                        FROM DBO.SHOPPINGLIST;";
            var productToDelete = (from m in SqlDataAccessForShoppingList.LoadData<ShoppingListModel>(sql)
                                   where m.Id == id
                                   select m).Single();
            return View(productToDelete);

        }


        [HttpPost]
        [Authorize]
        public ActionResult DeleteFromBasket(EditAndDeleteModel product)
        {
            if (ModelState.IsValid)
            {
                ShoppingListProcessor.Delete(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Code,
                    product.Price,
                    product.Qty,
                    product.Veg,
                    product.QtyToBuy,
                    product.Summary,
                    product.SumTotal

                    );
                return RedirectToAction("ShoppingListView");
            }

            return View();
        }


        [Authorize]
        public ActionResult DeleteAllFromBasket()
        {
            string sql = @"SELECT * FROM DBO.SHOPPINGLIST;";
            var productToDelete = from m in SqlDataAccessForShoppingList.LoadData<DataLibrary.Models.ShoppingListModel>(sql)
                                  select m;
            return View(productToDelete);

        }


        [HttpPost]
        [Authorize]
        public ActionResult DeleteAllFromBasket(EditAndDeleteModel product)
        {
            if (ModelState.IsValid)
            {
                ShoppingListProcessor.DeleteAll(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Code,
                    product.Price,
                    product.Qty,
                    product.Veg,
                    product.QtyToBuy

                    );
                return RedirectToAction("ShoppingListView");
            }
            return View();
        }

        [Authorize]
                public ActionResult Buy(int id)
        {
            string sql = @"SELECT Id, Name, Description, Code, Price, Qty, Veg, Summary
                        FROM DBO.PRODUCT;";
            var productToBuy = (from m in SqlDataAccessForProduct.LoadData<ShoppingListModel>(sql)
                                where m.Id == id
                                select m).Single();            

            return View(productToBuy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Buy(ShoppingListModel product)
        {

            if (ModelState.IsValid & product.Qty >= product.QtyToBuy)
            {
                if (product.QtyToBuy <= 0)
                {                   
                        return RedirectToAction("ProductDatabase");
                   }
                
                ShoppingListProcessor.CreateShoppingList(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Code,
                    product.Price,
                    product.Qty,
                    product.Veg,
                    product.QtyToBuy,
                    product.Summary                   
                         
                        );   
                SqlDataAccessForProduct.Minus(product);               
                
                return RedirectToAction("ProductDatabase");
                }           
            else { return RedirectToAction("Index"); }

        }      
        [Authorize]
        public ActionResult ShoppingListView()
        {            
               return View(ShoppingListProcessor.LoadShoppingList());
        }

       public ActionResult ConfirmOrder()
        {
            string userId = "Order_" + User.Identity.GetUserName().ToString();
            return View(ShoppingListProcessor.CreateOrder(userId));
        }
        [Authorize]
       /* [ValidateAntiForgeryToken]*/
        public ActionResult LoadOrderView(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            try
            {
                string userId = "Order_" + User.Identity.GetUserName().ToString();
                return View(ShoppingListProcessor.LoadOrder(userId));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("EmptyOrder");
            }
        }

        
        public ActionResult EmptyOrder()
        {
            return View();
        }

        [Authorize]
       /* [ValidateAntiForgeryToken]*/
        public ActionResult LoadDatabases()
        {
            string userId = "Order_"+User.Identity.GetUserName().ToString();
           
            return View(ShoppingListProcessor.DatabaseListing(userId));
            
        }
       
        public ActionResult OrderForPrint(string name)
        {  
            string sql = @"SELECT * FROM dbo."+ name + ";"; 
          return View(SqlDataAccessForShoppingList.LoadData<ShoppingListModel>(sql));
        }


    }
}
