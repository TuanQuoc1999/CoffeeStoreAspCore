using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.Helper;
using CoffeeStoreAspCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace CoffeeStoreAspCore.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            var km = SessionHelper.GetObjectFromJson<KM>(HttpContext.Session, "khuyenmai");
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart !=null)
            {
                ViewBag.cart = cart;
                if(km != null)
                {
                    ViewBag.total = cart.Sum(item => item.Drink.UnitPrice * item.Quantity) - ((cart.Sum(item => item.Drink.UnitPrice * item.Quantity)*km.DiscountPercent)/100);
                }
                else
                {
                    ViewBag.total = cart.Sum(item => item.Drink.UnitPrice * item.Quantity);
                }
                return View();
            }
            else
            {
                ViewBag.cart = null;
                ViewBag.err = "Khong co gi trong gio hang";
                return View();
            }
            
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            StoreDBContext productModel = new StoreDBContext();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Drink = productModel.Drinks.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Drink = productModel.Drinks.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("edit/{id&data}")]
        public IActionResult Edit(int id ,string data)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart[index].Quantity = Convert.ToInt32(data);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Drink.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [Route("thanhtoan")]
        public async Task<IActionResult> ThanhToanAsync()
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(cart);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync
            ("https://localhost:5001/api/Voucher/CreateNew", httpContent);

                return RedirectToAction("Index");
            }
        }
        [Route("khuyenmai/{code}")]
        public IActionResult KhuyenMai(string code)
        {
            StoreDBContext productModel = new StoreDBContext();
            KM a = new KM();
            var b = from x in productModel.Vouchers
                    where x.CodeText == code
                    select x.AvailableTimes;
            var c = from x in productModel.Vouchers
                    where x.CodeText == code
                    select x.DiscountPercent;
            a.AvailableTimes = Convert.ToInt32(b);
            a.DiscountPercent = Convert.ToInt32(c);

            
            
            SessionHelper.SetObjectAsJson(HttpContext.Session, "khuyenmai", a);
            return RedirectToAction("Index");
        }
    }
}