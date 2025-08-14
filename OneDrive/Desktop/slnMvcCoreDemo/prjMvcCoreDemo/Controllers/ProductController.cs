using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public ProductController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult List(CKeywordViewModel p)
        {
            DbDemoContext db = new DbDemoContext();
            IEnumerable<TProduct> datas = null;
            if (string.IsNullOrEmpty(p.txtKeyword))
            {
                datas = from d in db.TProducts
                        select d;//因為p跟linq語法p重複,所以改成d
            }
            else
                datas = db.TProducts.Where(d => d.FName.Contains(p.txtKeyword));
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CProductWrap p)//從綠框CProductWrap拿資料
        {
            DbDemoContext db = new DbDemoContext();
            db.TProducts.Add(p.product);//product是黑框
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult CopyAndCreate(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            DbDemoContext db = new DbDemoContext();
            TProduct d = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (d != null)
            {
                TProduct product = new TProduct();
                product.FName = d.FName;
                product.FQty = d.FQty;
                product.FCost = d.FCost;
                product.FPrice = d.FPrice;
                db.TProducts.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                DbDemoContext db = new DbDemoContext();
                TProduct d = db.TProducts.FirstOrDefault(p => p.FId == id);
                if (d != null)
                {
                    db.TProducts.Remove(d);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            DbDemoContext db = new DbDemoContext();
            TProduct d = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public ActionResult Edit(CProductWrap uiProd)
        {
            DbDemoContext db = new DbDemoContext();
            TProduct dbProd = db.TProducts.FirstOrDefault(p => p.FId == uiProd.FId);
            if (dbProd == null)
                return RedirectToAction("List");
            if (uiProd.photo != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                dbProd.FImagePath = photoName;
                uiProd.photo.CopyTo(new FileStream(_enviro.WebRootPath + "/images/" + photoName, FileMode.Create));
            }
            dbProd.FName = uiProd.FName;
            dbProd.FQty = uiProd.FQty;
            dbProd.FCost = uiProd.FCost;
            dbProd.FPrice = uiProd.FPrice;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
