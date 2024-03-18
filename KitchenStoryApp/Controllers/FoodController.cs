using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodLibrary;
using KitchenStoryApp.Models;

namespace KitchenStoryApp.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index()
        {
            FoodDAL dal = new FoodDAL();
            List<Food> foodlist = dal.GetFoodList();
            List<FoodModel> foodModels = new List<FoodModel>();
            foreach (Food food in foodlist)
            {
                foodModels.Add(new FoodModel { FoodId = food.FoodId, FoodName = food.FoodName, FoodPrice = food.FoodPrice });

            }
            return View(foodModels);
        }

        // GET: Food/Details/5
        public ActionResult Details(int id)
        {
            int FoodId = id;

            FoodDAL dal = new FoodDAL();
            Food food = new Food();
            food = dal.FindFood(FoodId);
            FoodModel model = new FoodModel();
            model.FoodId = food.FoodId;
            model.FoodName = food.FoodName;
            model.FoodPrice = food.FoodPrice;
            return View(model);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                FoodDAL dal = new FoodDAL();


                Food food = new Food();
                food.FoodName = collection["FoodName"];
                food.FoodPrice = Convert.ToSingle(collection["FoodPrice"]);

                dal.AddFood(food);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        // GET: Food/Edit/5
        public ActionResult Edit(int id)
        {
            int FoodId = id;

            FoodDAL dal = new FoodDAL();
            Food food = new Food();
            food = dal.FindFood(FoodId);
            FoodModel model = new FoodModel();
            model.FoodId = food.FoodId;
            model.FoodName = food.FoodName;
            model.FoodPrice = food.FoodPrice;
            return View(model);
        }

        // POST: Food/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            bool completed = false;
            try
            {
                FoodDAL dal = new FoodDAL();
                Food food = new Food();
                food.FoodId = id;
                food.FoodName = collection["FoodName"];
                food.FoodPrice = Convert.ToSingle(collection["FoodPrice"]);
                completed = dal.EditFood(food, id);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
            if (completed)
                return RedirectToAction("Index");
            else

                return View();
        }

        // GET: Food/Delete/5
        public ActionResult Delete(int id)
        {
            int FoodId = id;

            FoodDAL dal = new FoodDAL();
            Food food = new Food();
            food = dal.FindFood(FoodId);
            FoodModel model = new FoodModel();
            model.FoodId = food.FoodId;
            model.FoodName = food.FoodName;
            model.FoodPrice = food.FoodPrice;
            return View(model);
        }

        // POST: Food/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool completed = false;
            try
            {
                // TODO: Add delete logic here
                FoodDAL dal = new FoodDAL();
                int foodid = id;
                completed = dal.RemoveFood(foodid);
                if (completed)
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
            return View();
        }
        public ActionResult Menu()
        {
            FoodDAL dal = new FoodDAL();
            List<Food> foodlist = dal.GetFoodList();
            List<FoodModel> foodmodels = new List<FoodModel>();
            foreach (Food item in foodlist)
            {
                foodmodels.Add(new FoodModel { FoodId = item.FoodId, FoodName = item.FoodName, FoodPrice = item.FoodPrice });

            }
            return View(foodmodels);
        }
        public ActionResult SelectedFood(int Foodid)
        {
            int food_id = Foodid;
            FoodDAL dal = new FoodDAL();
            Food food = new Food();
            food = dal.FindFood(food_id);
            FoodModel model = new FoodModel();
            model.FoodId = food.FoodId;
            model.FoodName = food.FoodName;
            model.FoodPrice = food.FoodPrice;
            TempData["FoodPrice"] = model.FoodPrice;
            TempData["FoodName"] = model.FoodName;
            TempData.Keep();
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectedFood(int qty, int Foodid)
        {
            if (TempData["FoodName"] != null && TempData["FoodPrice"] != null)
            {
                string fooditem = TempData["FoodName"].ToString();
                float price = Convert.ToSingle(TempData["FoodPrice"]);
                float Total_Amt = price * qty;
                TempData["Total_amt"] = Total_Amt;

                TempData.Keep();
                return RedirectToAction("Payment");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Payment()
        {
            ViewBag.TotalAmount = Convert.ToSingle(TempData["Total_amt"]);

            return View();
        }
    }
       
}
