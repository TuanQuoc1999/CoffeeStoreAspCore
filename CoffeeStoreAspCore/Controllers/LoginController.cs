using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAspCore.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {


        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        [Route("DangNhap")]
        public IActionResult DangNhap(string username, string pass, bool remember)
        {

            HttpContext.Session.SetString("username", username);
            return RedirectToAction("Index", "Home");
            

        }

        [HttpGet]
        [Route("dangxuat")]
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
        }

        //private bool CheckDangNhap(String username,String pass)
        //{
           
        //}


    }
}
