using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLibrary;
using KitchenStoryApp.Models;

namespace KitchenStoryApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminDAL _adminDAL = new AdminDAL();
        public ActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminRegister(AdminModel adminModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Admin admin = new Admin
                    {
                        AdminName = adminModel.AdminName,
                        Email = adminModel.Email,
                        Password = adminModel.Password
                    };

                    _adminDAL.AddAdmin(admin);
                    return RedirectToAction("Index", "Food");
                }
                return View(adminModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(adminModel);
            }
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminModel loginModel)
        {
            try
            {
                bool isValidLogin = _adminDAL.ValidateAdminLogin(loginModel.Email, loginModel.Password);

                if (isValidLogin)
                {
                    return RedirectToAction("Index", "Food");
                }
                ViewBag.ErrorMsg = "Invalid email or password.";
                return View(loginModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View(loginModel);
            }
        }

    }
}