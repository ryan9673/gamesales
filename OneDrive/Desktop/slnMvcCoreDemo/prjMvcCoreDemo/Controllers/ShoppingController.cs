using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ShoppingController : Controller
    {
        public ActionResult CartView()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                return RedirectToAction("List");
            List<CShoppingCartItem> cart = null;
            string json = "";
            json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
            cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            return View(cart);
        }
        public IActionResult ResponseView(int? id)
        {
            var datas = (new DbDemoContext()).TResponses.Where(m => m.FProductId == id);
            ViewBag.FProductId = id;
            return View(datas);
        }
        [HttpPost]
        public IActionResult ResponseView(TResponse p)
        {
            p.FDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            DbDemoContext db = new DbDemoContext();
            db.TResponses.Add(p);
            db.SaveChanges();

            return RedirectToAction("ResponseView", new { id = p.FProductId });
        }
        public IActionResult List()
        {
            DbDemoContext db = new DbDemoContext();
            var datas = from p in db.TProducts
                        select p;                       //datas藍框
            List<CProductWrap> list = new List<CProductWrap>();//加工變綠框  
            foreach (var p in datas) 
                list.Add(new CProductWrap(p));
            return View(list);
        }
        public IActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.PRODUCTID = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(CAddToCartViewModel p)
        {
            DbDemoContext db = new DbDemoContext();
            TProduct product = db.TProducts.FirstOrDefault(m => m.FId == p.txtProductId);
            if (product == null)
                return RedirectToAction("List");

            List<CShoppingCartItem> cart = null;
            string json = "";//從購物車取出是字串
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))//如果有這組key則從session取出
            {
                json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            }
            else
                cart = new List<CShoppingCartItem>();
            CShoppingCartItem x = new CShoppingCartItem();
            x.productId = p.txtProductId;
            x.count = p.txtCount;
            x.price = (decimal)product.FPrice;
            x.product = product;
            cart.Add(x);
            json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST, json);

            return RedirectToAction("List");
        }
    }
}
